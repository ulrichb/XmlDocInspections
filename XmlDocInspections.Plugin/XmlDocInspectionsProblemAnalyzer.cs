using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using JetBrains.Util.Logging;
using XmlDocInspections.Plugin.Highlighting;
using XmlDocInspections.Plugin.Infrastructure;
using XmlDocInspections.Plugin.Settings;
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
    [ElementProblemAnalyzer(typeof(ITypeMemberDeclaration), HighlightingTypes = new[] { typeof(MissingXmlDocHighlighting) })]
    public class XmlDocInspectionsProblemAnalyzer : ElementProblemAnalyzer<ITypeMemberDeclaration>
    {
        private static readonly ILogger Log = Logger.GetLogger(typeof(XmlDocInspectionsProblemAnalyzer));

        private readonly ISettingsStore _settingsStore;

        public XmlDocInspectionsProblemAnalyzer(ISettingsStore settingsStore)
        {
            Log.LogMessage(LoggingLevel.INFO, ".ctor");
            _settingsStore = settingsStore;
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
                var module = typeMember.Module;
                var contextRange = ContextRange.Smart(module.ToDataContext());

                var exclusionRegex = _settingsStore.BindToContextTransient(contextRange).GetValue((XmlDocInspectionsSettings s) => s.ExclusionRegex);
                if (string.IsNullOrWhiteSpace(exclusionRegex) || !Regex.IsMatch(module.Name, exclusionRegex))
                {
                    // Note that AccessibilityDomain also take containing type's accessibility into account.
                    var accessibilityDomainType = typeMember.AccessibilityDomain.DomainType;

                    // Note that types are also type members.
                    var isTypeMember = !(typeMember is ITypeElement);

                    if (IsConfigured(isTypeMember, accessibilityDomainType, contextRange))
                        yield return new MissingXmlDocHighlighting(isTypeMember, accessibilityDomainType, declaration);
                }
            }
        }

        private bool IsConfigured(bool isTypeMember, AccessibilityDomain.AccessibilityDomainType accessibility, [NotNull] ContextRange contextRange)
        {
            var accessibilitySettingFlags = isTypeMember
                ? _settingsStore.BindToContextTransient(contextRange).GetValue((XmlDocInspectionsSettings s) => s.TypeMemberAccessibility)
                : _settingsStore.BindToContextTransient(contextRange).GetValue((XmlDocInspectionsSettings s) => s.TypeAccessibility);

            return AccessibilityUtilities.IsAccessibilityConfigured(accessibility, accessibilitySettingFlags);
        }
    }
}