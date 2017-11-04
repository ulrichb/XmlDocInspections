using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JetBrains.Application.Settings;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Feature.Services.Daemon.OptionPages;
#if RS20171
using JetBrains.UI.Options;
using JetBrains.UI.Options.OptionsDialog2.SimpleOptions;
using JetBrains.Ide.Resources;
#else
using JetBrains.Application.UI.Options;
using JetBrains.Application.UI.Options.OptionsDialog;

#endif

namespace XmlDocInspections.Plugin.Settings
{
    /// <summary>
    /// An options page for XML Doc inspections.
    /// </summary>
    [ExcludeFromCodeCoverage /* options page user interface is tested manually */]
    [OptionsPage(CPageId, PageTitle,
#if RS20171
        typeof(IdeThemedIcons.XmlDocument),
#else
        typeof(XmlDocInspectionsIcons.Xml16),
#endif
        ParentId = CodeInspectionPage.PID)]
    public class XmlDocInspectionsOptionsPage : SimpleOptionsPage
    {
        private readonly Lifetime _lifetime;
        private readonly OptionsSettingsSmartContext _settings;

        public const string PageTitle = "XML Doc Inspections";
        private const string CPageId = "XmlDocInspectionsOptions";

        public XmlDocInspectionsOptionsPage(Lifetime lifetime, OptionsSettingsSmartContext settings)
            : base(lifetime, settings)
        {
            _lifetime = lifetime;
            _settings = settings;

            AddText("The following rules are applied when warning about missing XML Doc comments. " +
                    "Note that the severity level for the warning can be configured on the \"Inspection Severity\" page.");

            AddHeader("Inclusion rules");

            AddText("Types");
            AddAccessibilityBoolOption((XmlDocInspectionsSettings s) => s.TypeAccessibility);

            AddText("Type members");
            AddAccessibilityBoolOption((XmlDocInspectionsSettings s) => s.TypeMemberAccessibility);

            AddText();
            AddStringOption((XmlDocInspectionsSettings s) => s.IncludeAttributeFullNames, "Types/members with attributes:\t");

            AddHeader("Exclusion rules");

            AddBoolOption((XmlDocInspectionsSettings s) => s.ExcludeConstructors, "Exclude constructors");

            AddBoolOption(
                (XmlDocInspectionsSettings s) => s.ExcludeMembersOverridingSuperMember,
                "Exclude members which override base members");

            AddText("");

            AddStringOption((XmlDocInspectionsSettings s) => s.ProjectExclusionRegex, "Project exclusion regex:\t");

            AddText(
                "\n" +
                "Warning: After changing these settings, " +
                "cleaning the solution cache (see \"General\" options page) " +
                "is necessary to update already analyzed code.");

            FinishPage();
        }

        private void AddAccessibilityBoolOption<T>(Expression<Func<T, AccessibilitySettingFlags>> settingsExpression)
        {
            //AddText("Show inspection accessibility:");
            var flagsProperty = new Property<AccessibilitySettingFlags>(_lifetime, "AccessibilitySettingFlags");
            _settings.SetBinding(_lifetime, settingsExpression, flagsProperty);

            AddAccessibilityBoolOption(flagsProperty, "public", AccessibilitySettingFlags.Public);
            AddAccessibilityBoolOption(flagsProperty, "internal", AccessibilitySettingFlags.Internal);
            AddAccessibilityBoolOption(flagsProperty, "protected internal", AccessibilitySettingFlags.ProtectedOrInternal);
            AddAccessibilityBoolOption(flagsProperty, "protected", AccessibilitySettingFlags.Protected);
            AddAccessibilityBoolOption(flagsProperty, "private", AccessibilitySettingFlags.Private);
        }

        private void AddAccessibilityBoolOption(
            IProperty<AccessibilitySettingFlags> flagsProperty,
            string text,
            AccessibilitySettingFlags accessibilitySettingFlag)
        {
            var optionBoolProperty = new Property<bool>(_lifetime, text);
            BindBoolPropertyToAccessibilitySettingFlag(optionBoolProperty, flagsProperty, accessibilitySettingFlag);

            var boolOption = AddBoolOption(optionBoolProperty, text);
            SetIndent(boolOption, 1);
        }

        private void BindBoolPropertyToAccessibilitySettingFlag(
            IProperty<bool> property,
            IProperty<AccessibilitySettingFlags> flagsProperty,
            AccessibilitySettingFlags flagToBindTo)
        {
            property.Value = flagsProperty.Value.HasFlag(flagToBindTo);

            property.Change.Advise_HasNew(
                _lifetime,
                x => flagsProperty.Value = x.New ? flagsProperty.Value | flagToBindTo : flagsProperty.Value & ~flagToBindTo);
        }
    }
}
