using System;
using JetBrains.Application.Settings;
using NUnit.Framework;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin.Tests.IntegrationTests
{
    [TestFixture]
    public class BasicTests : MissingXmlDocHighlightingTestsBase
    {
        protected override void MutateSettings(IContextBoundSettingsStore settingsStore)
        {
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.TypeAccessibility, AccessibilitySettingFlags.All);
            settingsStore.SetValue((XmlDocInspectionsSettings s) => s.TypeMemberAccessibility, AccessibilitySettingFlags.All);
        }

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
        public void TestInternalClassWithoutDocs()
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

        [Test]
        public void TestStructWithoutDocs()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestDelegateWithoutDocs()
        {
            DoNamedTest2();
        }
    }
}