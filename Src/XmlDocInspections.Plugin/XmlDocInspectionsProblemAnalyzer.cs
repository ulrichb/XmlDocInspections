using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using ReSharperExtensionsShared.ProblemAnalyzers;
using XmlDocInspections.Plugin.Highlighting;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin
{
    /// <summary>
    /// A problem analyzer for the XML Doc inspections.
    /// </summary>
    [ElementProblemAnalyzer(typeof(ICSharpTypeMemberDeclaration), HighlightingTypes = new[] { typeof(MissingXmlDocHighlighting) })]
    public class XmlDocInspectionsProblemAnalyzer : SimpleElementProblemAnalyzer<ICSharpTypeMemberDeclaration, ITypeMember>
    {
        private readonly ISettingsStore _settingsStore;
        private readonly ISettingsOptimization _settingsOptimization;

        public XmlDocInspectionsProblemAnalyzer(ISettingsStore settingsStore, ISettingsOptimization settingsOptimization)
        {
            _settingsStore = settingsStore;
            _settingsOptimization = settingsOptimization;
        }

        protected override void Run(
            ICSharpTypeMemberDeclaration declaration,
            ITypeMember typeMember,
            ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer)
        {
            var highlightingResults = HandleTypeMember(declaration, typeMember).ToList();

            highlightingResults.ForEach(x => consumer.AddHighlighting(x));
        }

        private IEnumerable<IHighlighting> HandleTypeMember(ICSharpTypeMemberDeclaration declaration, ITypeMember typeMember)
        {
            var settingsStore = _settingsStore.BindToContextTransient(ContextRange.Smart(typeMember.Module.ToDataContext()));
            var settings = settingsStore.GetKey<XmlDocInspectionsSettings>(_settingsOptimization);

            if (!(IsProjectExcluded(declaration, settings) || IsTypeMemberExcluded(typeMember, settings)))
            {
                if (IsAccessibilityMatchingWithConfiguration(typeMember, settings))
                {
                    if (typeMember.GetXMLDoc(inherit: false) == null)
                        yield return new MissingXmlDocHighlighting(declaration);
                }
            }
        }

        private static bool IsProjectExcluded(IDeclaration declaration, XmlDocInspectionsSettings settings)
        {
            var projectExclusionRegex = settings.ProjectExclusionRegex;

            if (string.IsNullOrWhiteSpace(projectExclusionRegex))
                return false;

            var project = declaration.GetProject().NotNull();
            return Regex.IsMatch(project.Name, projectExclusionRegex);
        }

        private bool IsTypeMemberExcluded(ITypeMember typeMember, XmlDocInspectionsSettings settings)
        {
            return settings.ExcludeMembersOverridingSuperMember && IsOverridingSuperMember(typeMember) ||
                   settings.ExcludeConstructors && typeMember is IConstructor;
        }

        private bool IsOverridingSuperMember(ITypeMember typeMember)
        {
            return typeMember is IOverridableMember overridableMember && overridableMember.HasImmediateSuperMembers();
        }

        private static bool IsAccessibilityMatchingWithConfiguration(ITypeMember typeMember, XmlDocInspectionsSettings settings)
        {
            // Note that types are also type members.
            var isJustTypeMember = !(typeMember is ITypeElement);

            // Note that AccessibilityDomain also take containing type's accessibility into account.
            var accessibilityDomainType = typeMember.AccessibilityDomain.DomainType;

            var accessibilitySettingFlags = isJustTypeMember ? settings.TypeMemberAccessibility : settings.TypeAccessibility;

            return AccessibilityUtility.IsAccessibilityConfigured(accessibilityDomainType, accessibilitySettingFlags);
        }
    }
}
