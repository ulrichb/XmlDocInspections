using JetBrains.ReSharper.Psi;
using XmlDocInspections.Plugin.Settings;
using static JetBrains.ReSharper.Psi.AccessibilityDomain;

namespace XmlDocInspections.Plugin
{
    /// <summary>
    /// Utilities for <see cref="AccessibilityDomain.AccessibilityDomainType"/>.
    /// </summary>
    public static class AccessibilityUtility
    {
        public static bool IsAccessibilityConfigured(AccessibilityDomainType accessibility, AccessibilitySettingFlags settingFlags)
        {
            switch (accessibility)
            {
                case AccessibilityDomainType.PUBLIC:
                    return settingFlags.HasFlag(AccessibilitySettingFlags.Public);
                case AccessibilityDomainType.INTERNAL:
                    return settingFlags.HasFlag(AccessibilitySettingFlags.Internal);
                case AccessibilityDomainType.PROTECTED_OR_INTERNAL:
                    return settingFlags.HasFlag(AccessibilitySettingFlags.ProtectedOrInternal);
                // PROTECTED_AND_INTERNAL is used e.g. for protected members/types *within* an internal class.
                case AccessibilityDomainType.PROTECTED_AND_INTERNAL:
                    return settingFlags.HasFlag(AccessibilitySettingFlags.Protected) && settingFlags.HasFlag(AccessibilitySettingFlags.Internal);
                case AccessibilityDomainType.PROTECTED:
                    return settingFlags.HasFlag(AccessibilitySettingFlags.Protected);
                case AccessibilityDomainType.PRIVATE:
                    return settingFlags.HasFlag(AccessibilitySettingFlags.Private);
            }

            return false;
        }

        public static string FormatAccessibilityDomainType(AccessibilityDomainType accessibility)
        {
            switch (accessibility)
            {
                case AccessibilityDomainType.PUBLIC:
                    return "public";
                case AccessibilityDomainType.INTERNAL:
                    return "internal";
                case AccessibilityDomainType.PROTECTED_OR_INTERNAL:
                    return "protected internal";
                case AccessibilityDomainType.PROTECTED_AND_INTERNAL:
                case AccessibilityDomainType.PROTECTED:
                    return "protected";
                case AccessibilityDomainType.PRIVATE:
                    return "private";
                default:
                    return "<unknown>";
            }
        }
    }
}
