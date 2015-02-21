using JetBrains.Annotations;
using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;
using JetBrains.Application.Settings;
using JetBrains.DataFlow;
using JetBrains.UI.Application;
using JetBrains.UI.Options;
using JetBrains.UI.Options.Helpers;
using JetBrains.UI.Resources;
#if RESHARPER8
using JetBrains.ReSharper.Features.Environment.Options.Inspections;

#else
using JetBrains.ReSharper.Feature.Services.Daemon.OptionPages;

#endif

namespace XmlDocInspections.Plugin.Settings
{
    /// <summary>
    /// An options page for XML Doc inspections.
    /// </summary>
    [OptionsPage(
        CPageId,
        PageTitle,
        typeof(CommonThemedIcons.Bulb),
        ParentId = CodeInspectionPage.PID)]
    public class XmlDocInspectionsOptionsPage : AStackPanelOptionsPage // REVIEW: R#9 ISearchablePage ??
    {
        private readonly Lifetime _lifetime;
        private readonly IUIApplication _environment;
        private readonly OptionsSettingsSmartContext _settings;

        public const string PageTitle = "Xml Doc Inspections";
        private const string CPageId = "XmlDocInspectionsOptions";

        public XmlDocInspectionsOptionsPage(Lifetime lifetime, IUIApplication environment, OptionsSettingsSmartContext settings)
            : base(lifetime, environment, CPageId)
        {
            _lifetime = lifetime;
            _environment = environment;
            _settings = settings;
            InitControls();
        }

        private void InitControls()
        {
            Controls.Add(CreateExclusionEdit((XmlDocInspectionsSettings s) => s.ExclusionRegex));

            Controls.Add(CreateAccessibilityCheckBoxes("Types with accessibility", (XmlDocInspectionsSettings s) => s.TypeAccessibility));
            Controls.Add(CreateAccessibilityCheckBoxes("Type members with accessibility", (XmlDocInspectionsSettings s) => s.TypeMemberAccessibility));
        }

        private Control CreateExclusionEdit<T>([NotNull] Expression<Func<T, string>> settingsExpression)
        {
            var tableLayoutPanel = new TableLayoutPanel { AutoSize = true };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // left column

            var label = new Controls.Label("Exclusion regex:");
            tableLayoutPanel.Controls.Add(label, 0, 0);

            var editBox = new Controls.EditBox();
            editBox.Size = new Size(350, editBox.Size.Height);
            tableLayoutPanel.Controls.Add(editBox, 1, 0);

            _settings.SetBinding(_lifetime, settingsExpression, editBox.Text);

            return tableLayoutPanel;
        }

        private Control CreateAccessibilityCheckBoxes<T>(
            [NotNull] string text,
            [NotNull] Expression<Func<T, AccessibilitySettingFlags>> settingsExpression)
        {
            var result = new GroupBox { Text = text, AutoSize = true, MinimumSize = new Size(250, 0) };

            var panel = new Controls.VertStackPanel(_environment) { Dock = DockStyle.Fill };
            result.Controls.Add(panel);

            var accessSettingFlags = new Property<AccessibilitySettingFlags>(_lifetime, "AccessibilitySettingFlags");
            _settings.SetBinding(_lifetime, settingsExpression, accessSettingFlags);

            var accessibilityEntries = new[]
            {
                new {Text = "public", AccessibilitySettingFlag = AccessibilitySettingFlags.Public},
                new {Text = "internal", AccessibilitySettingFlag = AccessibilitySettingFlags.Internal},
                new {Text = "protected internal", AccessibilitySettingFlag = AccessibilitySettingFlags.ProtectedOrInternal},
                new {Text = "protected", AccessibilitySettingFlag = AccessibilitySettingFlags.Protected},
                new {Text = "private", AccessibilitySettingFlag = AccessibilitySettingFlags.Private}
            };

            foreach (var entry in accessibilityEntries)
            {
                var checkBox = new Controls.CheckBox { Text = entry.Text, AutoSize = true, Margin = new Padding(3, 1, 3, 1) };
                panel.Controls.Add(checkBox);

                BindToAccessibilitySettingFlag(checkBox, accessSettingFlags, entry.AccessibilitySettingFlag);
            }

            return result;
        }

        private void BindToAccessibilitySettingFlag(
            [NotNull] Controls.CheckBox checkBox,
            [NotNull] IProperty<AccessibilitySettingFlags> flagsProperty,
            AccessibilitySettingFlags flagToBindTo)
        {
            checkBox.Checked.Value = flagsProperty.Value.HasFlag(flagToBindTo);

            checkBox.Checked.Change.Advise_HasNew(
                _lifetime,
                x => flagsProperty.Value = x.New ? flagsProperty.Value | flagToBindTo : flagsProperty.Value & ~flagToBindTo);
        }
    }
}