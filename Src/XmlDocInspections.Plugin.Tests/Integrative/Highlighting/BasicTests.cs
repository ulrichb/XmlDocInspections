using JetBrains.Application.Settings;
using NUnit.Framework;

namespace XmlDocInspections.Plugin.Tests.Integrative.Highlighting
{
    [TestFixture]
    public class BasicTests : MissingXmlDocHighlightingDirectoryTestsBase
    {
        protected override void MutateSettings(IContextBoundSettingsStore settingsStore) => EnableAllXmlDocOptions(settingsStore);

        [Test]
        public void TestClassesAndMembersWithDocs() => DoNamedTest2("IToBeExplicitlyImplementedInterface.cs");

        [Test]
        public void TestClassesAndMembersWithoutDocs() => DoNamedTest2("IToBeExplicitlyImplementedInterface.cs");

        [Test]
        public void TestInternalClassWithoutDocs() => DoNamedTest2();

        [Test]
        public void TestInheritance() => DoNamedTest2();

        [Test]
        public void TestInterfacesWithoutDocs() => DoNamedTest2();

        [Test]
        public void TestPartialClassWithDocsInOneFile_A() => DoNamedTest2("PartialClassWithDocsInOneFile_B.cs");

        [Test]
        public void TestPartialClassWithDocsInOneFile_B() => DoNamedTest2("PartialClassWithDocsInOneFile_A.cs");

        [Test]
        public void TestPartialClassWithoutDocs_A() => DoNamedTest2("PartialClassWithoutDocs_B.cs");

        [Test]
        public void TestPartialClassWithoutDocs_B() => DoNamedTest2("PartialClassWithoutDocs_A.cs");

        [Test]
        public void TestStructWithoutDocs() => DoNamedTest2();

        [Test]
        public void TestDelegateWithoutDocs() => DoNamedTest2();
    }
}
