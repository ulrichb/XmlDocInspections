using JetBrains.Application.Settings;
using NUnit.Framework;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin.Tests.Integrative
{
    [TestFixture]
    public class DisabledRequireDocsOnOverridingMemberTests : MissingXmlDocHighlightingTestsBase
    {
        protected override void MutateSettings(IContextBoundSettingsStore settingsStore)
        {
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.TypeAccessibility, AccessibilitySettingFlags.All);
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.TypeMemberAccessibility, AccessibilitySettingFlags.All);
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.RequireDocsOnOverridingMember, false);
        }

        [Test]
        public void TestInheritance()
        {
            DoNamedTest2();
        }
    }
}