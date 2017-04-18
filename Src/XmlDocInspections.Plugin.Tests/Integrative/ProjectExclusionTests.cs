using JetBrains.Application.Settings;
using NUnit.Framework;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin.Tests.Integrative
{
    public abstract class ProjectExclusionTests : MissingXmlDocHighlightingTestsBase
    {
        public class ProjectExclusionWithMatchingRegexTests : ProjectExclusionTests
        {
            protected override string ProjectExclusionRegexValue => "^Excl.*ject$";

            [Test]
            public void TestClassesAndMembersWithoutDocs() => DoNamedTest2();
        }

        public class ProjectExclusionWithWhitespaceTests : ProjectExclusionTests
        {
            protected override string ProjectExclusionRegexValue => " \t ";

            [Test]
            public void TestClassesAndMembersWithoutDocs() => DoNamedTest2();
        }

        protected override string ProjectName => "ExcludedProject";

        protected abstract string ProjectExclusionRegexValue { get; }

        protected override void MutateSettings(IContextBoundSettingsStore settingsStore)
        {
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.ProjectExclusionRegex, ProjectExclusionRegexValue);
        }
    }
}
