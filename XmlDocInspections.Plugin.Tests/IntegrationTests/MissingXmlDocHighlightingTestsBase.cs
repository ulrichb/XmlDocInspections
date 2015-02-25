using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp;
using JetBrains.Util;
using XmlDocInspections.Plugin.Highlighting;

namespace XmlDocInspections.Plugin.Tests.IntegrationTests
{
    public abstract class MissingXmlDocHighlightingTestsBase : CSharpHighlightingTestNet4Base
    {
        protected override string RelativeTestDataPath
        {
            get { return "."; }
        }

        protected override string GetGoldTestDataPath(string fileName)
        {
            return Path.Combine(GetCodeFileDirectoryPath(), GetType().Name, fileName + ".gold");
        }

        private static string GetCodeFileDirectoryPath([CallerFilePath] string callerFilePath = null)
        {
            return Path.GetDirectoryName(callerFilePath.NotNull());
        }

        protected override bool HighlightingPredicate(IHighlighting highlighting, IContextBoundSettingsStore settingsStore)
        {
            return highlighting is XmlDocHighlightingBase;
        }

        protected override void DoNamedTest2(params string[] auxFiles)
        {
            ExecuteWithinSettingsTransaction(settingsStore =>
            {
                RunGuarded(() => MutateSettings(settingsStore));
                base.DoNamedTest2(auxFiles);
            });
        }

        protected abstract void MutateSettings([NotNull]IContextBoundSettingsStore settingsStore);
    }
}