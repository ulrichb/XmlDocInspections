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
using ReSharperExtensionsShared.Debugging;
using XmlDocInspections.Plugin.Highlighting;
using XmlDocInspections.Plugin.Settings;
#if RESHARPER8
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.Stages;

#else
using JetBrains.ReSharper.Feature.Services.Daemon;

#endif

namespace XmlDocInspections.Plugin
{
    /// <summary>
    /// A problem analyzer for the XML Doc inspections.
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

            highlightingResults.ForEach(x => consumer.AddHighlighting(x));

#if DEBUG
            var message = DebugUtility.FormatIncludingContext(typeMember) + " => ["
                          + string.Join(", ", highlightingResults.Select(x => x.GetType().Name)) + "]";

            Log.LogMessage(LoggingLevel.VERBOSE, DebugUtility.FormatWithElapsed(message, stopwatch));
#endif
        }

        private IEnumerable<IHighlighting> HandleTypeMember([NotNull] IDeclaration declaration, [CanBeNull] ITypeMember typeMember)
        {
            // We ignore inherited XML Docs.
            if (typeMember != null && typeMember.GetXMLDoc(inherit: false) == null)
            {
                var module = typeMember.Module;
                var settingsStore = _settingsStore.BindToContextTransient(ContextRange.Smart(module.ToDataContext()));

                var projectExclusionRegex = settingsStore.GetValue((XmlDocInspectionsSettings s) => s.ProjectExclusionRegex);
                if (string.IsNullOrWhiteSpace(projectExclusionRegex) || !Regex.IsMatch(module.Name, projectExclusionRegex))
                {
                    // Note that AccessibilityDomain also take containing type's accessibility into account.
                    var accessibilityDomainType = typeMember.AccessibilityDomain.DomainType;

                    // Note that types are also type members.
                    var isTypeMember = !(typeMember is ITypeElement);

                    if (IsConfigured(settingsStore, isTypeMember, accessibilityDomainType))
                        yield return new MissingXmlDocHighlighting(isTypeMember, accessibilityDomainType, declaration);
                }
            }
        }

        private bool IsConfigured(IContextBoundSettingsStore settingsStore, bool isTypeMember, AccessibilityDomain.AccessibilityDomainType accessibility)
        {
            var accessibilitySettingFlags = isTypeMember
                ? settingsStore.GetValue((XmlDocInspectionsSettings s) => s.TypeMemberAccessibility)
                : settingsStore.GetValue((XmlDocInspectionsSettings s) => s.TypeAccessibility);

            return AccessibilityUtility.IsAccessibilityConfigured(accessibility, accessibilitySettingFlags);
        }
    }
}