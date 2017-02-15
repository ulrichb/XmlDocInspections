using JetBrains.Application;
using JetBrains.ReSharper.Daemon.CSharp.Errors;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Intentions.CSharp.QuickFixes;

#if RESHARPER20161
using JetBrains.DataFlow;
using JetBrains.Util;
#endif

namespace XmlDocInspections.Plugin.Highlighting
{
    [ShellComponent]
    internal class XmlDocInspectionsQuickFixRegistrarComponent
    {
        public XmlDocInspectionsQuickFixRegistrarComponent(
#if RESHARPER20161
            Lifetime lifetime, 
#endif
            IQuickFixes table)
        {
            table.RegisterQuickFix<MissingXmlDocHighlighting>(
#if RESHARPER20161
                lifetime,
#else
                null,
#endif
                h => new AddDocCommentFix(Wrap(h)), typeof(AddDocCommentFix)
#if RESHARPER20161
                , null, BeforeOrAfter.Before
#endif
            );
        }

        private static PublicOrProtectedMemberNotDocumentedWarning Wrap(MissingXmlDocHighlighting highlighting)
        {
            // Wrap the highlighting because there is no generic c'tor overload in AddDocCommentFix. Note that the QuickFix is only
            // interested in the warning's declaration.
            return new PublicOrProtectedMemberNotDocumentedWarning(highlighting.TreeNode);
        }
    }
}
