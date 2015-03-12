using JetBrains.Application.Settings;
using NUnit.Framework;

namespace XmlDocInspections.Plugin.Tests.IntegrationTests
{
    [TestFixture]
    public class DefaultSettingsTests : MissingXmlDocHighlightingTestsBase
    {
        protected override void MutateSettings(IContextBoundSettingsStore settingsStore)
        {
        }

        [Test]
        public void TestClassesAndMembersWithoutDocs()
        {
            DoNamedTest2();
        }
    }
}