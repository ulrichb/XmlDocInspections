using JetBrains.Application;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Intentions.CSharp.QuickFixes;
using JetBrains.ReSharper.Psi.CSharp.Tree;

#if RESHARPER20171
using JetBrains.ReSharper.Daemon.CSharp.Errors;

#endif

namespace XmlDocInspections.Plugin.Highlighting
{
    [ShellComponent]
    internal class XmlDocInspectionsQuickFixRegistrarComponent
    {
        public XmlDocInspectionsQuickFixRegistrarComponent(IQuickFixes table)
        {
            table.RegisterQuickFix<MissingXmlDocHighlighting>(null, h => CreateAddDocCommentFix(h.TreeNode), typeof(AddDocCommentFix));
        }

        private static AddDocCommentFix CreateAddDocCommentFix(ICSharpDeclaration declaration)
        {
#if RESHARPER20171
            PublicOrProtectedMemberNotDocumentedWarning Wrap()
            {
                // Wrap the highlighting because there is no generic c'tor overload in AddDocCommentFix. Note that the QuickFix is only
                // interested in the warning's declaration.
                return new PublicOrProtectedMemberNotDocumentedWarning(declaration);
            }
            return new AddDocCommentFix(Wrap());
#else
            return new AddDocCommentFix(declaration);
#endif
        }
    }
}
