using System;
using System.Collections.Generic;
using JetBrains.Application;
using JetBrains.Application.Parts;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Intentions.CSharp.QuickFixes;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace XmlDocInspections.Plugin.Highlighting
{
    [ShellComponent(Instantiation.DemandAnyThreadSafe)]
    internal class XmlDocInspectionsQuickFixRegistrarComponent : IQuickFixesProvider
    {
        public IEnumerable<Type> Dependencies => Array.Empty<Type>();

        public void Register(IQuickFixesRegistrar table)
        {
            table.RegisterQuickFix<MissingXmlDocHighlighting>(
                Lifetime.Eternal,
                h => CreateAddDocCommentFix(h.HighlightingNode), typeof(AddDocCommentFix));
        }

        private static AddDocCommentFix CreateAddDocCommentFix(ICSharpDeclaration declaration) => new AddDocCommentFix(declaration);
    }
}
