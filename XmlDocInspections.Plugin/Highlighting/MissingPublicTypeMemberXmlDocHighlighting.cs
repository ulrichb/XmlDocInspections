using JetBrains.Annotations;
using JetBrains.ReSharper.Psi.Tree;
using XmlDocInspections.Plugin.Highlighting;
#if RESHARPER8
using JetBrains.ReSharper.Daemon;
#else
using JetBrains.ReSharper.Feature.Services.Daemon;

#endif

[assembly: RegisterConfigurableSeverity(
    MissingPublicTypeMemberXmlDocHighlighting.SeverityId,
    null,
    HighlightingGroupIds.CodeSmell,
    MissingPublicTypeMemberXmlDocHighlighting.Message,
    MissingPublicTypeMemberXmlDocHighlighting.Description,
    Severity.WARNING,
    solutionAnalysisRequired: false)]

namespace XmlDocInspections.Plugin.Highlighting
{
    [ConfigurableSeverityHighlighting(
        SeverityId,
        "CSHARP", // VB_SUPPORT
        OverlapResolve = OverlapResolveKind.WARNING,
        ToolTipFormatString = Message)]
    public class MissingPublicTypeMemberXmlDocHighlighting : XmlDocHighlightingBase
    {
        public const string SeverityId = "MissingPublicTypeMemberXmlDocHighlighting";

        public const string Message = "Missing XML Doc comment for type members with public visibility";

        public const string Description = Message;

        public MissingPublicTypeMemberXmlDocHighlighting([NotNull] IDeclaration declaration)
            : base(declaration, Message)
        {
        }
    }
}