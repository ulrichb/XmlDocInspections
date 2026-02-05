using JetBrains.Application.Settings;
using NUnit.Framework;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin.Tests.Integrative.Highlighting
{
    [TestFixture]
    public class EnabledExcludeExtensionBlocksTests : MissingXmlDocHighlightingDirectoryTestsBase
    {
        protected override void MutateSettings(IContextBoundSettingsStore settingsStore)
        {
            EnableAllXmlDocOptions(settingsStore);
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.ExcludeExtensionBlocks, true);
        }

        [Test]
        public void ExtensionBlocks() => DoNamedTest();
    }
}
