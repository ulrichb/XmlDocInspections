using JetBrains.Application.Settings;
using NUnit.Framework;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin.Tests.Integrative
{
    [TestFixture]
    public class DisabledRequireDocsOnConstructorsTests : MissingXmlDocHighlightingTestsBase
    {
        protected override void MutateSettings(IContextBoundSettingsStore settingsStore)
        {
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.TypeAccessibility, AccessibilitySettingFlags.All);
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.TypeMemberAccessibility, AccessibilitySettingFlags.All);
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.RequireDocsOnConstructors, false);
        }

        [Test]
        public void TestClassesAndMembersWithoutDocs() => DoNamedTest2();
    }
}
