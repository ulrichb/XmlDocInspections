using System;
using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp;
using NUnit.Framework;
using XmlDocInspections.Plugin.Highlighting;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin.Tests
{
    public class MissingXmlDocHighlightingTest : CSharpHighlightingTestNet4Base
    {
        protected override string RelativeTestDataPath
        {
            get { return "."; }
        }

        protected override bool HighlightingPredicate(IHighlighting highlighting, IContextBoundSettingsStore settingsStore)
        {
            return highlighting is XmlDocHighlightingBase;
        }

        protected override void WithProject(IProject project, ISettingsStore settingsStore, Action action)
        {
            var contextRange = ContextRange.Smart(project.ToDataContext());
            var contextBoundSettingsStore = settingsStore.BindToContextTransient(contextRange);

            contextBoundSettingsStore.SetValue((XmlDocInspectionsSettings s) => s.TypeAccessibility, AccessibilitySettingFlags.All);
            contextBoundSettingsStore.SetValue((XmlDocInspectionsSettings s) => s.TypeMemberAccessibility, AccessibilitySettingFlags.All);

            base.WithProject(project, settingsStore, action);
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