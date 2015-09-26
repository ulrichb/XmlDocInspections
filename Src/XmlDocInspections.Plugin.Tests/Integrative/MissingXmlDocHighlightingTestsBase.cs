using System.IO;
using JetBrains.Annotations;
using JetBrains.Application.Settings;
using XmlDocInspections.Plugin.Highlighting;
#if RESHARPER8
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;

#endif

namespace XmlDocInspections.Plugin.Tests.Integrative
{
    public abstract class MissingXmlDocHighlightingTestsBase : CSharpHighlightingTestNet4Base
    {
        protected override string RelativeTestDataPath => "Highlighting";

        protected override string GetGoldTestDataPath(string fileName)
        {
            return base.GetGoldTestDataPath(Path.Combine(GetType().Name, fileName));
        }

#if RESHARPER8
        protected override bool HighlightingPredicate(IHighlighting highlighting, IContextBoundSettingsStore settingsStore)
#else
        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile sourceFile)
#endif
        {
            return highlighting is MissingXmlDocHighlighting;
        }

        protected override void DoNamedTest2(params string[] auxFiles)
        {
            ExecuteWithinSettingsTransaction(settingsStore =>
            {
                RunGuarded(() => MutateSettings(settingsStore));
                base.DoNamedTest2(auxFiles);
            });
        }

        protected abstract void MutateSettings([NotNull] IContextBoundSettingsStore settingsStore);
    }
}