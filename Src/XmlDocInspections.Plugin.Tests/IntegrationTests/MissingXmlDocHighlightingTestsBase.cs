using System;
using System.IO;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.Util;
using XmlDocInspections.Plugin.Highlighting;

#if RESHARPER8
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
#endif

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
            return Path.GetDirectoryName(callerFilePath.NotNull());
        }

#if RESHARPER8
        protected override bool HighlightingPredicate(IHighlighting highlighting, IContextBoundSettingsStore settingsStore)
#else
        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile sourceFile)
#endif
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