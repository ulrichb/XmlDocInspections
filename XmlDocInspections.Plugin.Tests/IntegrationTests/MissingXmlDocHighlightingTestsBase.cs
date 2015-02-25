using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp;
using XmlDocInspections.Plugin.Highlighting;

namespace XmlDocInspections.Plugin.Tests.IntegrationTests
{
    public abstract class MissingXmlDocHighlightingTestsBase : CSharpHighlightingTestNet4Base
    {
        protected override string RelativeTestDataPath
        {
            get { return "XmlDocInspections.Sample"; }
        }

        protected override string GetGoldTestDataPath(string fileName)
        {
            return Path.Combine(GetCodeFileDirectoryPath(), GetType().Name, fileName + ".gold");
        }

        private static string GetCodeFileDirectoryPath([CallerFilePath] string callerFilePath = null)
        {
            Trace.Assert(callerFilePath != null);
            return Path.GetDirectoryName(callerFilePath);
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