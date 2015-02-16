using JetBrains.Annotations;
using JetBrains.ReSharper.Psi.Tree;
using XmlDocInspections.Plugin.Highlighting;
#if RESHARPER8
using JetBrains.ReSharper.Daemon;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;
#endif

[assembly: RegisterConfigurableSeverity(
    MissingProtectedTypeXmlDocHighlighting.SeverityId,
    null,
    HighlightingGroupIds.CodeSmell,
    MissingProtectedTypeXmlDocHighlighting.Message,
    MissingProtectedTypeXmlDocHighlighting.Description,
    Severity.WARNING,
    solutionAnalysisRequired: false)]

namespace XmlDocInspections.Plugin.Highlighting
{
    [ConfigurableSeverityHighlighting(
        SeverityId,
        "CSHARP", // VB_SUPPORT
        OverlapResolve = OverlapResolveKind.WARNING,
        ToolTipFormatString = Message)]
    public class MissingProtectedTypeXmlDocHighlighting : XmlDocHighlightingBase
    {
        public const string SeverityId = "MissingProtectedTypeXmlDocHighlighting";

        public const string Message = "Missing XML Doc comment for types with protected visibility";

        public const string Description = Message;

        public MissingProtectedTypeXmlDocHighlighting([NotNull] IDeclaration declaration)
            : base(declaration, Message)
        {
        }
    }
}