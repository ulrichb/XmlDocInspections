using System;

namespace XmlDocInspections.Plugin.Settings
{
    /// <summary>
    /// Flags enum for the accessibility settings.
    /// </summary>
    [Flags]
    public enum AccessibilitySettingFlags
    {
        None = 0,

        Public = 0x1,
        Internal = 0x2,
        ProtectedOrInternal = 0x4,
        Protected = 0x8,
        Private = 0x10,

        All = Public | Internal | ProtectedOrInternal | Protected | Private
    }
}