using JetBrains.Application.Settings;
#if RESHARPER8
using JetBrains.ReSharper.Settings;

#else
using JetBrains.ReSharper.Resources.Settings;

#endif

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

        [SettingsEntry("Tests$", "ExclusionRegex")]
        public string ExclusionRegex { get; set; }

        [SettingsEntry(DefaultAccessibilityFlags, "TypeAccessibility")]
        public AccessibilitySettingFlags TypeAccessibility;

        [SettingsEntry(DefaultAccessibilityFlags, "TypeMemberAccessibility")]
        public AccessibilitySettingFlags TypeMemberAccessibility;
    }
}