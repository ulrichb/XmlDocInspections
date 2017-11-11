using JetBrains.Application.Settings;
using NUnit.Framework;

namespace XmlDocInspections.Plugin.Tests.Integrative.Highlighting
{
    [TestFixture]
    public class BasicTests : MissingXmlDocHighlightingDirectoryTestsBase
    {
        protected override void MutateSettings(IContextBoundSettingsStore settingsStore) => EnableAllXmlDocOptions(settingsStore);

        [Test]
        public void ClassesAndMembersWithDocs() => DoNamedTest("IToBeExplicitlyImplementedInterface.cs");

        [Test]
        public void ClassesAndMembersWithoutDocs() => DoNamedTest("IToBeExplicitlyImplementedInterface.cs");

        [Test]
        public void InternalClassWithoutDocs() => DoNamedTest();

        [Test]
        public void Inheritance() => DoNamedTest();

        [Test]
        public void InterfacesWithoutDocs() => DoNamedTest();

        [Test]
        public void PartialClassWithDocsInOneFile_A() => DoNamedTest("PartialClassWithDocsInOneFile_B.cs");

        [Test]
        public void PartialClassWithDocsInOneFile_B() => DoNamedTest("PartialClassWithDocsInOneFile_A.cs");

        [Test]
        public void PartialClassWithoutDocs_A() => DoNamedTest("PartialClassWithoutDocs_B.cs");

        [Test]
        public void PartialClassWithoutDocs_B() => DoNamedTest("PartialClassWithoutDocs_A.cs");

        [Test]
        public void StructWithoutDocs() => DoNamedTest();

        [Test]
        public void DelegateWithoutDocs() => DoNamedTest();
    }
}
