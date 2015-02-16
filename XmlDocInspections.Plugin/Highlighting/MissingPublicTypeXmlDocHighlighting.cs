using JetBrains.Annotations;
using JetBrains.ReSharper.Psi.Tree;
using XmlDocInspections.Plugin.Highlighting;
#if RESHARPER8
using JetBrains.ReSharper.Daemon;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;
#endif

[assembly: RegisterConfigurableSeverity(
    MissingPublicTypeXmlDocHighlighting.SeverityId,
    null,
    HighlightingGroupIds.CodeSmell,
    MissingPublicTypeXmlDocHighlighting.Message,
    MissingPublicTypeXmlDocHighlighting.Description,
    Severity.WARNING,
    solutionAnalysisRequired: false)]

namespace XmlDocInspections.Plugin.Highlighting
{
    [ConfigurableSeverityHighlighting(
        SeverityId,
        "CSHARP", // VB_SUPPORT
        OverlapResolve = OverlapResolveKind.WARNING,
        ToolTipFormatString = Message)]
    public class MissingPublicTypeXmlDocHighlighting : XmlDocHighlightingBase
    {
        public const string SeverityId = "MissingPublicTypeXmlDocHighlighting";

        public const string Message = "Missing XML Doc comment for types with public visibility";

        public const string Description = Message;

        public MissingPublicTypeXmlDocHighlighting([NotNull] IDeclaration declaration)
            : base(declaration, Message)
        {
        }
    }
}