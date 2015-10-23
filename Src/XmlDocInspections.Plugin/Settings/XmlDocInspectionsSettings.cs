using JetBrains.Application.Settings;
using JetBrains.ReSharper.Resources.Settings;

namespace XmlDocInspections.Plugin.Settings
{
    /// <summary>
    /// Settings class for Xml Doc inspections.
    /// </summary>
    [SettingsKey(typeof (CodeInspectionSettings), "Xml Doc Inspections")]
    public class XmlDocInspectionsSettings
    {
        private const AccessibilitySettingFlags DefaultAccessibilityFlags =
            AccessibilitySettingFlags.Public | AccessibilitySettingFlags.Protected | AccessibilitySettingFlags.ProtectedOrInternal;

        [SettingsEntry("Tests$", "ProjectExclusionRegex")]
        public string ProjectExclusionRegex;

        [SettingsEntry(DefaultAccessibilityFlags, "TypeAccessibility")]
        public AccessibilitySettingFlags TypeAccessibility;

        [SettingsEntry(DefaultAccessibilityFlags, "TypeMemberAccessibility")]
        public AccessibilitySettingFlags TypeMemberAccessibility;
    }
}