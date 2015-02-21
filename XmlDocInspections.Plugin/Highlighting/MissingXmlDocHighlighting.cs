using XmlDocInspections.Plugin.Settings;
using JetBrains.ReSharper.Psi;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi.Tree;
using XmlDocInspections.Plugin.Highlighting;
#if RESHARPER8
using JetBrains.ReSharper.Daemon;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;

#endif

[assembly: RegisterConfigurableSeverity(
    MissingXmlDocHighlighting.SeverityId,
    null,
    HighlightingGroupIds.CodeSmell,
    MissingXmlDocHighlighting.Title,
    MissingXmlDocHighlighting.Description,
    Severity.WARNING,
    solutionAnalysisRequired: false)]

namespace XmlDocInspections.Plugin.Highlighting
{
    /// <summary>
    /// Xml Doc highlighting for types / type members with specific accessibility.
    /// </summary>
    [ConfigurableSeverityHighlighting(
        SeverityId,
        "CSHARP", // VB_SUPPORT
        OverlapResolve = OverlapResolveKind.NONE,
        ToolTipFormatString = Message)]
    public class MissingXmlDocHighlighting : XmlDocHighlightingBase
    {
        public const string SeverityId = "MissingXmlDoc";

        private const string Message = "Missing XML Doc comment for {0} {1}";

        public const string Title = "Missing XML Doc comment for type / type member";

        public const string Description =
            "Missing XML Doc comment for type / type member. " +
            "See the '" + XmlDocInspectionsOptionsPage.PageTitle + "' options page for further configuration settings.";

        public MissingXmlDocHighlighting(bool isTypeMember, AccessibilityDomain.AccessibilityDomainType accessibilityDomainType,
            IDeclaration declaration)
            : base(declaration, FormatMessage(isTypeMember, accessibilityDomainType))
        {
        }

        private static string FormatMessage(bool isTypeMember, AccessibilityDomain.AccessibilityDomainType accessibilityDomainType)
        {
            return string.Format(
                Message,
                AccessibilityUtilities.FormatAccessibilityDomainType(accessibilityDomainType),
                isTypeMember ? "type member" : "type");
        }
    }
}