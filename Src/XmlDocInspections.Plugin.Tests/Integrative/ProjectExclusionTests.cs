using JetBrains.Application.Settings;
using NUnit.Framework;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin.Tests.Integrative
{
    [TestFixture]
    public class ProjectExclusionTests : MissingXmlDocHighlightingTestsBase
    {
        protected override void MutateSettings(IContextBoundSettingsStore settingsStore)
        {
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.ProjectExclusionRegex, "^Excl.*ject$");
        }

        protected override string ProjectName => "ExcludedProject";

        [Test]
        public void TestStructWithoutDocs()
        {
            DoNamedTest2();
        }
    }
}
