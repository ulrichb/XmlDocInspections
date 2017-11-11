using JetBrains.Application.Settings;
using NUnit.Framework;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin.Tests.Integrative.Highlighting
{
    [TestFixture]
    public class EnabledExcludeConstructorsTests : MissingXmlDocHighlightingDirectoryTestsBase
    {
        protected override void MutateSettings(IContextBoundSettingsStore settingsStore)
        {
            EnableAllXmlDocOptions(settingsStore);
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.ExcludeConstructors, true);
        }

        [Test]
        public void TestClassesAndMembersWithoutDocs() => DoNamedTest2("IToBeExplicitlyImplementedInterface.cs");
    }
}
