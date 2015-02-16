using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp;
using NUnit.Framework;
using XmlDocInspections.Plugin.Highlighting;

namespace XmlDocInspections.Plugin.Tests
{
    public class MissingXmlDocHighlightingTest : CSharpHighlightingTestNet4Base
    {
        protected override string RelativeTestDataPath
        {
            get { return "."; }
        }

        protected override bool HighlightingPredicate(
            IHighlighting highlighting, IContextBoundSettingsStore settingsStore)
        {
            return highlighting is XmlDocHighlightingBase;
        }

        // TODO: delegates, invalid files

        [Test]
        public void TestClassesAndMembersWithDocs()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestClassesAndMembersWithoutDocs()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestDerivedClassWithoutDocs()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestInterfacesWithoutDocs()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestPartialClassWithDocsInOneFile_A()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestPartialClassWithDocsInOneFile_B()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestPartialClassWithoutDocs_A()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestPartialClassWithoutDocs_B()
        {
            DoNamedTest2();
        }
    }
}