using JetBrains.Application.Settings;
using JetBrains.ReSharper.Resources.Settings;

namespace XmlDocInspections.Plugin.Settings
{
    /// <summary>
    /// Settings class for Xml Doc inspections.
    /// </summary>
    [SettingsKey(typeof(CodeInspectionSettings), "Xml Doc Inspections")]
    public class XmlDocInspectionsSettings
    {
        private const AccessibilitySettingFlags DefaultAccessibilityFlags =
            AccessibilitySettingFlags.Public | AccessibilitySettingFlags.Protected | AccessibilitySettingFlags.ProtectedOrInternal;

        [SettingsEntry("Tests$", "Project exclusion regex")]
        public string ProjectExclusionRegex;

        [SettingsEntry(DefaultAccessibilityFlags, "Type accessibility")]
        public AccessibilitySettingFlags TypeAccessibility;

        [SettingsEntry(DefaultAccessibilityFlags, "Type member accessibility")]
        public AccessibilitySettingFlags TypeMemberAccessibility;

        [SettingsEntry(true, "Require XML Docs on constructors")]
        public bool RequireDocsOnConstructors;

        [SettingsEntry(true, "Require XML Docs on overriding members")]
        public bool RequireDocsOnOverridingMember;
    }
}
