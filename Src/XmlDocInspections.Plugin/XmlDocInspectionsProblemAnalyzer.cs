using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using ReSharperExtensionsShared.ProblemAnalyzers;
using XmlDocInspections.Plugin.Highlighting;

namespace XmlDocInspections.Plugin
{
    /// <summary>
    /// A problem analyzer for the XML Doc inspections.
    /// </summary>
    [ElementProblemAnalyzer(typeof(ICSharpTypeMemberDeclaration), HighlightingTypes = new[] { typeof(MissingXmlDocHighlighting) })]
    public class XmlDocInspectionsProblemAnalyzer : SimpleElementProblemAnalyzer<ICSharpTypeMemberDeclaration, ITypeMember>
    {
        private readonly XmlDocInspectionsSettingsCache _xmlDocInspectionsSettingsCache;

        public XmlDocInspectionsProblemAnalyzer(XmlDocInspectionsSettingsCache xmlDocInspectionsSettingsCache)
        {
            _xmlDocInspectionsSettingsCache = xmlDocInspectionsSettingsCache;
        }

        protected override void Run(
            ICSharpTypeMemberDeclaration declaration,
            ITypeMember typeMember,
            ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer)
        {
            HandleTypeMember(declaration, typeMember, consumer);
        }

        private void HandleTypeMember(ICSharpTypeMemberDeclaration declaration, ITypeMember typeMember, IHighlightingConsumer consumer)
        {
            if (declaration.IsSynthetic() || declaration.GetContainingTypeDeclaration()?.IsSynthetic() == true)
                return;

            var settings = _xmlDocInspectionsSettingsCache.GetCachedSettings(declaration.ToDataContext());

            if (IsProjectExcluded(declaration, settings) || IsTypeMemberExcluded(typeMember, settings))
                return;

            if (IsAccessibilityIncluded(typeMember, settings) || IsIncludedByAttribute(typeMember, settings))
            {
                if (typeMember.GetXMLDoc(inherit: false) == null)
                    consumer.AddHighlighting(new MissingXmlDocHighlighting(declaration));
            }
        }

        private static bool IsProjectExcluded(IDeclaration declaration, CachedXmlDocInspectionsSettings settings)
        {
            var projectExclusionRegex = settings.Value.ProjectExclusionRegex;

            if (string.IsNullOrWhiteSpace(projectExclusionRegex))
                return false;

            var project = declaration.GetProject().NotNull();
            return Regex.IsMatch(project.Name, projectExclusionRegex);
        }

        private bool IsTypeMemberExcluded(ITypeMember typeMember, CachedXmlDocInspectionsSettings settings)
        {
            return settings.Value.ExcludeMembersOverridingSuperMember && IsOverridingSuperMember(typeMember) ||
                   settings.Value.ExcludeConstructors && typeMember is IConstructor;
        }

        private bool IsOverridingSuperMember(ITypeMember typeMember)
        {
            return typeMember is IOverridableMember overridableMember && overridableMember.HasImmediateSuperMembers();
        }

        private static bool IsAccessibilityIncluded(ITypeMember typeMember, CachedXmlDocInspectionsSettings settings)
        {
            // Note that types are also type members.
            var isJustTypeMember = !(typeMember is ITypeElement);

            // Note that AccessibilityDomain also take containing type's accessibility into account.
            var accessibilityDomainType = typeMember.AccessibilityDomain.DomainType;

            var accessibilitySettingFlags = isJustTypeMember ? settings.Value.TypeMemberAccessibility : settings.Value.TypeAccessibility;

            return AccessibilityUtility.IsAccessibilityConfigured(accessibilityDomainType, accessibilitySettingFlags);
        }

        private bool IsIncludedByAttribute(ITypeMember typeMember, CachedXmlDocInspectionsSettings settings) =>
            settings.IncludeAttributeClrTypeNames.Any(x => typeMember.HasAttributeInstance(x, inherit: true));
    }
}
