using JetBrains.Application.Settings;
using JetBrains.ReSharper.TestFramework.Xaml;
using NUnit.Framework;
using XmlDocInspections.Plugin.Tests.Integrative.Highlighting;

namespace XmlDocInspections.Plugin.Tests.Integrative
{
    [TestFixture]
    [TestWPF40]
    public class SpecialCodeWpfTests : MissingXmlDocHighlightingTestsBase
    {
        protected override string RelativeTestDataPath => @"Special\WPF";

        protected override void MutateSettings(IContextBoundSettingsStore settingsStore) => EnableAllXmlDocOptions(settingsStore);

        [Test(Description = "Regression test for https://github.com/ulrichb/XmlDocInspections/issues/6")]
        public void TestXamlUserControl() => DoNamedTest2();
    }
}
