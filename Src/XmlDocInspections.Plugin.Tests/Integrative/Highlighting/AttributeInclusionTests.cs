using System.IO;
using JetBrains.Application.Settings;
using NUnit.Framework;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin.Tests.Integrative.Highlighting
{
    [TestFixture]
    public class AttributeInclusionTests : MissingXmlDocHighlightingDirectoryTestsBase
    {
        protected override void MutateSettings(IContextBoundSettingsStore settingsStore)
        {
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.TypeAccessibility, AccessibilitySettingFlags.None);
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.TypeMemberAccessibility, AccessibilitySettingFlags.None);

            var attribute = settingsStore.GetValue((XmlDocInspectionsSettings s) => s.IncludeAttributeFullNames);
            Assert.IsTrue(attribute == "JetBrains.Annotations.PublicAPIAttribute"); // Fixate default value

            // Prove that the setting can contain spaces and duplicates:
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.IncludeAttributeFullNames, $"{attribute}  , {attribute}  ");
        }

        [Test]
        public void AttributeInclusion() => DoNamedTest();

        public class WithExcludeMembersOverridingSuperMember : AttributeInclusionTests
        {
            protected override void MutateSettings(IContextBoundSettingsStore settingsStore)
            {
                base.MutateSettings(settingsStore);
                settingsStore.SetValue((XmlDocInspectionsSettings s) => s.ExcludeMembersOverridingSuperMember, true);
            }

            protected override string GetRelativeGoldFilePath(string fileName) =>
                Path.Combine(nameof(AttributeInclusionTests), $"{fileName}_{nameof(WithExcludeMembersOverridingSuperMember)}");
        }
    }
}
