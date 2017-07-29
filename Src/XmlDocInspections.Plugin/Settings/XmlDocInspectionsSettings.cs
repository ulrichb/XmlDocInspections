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

        [SettingsEntry(DefaultAccessibilityFlags, "Type accessibility")]
        public AccessibilitySettingFlags TypeAccessibility;

        [SettingsEntry(DefaultAccessibilityFlags, "Type member accessibility")]
        public AccessibilitySettingFlags TypeMemberAccessibility;

        [SettingsEntry(false, "Exclude constructors")]
        public bool ExcludeConstructors;

        [SettingsEntry(false, "Exclude members which override super/base members")]
        public bool ExcludeMembersOverridingSuperMember;

        [SettingsEntry("Tests$", "Project exclusion regex")]
        public string ProjectExclusionRegex;
    }
}
