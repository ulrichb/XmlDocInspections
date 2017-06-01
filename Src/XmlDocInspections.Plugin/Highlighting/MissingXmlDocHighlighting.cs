using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.Tree;
using ReSharperExtensionsShared.Highlighting;
using XmlDocInspections.Plugin.Highlighting;
using XmlDocInspections.Plugin.Settings;
using static JetBrains.ReSharper.Psi.AccessibilityDomain;

[assembly: RegisterConfigurableSeverity(
    MissingXmlDocHighlighting.SeverityId,
    CompoundItemName: null,
    Group: HighlightingGroupIds.CodeSmell,
    Title: MissingXmlDocHighlighting.Title,
    Description: MissingXmlDocHighlighting.Description,
    DefaultSeverity: Severity.WARNING)]

namespace XmlDocInspections.Plugin.Highlighting
{
    /// <summary>
    /// Xml Doc highlighting for types / type members with specific accessibility.
    /// </summary>
    [ConfigurableSeverityHighlighting(
        SeverityId,
        "CSHARP",
        OverlapResolve = OverlapResolveKind.NONE,
        ToolTipFormatString = Message)]
    public class MissingXmlDocHighlighting : SimpleTreeNodeHighlightingBase<IDeclaration>
    {
        public const string SeverityId = "MissingXmlDoc";
        public const string Title = "Missing XML Doc comment for type / type member";
        private const string Message = "Missing XML Doc comment for {0} {1}";

        public const string Description =
            "Missing XML Doc comment for type / type member. " +
            "See the '" + XmlDocInspectionsOptionsPage.PageTitle + "' options page for further configuration settings.";

        public MissingXmlDocHighlighting(bool isTypeMember, AccessibilityDomainType accessibilityDomainType, IDeclaration declaration)
            : base(declaration, FormatMessage(isTypeMember, accessibilityDomainType))
        {
        }

        private static string FormatMessage(bool isTypeMember, AccessibilityDomainType accessibilityDomainType)
        {
            return string.Format(
                Message,
                AccessibilityUtility.FormatAccessibilityDomainType(accessibilityDomainType),
                isTypeMember ? "type member" : "type");
        }

        public override DocumentRange CalculateRange()
        {
            return TreeNode.GetNameDocumentRange();
        }
    }
}
