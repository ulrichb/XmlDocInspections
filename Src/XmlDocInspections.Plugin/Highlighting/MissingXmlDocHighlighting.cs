using JetBrains.Diagnostics;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using ReSharperExtensionsShared.Highlighting;
using XmlDocInspections.Plugin.Highlighting;
using XmlDocInspections.Plugin.Settings;

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
        CSharpLanguage.Name,
        OverlapResolve = OverlapResolveKind.NONE,
        ToolTipFormatString = Message)]
    public class MissingXmlDocHighlighting : SimpleTreeNodeHighlightingBase<ICSharpDeclaration>
    {
        public const string SeverityId = "MissingXmlDoc";
        public const string Title = "Missing XML Doc comment for type / type member";
        private const string Message = "Missing XML Doc comment for {0} {1}";

        public const string Description =
            "Missing XML Doc comment for type / type member. " +
            "See the '" + XmlDocInspectionsOptionsPage.PageTitle + "' options page for further configuration settings.";

        public MissingXmlDocHighlighting(ICSharpTypeMemberDeclaration declaration)
            : base(declaration, FormatMessage(declaration.DeclaredElement.NotNull()))
        {
        }

        private static string FormatMessage(ITypeMember typeMember)
        {
            return string.Format(
                Message,
                AccessibilityUtility.FormatAccessibilityDomainType(typeMember.AccessibilityDomain.DomainType),
                typeMember is ITypeElement ? "type" : "type member");
        }

        public override DocumentRange CalculateRange() => HighlightingNode.GetNameDocumentRange();
    }
}
