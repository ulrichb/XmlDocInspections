using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JetBrains.Application.Settings;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Feature.Services.Daemon.OptionPages;
using JetBrains.UI.Options;
using JetBrains.UI.Options.OptionsDialog2.SimpleOptions;
using JetBrains.UI.Resources;

namespace XmlDocInspections.Plugin.Settings
{
    /// <summary>
    /// An options page for XML Doc inspections.
    /// </summary>
    [ExcludeFromCodeCoverage /* options page user interface is tested manually */]
    [OptionsPage(CPageId, PageTitle, typeof(CommonThemedIcons.Bulb), ParentId = CodeInspectionPage.PID)]
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

            AddText("Inspections are enabled for the following code elements. " +
                    "Note that the severity level can be configured on the \"Inspection Severity\" page.");

            AddHeader("Types");
            AddAccessibilityBoolOption((XmlDocInspectionsSettings s) => s.TypeAccessibility);

            AddHeader("Type members");
            AddAccessibilityBoolOption((XmlDocInspectionsSettings s) => s.TypeMemberAccessibility);

            AddBoolOption(
                (XmlDocInspectionsSettings s) => s.RequireDocsOnOverridingMember,
                "Members which override base members (i.e. do not exclude override-members)");

            AddText("");

            AddStringOption((XmlDocInspectionsSettings s) => s.ProjectExclusionRegex, "Project exclusion regex: ");

            var cacheInfoText =
                "\nWarning: After changing these settings, " +
                "cleaning the solution cache (see \"General\" options page) " +
                "is necessary to update already analyzed code.";
            AddText(cacheInfoText);

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
