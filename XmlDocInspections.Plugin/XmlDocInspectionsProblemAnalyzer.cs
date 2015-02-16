using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using JetBrains.Util.Logging;
using XmlDocInspections.Plugin.Highlighting;
using XmlDocInspections.Plugin.Infrastructure;
#if RESHARPER8
using JetBrains.ReSharper.Daemon.Stages;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;

#endif

namespace XmlDocInspections.Plugin
{
    /// <summary>
    /// A problem analyzer daemon for the XML Doc inspections.
    /// </summary>
    [ElementProblemAnalyzer(typeof (ITypeMemberDeclaration), HighlightingTypes =
        new[]
        {
            typeof (MissingPublicTypeXmlDocHighlighting),
            typeof (MissingPublicTypeMemberXmlDocHighlighting)
        })]
    public class XmlDocInspectionsProblemAnalyzer : ElementProblemAnalyzer<ITypeMemberDeclaration>
    {
        private static readonly ILogger Log = Logger.GetLogger(typeof (XmlDocInspectionsProblemAnalyzer));

        public XmlDocInspectionsProblemAnalyzer()
        {
            Log.LogMessage(LoggingLevel.INFO, ".ctor");
        }

        protected override void Run(ITypeMemberDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
#if DEBUG
            var stopwatch = Stopwatch.StartNew();
#endif

            var typeMember = element.DeclaredElement;
            var highlightingResults = HandleTypeMember(element, typeMember).ToList();

            highlightingResults.ForEach(x => consumer.AddHighlighting(x, x.CalculateRange()));

#if DEBUG
            var message = DebugUtilities.FormatIncludingContext(typeMember) + " => ["
                          + string.Join(", ", highlightingResults.Select(x => x.GetType().Name)) + "]";

            Log.LogMessage(LoggingLevel.VERBOSE, DebugUtilities.FormatWithElapsed(message, stopwatch));
#endif
        }

        private IEnumerable<XmlDocHighlightingBase> HandleTypeMember([NotNull] IDeclaration declaration, [CanBeNull] ITypeMember typeMember)
        {
            // We ignore inherited XML Docs.
            if (typeMember != null && typeMember.GetXMLDoc(inherit: false) == null)
            {
                // Note that types are also type members.

                // Note that AccessibilityDomain also take containing type's visibility into account.
                var accessibilityDomainType = typeMember.AccessibilityDomain.DomainType;

                if (accessibilityDomainType == AccessibilityDomain.AccessibilityDomainType.PUBLIC)
                {
                    if (typeMember is ITypeElement)
                        yield return new MissingPublicTypeXmlDocHighlighting(declaration);
                    else
                        yield return new MissingPublicTypeMemberXmlDocHighlighting(declaration);
                }
                else if (accessibilityDomainType == AccessibilityDomain.AccessibilityDomainType.PROTECTED ||
                         accessibilityDomainType == AccessibilityDomain.AccessibilityDomainType.PROTECTED_OR_INTERNAL)
                {
                    if (typeMember is ITypeElement)
                        yield return new MissingProtectedTypeXmlDocHighlighting(declaration);
                    else
                        yield return new MissingProtectedTypeMemberXmlDocHighlighting(declaration);
                }
            }
        }
    }
}