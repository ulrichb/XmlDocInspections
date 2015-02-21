using JetBrains.ReSharper.Psi;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin
{
    /// <summary>
    /// Utilities for <see cref="AccessibilityDomain.AccessibilityDomainType"/>.
    /// </summary>
    public static class AccessibilityUtilities
    {
        public static bool IsAccessibilityConfigured(AccessibilityDomain.AccessibilityDomainType accessibility, AccessibilitySettingFlags settingFlags)
        {
            switch (accessibility)
            {
                case AccessibilityDomain.AccessibilityDomainType.PUBLIC:
                    return settingFlags.HasFlag(AccessibilitySettingFlags.Public);
                case AccessibilityDomain.AccessibilityDomainType.INTERNAL:
                    return settingFlags.HasFlag(AccessibilitySettingFlags.Internal);
                case AccessibilityDomain.AccessibilityDomainType.PROTECTED_OR_INTERNAL:
                    return settingFlags.HasFlag(AccessibilitySettingFlags.ProtectedOrInternal);
                case AccessibilityDomain.AccessibilityDomainType.PROTECTED:
                    return settingFlags.HasFlag(AccessibilitySettingFlags.Protected);
                case AccessibilityDomain.AccessibilityDomainType.PRIVATE:
                    return settingFlags.HasFlag(AccessibilitySettingFlags.Private);
            }

            return false;
        }

        public static string FormatAccessibilityDomainType(AccessibilityDomain.AccessibilityDomainType accessibility)
        {
            switch (accessibility)
            {
                case AccessibilityDomain.AccessibilityDomainType.PUBLIC:
                    return "public";
                case AccessibilityDomain.AccessibilityDomainType.INTERNAL:
                    return "internal";
                case AccessibilityDomain.AccessibilityDomainType.PROTECTED_OR_INTERNAL:
                    return "protected internal";
                case AccessibilityDomain.AccessibilityDomainType.PROTECTED:
                    return "protected";
                case AccessibilityDomain.AccessibilityDomainType.PRIVATE:
                    return "private";
                default:
                    return "<unknown>";
            }
        }
    }
}