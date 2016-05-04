using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq.Expressions;
using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Feature.Services.Daemon.OptionPages;
using JetBrains.UI.Options;
using JetBrains.UI.Options.OptionsDialog2.SimpleOptions;
using JetBrains.UI.Resources;
using JetBrains.UI.RichText;

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

            AddRichText(new RichText("Enable inspection for the following ").Append("types", new TextStyle(FontStyle.Bold)));
            AddAccessibilityBoolOption((XmlDocInspectionsSettings s) => s.TypeAccessibility);

            AddRichText(new RichText("Enable inspection for the following ").Append("type members", new TextStyle(FontStyle.Bold)));
            AddAccessibilityBoolOption((XmlDocInspectionsSettings s) => s.TypeMemberAccessibility);

            AddText("Note that the severity level can be configured on the \"Inspection Severity\" page.");

            AddStringOption((XmlDocInspectionsSettings s) => s.ProjectExclusionRegex, "Project exclusion regex: ");

            var cacheInfoText =
                "\nWarning: After changing these settings, " +
                "cleaning the solution cache (see \"General\" options page) " +
                "is necessary to update already analyzed code.";
            AddText(cacheInfoText);

            FinishPage();
        }

        private void AddAccessibilityBoolOption<T>([NotNull] Expression<Func<T, AccessibilitySettingFlags>> settingsExpression)
        {
            var flagsProperty = new Property<AccessibilitySettingFlags>(_lifetime, "AccessibilitySettingFlags");
            _settings.SetBinding(_lifetime, settingsExpression, flagsProperty);

            AddAccessibilityBoolOption(flagsProperty, "public", AccessibilitySettingFlags.Public);
            AddAccessibilityBoolOption(flagsProperty, "internal", AccessibilitySettingFlags.Internal);
            AddAccessibilityBoolOption(flagsProperty, "protected internal", AccessibilitySettingFlags.ProtectedOrInternal);
            AddAccessibilityBoolOption(flagsProperty, "protected", AccessibilitySettingFlags.Protected);
            AddAccessibilityBoolOption(flagsProperty, "private", AccessibilitySettingFlags.Private);
        }

        private void AddAccessibilityBoolOption(
            [NotNull] IProperty<AccessibilitySettingFlags> flagsProperty,
            [NotNull] string text,
            AccessibilitySettingFlags accessibilitySettingFlag)
        {
            var optionBoolProperty = new Property<bool>(_lifetime, text);
            BindBoolPropertyToAccessibilitySettingFlag(optionBoolProperty, flagsProperty, accessibilitySettingFlag);

            var boolOption = AddBoolOption(optionBoolProperty, text);
            SetIndent(boolOption, 1);
        }

        private void BindBoolPropertyToAccessibilitySettingFlag(
            [NotNull] IProperty<bool> property,
            [NotNull] IProperty<AccessibilitySettingFlags> flagsProperty,
            AccessibilitySettingFlags flagToBindTo)
        {
            property.Value = flagsProperty.Value.HasFlag(flagToBindTo);

            property.Change.Advise_HasNew(
                _lifetime,
                x => flagsProperty.Value = x.New ? flagsProperty.Value | flagToBindTo : flagsProperty.Value & ~flagToBindTo);
        }
    }
}