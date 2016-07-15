using System.IO;
using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.TestFramework;
using XmlDocInspections.Plugin.Highlighting;

namespace XmlDocInspections.Plugin.Tests.Integrative
{
    [TestNetFramework4]
    public abstract class MissingXmlDocHighlightingTestsBase : CSharpHighlightingTestBase
    {
        protected override string RelativeTestDataPath => "Highlighting";

        protected override string GetGoldTestDataPath(string fileName)
        {
            return base.GetGoldTestDataPath(Path.Combine(GetType().Name, fileName));
        }

        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile sourceFile)
        {
            return highlighting is MissingXmlDocHighlighting;
        }

        protected override void DoTestSolution(params string[] fileSet)
        {
            ExecuteWithinSettingsTransaction(settingsStore =>
            {
                RunGuarded(() => MutateSettings(settingsStore));
                base.DoTestSolution(fileSet);
            });
        }

        protected abstract void MutateSettings([NotNull] IContextBoundSettingsStore settingsStore);
    }
}