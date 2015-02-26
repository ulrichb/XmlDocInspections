using JetBrains.Annotations;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi.Tree;
#if RESHARPER8
using IHighlighting = JetBrains.ReSharper.Daemon.Impl.IHighlightingWithRange;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;

#endif

namespace XmlDocInspections.Plugin.Highlighting
{
    /// <summary>
    /// Base class for Xml Doc inspections.
    /// </summary>
    public abstract class XmlDocHighlightingBase : IHighlighting
    {
        private readonly IDeclaration _declaration;
        private readonly string _toolTipText;

        protected XmlDocHighlightingBase([NotNull] IDeclaration declaration, [NotNull] string toolTipText)
        {
            _declaration = declaration;
            _toolTipText = toolTipText;
        }

        public string ToolTip
        {
            get { return _toolTipText; }
        }

        public string ErrorStripeToolTip
        {
            get { return _toolTipText; }
        }

        public int NavigationOffsetPatch
        {
            get { return 0; }
        }

        public bool IsValid()
        {
            return _declaration.IsValid();
        }

        public DocumentRange CalculateRange()
        {
            return _declaration.GetNameDocumentRange();
        }
    }
}