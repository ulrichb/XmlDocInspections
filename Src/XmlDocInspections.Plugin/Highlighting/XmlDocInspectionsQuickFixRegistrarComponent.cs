using JetBrains.Application;
using JetBrains.ReSharper.Daemon.CSharp.Errors;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Intentions.CSharp.QuickFixes;

namespace XmlDocInspections.Plugin.Highlighting
{
    [ShellComponent]
    internal class XmlDocInspectionsQuickFixRegistrarComponent
    {
        public XmlDocInspectionsQuickFixRegistrarComponent(IQuickFixes table)
        {
            table.RegisterQuickFix<MissingXmlDocHighlighting>(null, h => new AddDocCommentFix(Wrap(h)), typeof(AddDocCommentFix));
        }

        private static PublicOrProtectedMemberNotDocumentedWarning Wrap(MissingXmlDocHighlighting highlighting)
        {
            // Wrap the highlighting because there is no generic c'tor overload in AddDocCommentFix. Note that the QuickFix is only
            // interested in the warning's declaration.
            return new PublicOrProtectedMemberNotDocumentedWarning(highlighting.TreeNode);
        }
    }
}
