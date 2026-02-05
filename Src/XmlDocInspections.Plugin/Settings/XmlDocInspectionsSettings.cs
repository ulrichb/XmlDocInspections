using System.Diagnostics.CodeAnalysis;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Resources.Settings;

namespace XmlDocInspections.Plugin.Settings
{
    /// <summary>
    /// Settings class for Xml Doc inspections.
    /// </summary>
    [SettingsKey(typeof(CodeInspectionSettings), "Xml Doc Inspections")]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class XmlDocInspectionsSettings
    {
        private const AccessibilitySettingFlags DefaultAccessibilityFlags =
            AccessibilitySettingFlags.Public | AccessibilitySettingFlags.Protected | AccessibilitySettingFlags.ProtectedOrInternal;


        [SettingsEntry(DefaultAccessibilityFlags, "Type accessibility")]
        public readonly AccessibilitySettingFlags TypeAccessibility;

        [SettingsEntry(DefaultAccessibilityFlags, "Type member accessibility")]
        public readonly AccessibilitySettingFlags TypeMemberAccessibility;

        [SettingsEntry("JetBrains.Annotations.PublicAPIAttribute", "Include types/members with attributes (comma separated full names)")]
        public readonly string IncludeAttributeFullNames;

        [SettingsEntry(false, "Exclude extension blocks")]
        public readonly bool ExcludeExtensionBlocks;

        [SettingsEntry(false, "Exclude constructors")]
        public readonly bool ExcludeConstructors;

        [SettingsEntry(false, "Exclude members which override super/base members")]
        public readonly bool ExcludeMembersOverridingSuperMember;

        [SettingsEntry("Tests$", "Project exclusion regex")]
        public readonly string ProjectExclusionRegex;
    }
}
