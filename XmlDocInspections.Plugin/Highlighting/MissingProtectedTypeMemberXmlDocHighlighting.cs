using JetBrains.Annotations;
using JetBrains.ReSharper.Psi.Tree;
using XmlDocInspections.Plugin.Highlighting;
#if RESHARPER8
using JetBrains.ReSharper.Daemon;
#else
using JetBrains.ReSharper.Feature.Services.Daemon;

#endif

[assembly: RegisterConfigurableSeverity(
    MissingProtectedTypeMemberXmlDocHighlighting.SeverityId,
    null,
    HighlightingGroupIds.CodeSmell,
    MissingProtectedTypeMemberXmlDocHighlighting.Message,
    MissingProtectedTypeMemberXmlDocHighlighting.Description,
    Severity.WARNING,
    solutionAnalysisRequired: false)]

namespace XmlDocInspections.Plugin.Highlighting
{
    [ConfigurableSeverityHighlighting(
        SeverityId,
        "CSHARP", // VB_SUPPORT
        OverlapResolve = OverlapResolveKind.WARNING,
        ToolTipFormatString = Message)]
    public class MissingProtectedTypeMemberXmlDocHighlighting : XmlDocHighlightingBase
    {
        public const string SeverityId = "MissingProtectedTypeMemberXmlDocHighlighting";

        public const string Message = "Missing XML Doc comment for type members with protected visibility";

        public const string Description = Message;

        public MissingProtectedTypeMemberXmlDocHighlighting([NotNull] IDeclaration declaration)
            : base(declaration, Message)
        {
        }
    }
}