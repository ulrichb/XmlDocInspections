using JetBrains.Application.Settings;
using NUnit.Framework;

namespace XmlDocInspections.Plugin.Tests.Integrative.Highlighting
{
    [TestFixture]
    public class DefaultSettingsTests : MissingXmlDocHighlightingDirectoryTestsBase
    {
        protected override void MutateSettings(IContextBoundSettingsStore settingsStore)
        {
        }

        [Test]
        public void TestClassesAndMembersWithoutDocs() => DoNamedTest2("IToBeExplicitlyImplementedInterface.cs");
    }
}
