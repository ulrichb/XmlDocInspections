using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Modules;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xaml;
using JetBrains.Util;
using JetBrains.Util.Logging;
using ReSharperExtensionsShared.Debugging;
using XmlDocInspections.Plugin.Highlighting;
using XmlDocInspections.Plugin.Settings;
using static JetBrains.ReSharper.Psi.AccessibilityDomain;

#if RESHARPER92 || RESHARPER100
using JetBrains.ReSharper.Daemon.CSharp.Stages;

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
        private readonly ISettingsOptimization _settingsOptimization;

        public XmlDocInspectionsProblemAnalyzer(ISettingsStore settingsStore, ISettingsOptimization settingsOptimization)
        {
            Log.Verbose(".ctor");
            _settingsStore = settingsStore;
            _settingsOptimization = settingsOptimization;
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

            Log.Verbose(DebugUtility.FormatWithElapsed(message, stopwatch));
#endif
        }

        private IEnumerable<IHighlighting> HandleTypeMember([NotNull] IDeclaration declaration, [CanBeNull] ITypeMember typeMember)
        {
            if (typeMember != null && typeMember.GetXMLDoc(inherit: false) == null)
            {
                if (!declaration.Language.Is<XamlLanguage>())
                {
                    var module = typeMember.Module;
                    var settingsStore = _settingsStore.BindToContextTransient(ContextRange.Smart(module.ToDataContext()));

                    var settings = settingsStore.GetKey<XmlDocInspectionsSettings>(_settingsOptimization);

                    if (IsModuleNotExcluded(module, settingsStore))
                    {
                        if (!IsOverridingMember(typeMember) || settings.RequireDocsOnOverridingMember)
                        {
                            // Note that AccessibilityDomain also take containing type's accessibility into account.
                            var accessibilityDomainType = typeMember.AccessibilityDomain.DomainType;

                            // Note that types are also type members.
                            var isTypeMember = !(typeMember is ITypeElement);

                            if (IsConfigured(settings, isTypeMember, accessibilityDomainType))
                                yield return new MissingXmlDocHighlighting(isTypeMember, accessibilityDomainType, declaration);
                        }
                    }
                }
            }
        }

        private bool IsOverridingMember(ITypeMember typeMember)
        {
            var overridableMember = typeMember as IOverridableMember;

            return overridableMember != null && overridableMember.HasImmediateSuperMembers();
        }

        private static bool IsModuleNotExcluded(IPsiModule module, IContextBoundSettingsStore settingsStore)
        {
            var projectExclusionRegex = settingsStore.GetValue((XmlDocInspectionsSettings s) => s.ProjectExclusionRegex);
            return string.IsNullOrWhiteSpace(projectExclusionRegex) || !Regex.IsMatch(module.Name, projectExclusionRegex);
        }

        private static bool IsConfigured(XmlDocInspectionsSettings settings, bool isTypeMember, AccessibilityDomainType accessibility)
        {
            var accessibilitySettingFlags = isTypeMember ? settings.TypeMemberAccessibility : settings.TypeAccessibility;

            return AccessibilityUtility.IsAccessibilityConfigured(accessibility, accessibilitySettingFlags);
        }
    }
}