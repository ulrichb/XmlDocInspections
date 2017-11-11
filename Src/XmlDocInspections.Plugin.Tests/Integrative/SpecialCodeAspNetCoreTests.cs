#if !RS20171
using JetBrains.Application.Settings;
using JetBrains.ReSharper.TestFramework;
using JetBrains.ReSharper.TestFramework.Web;
using NUnit.Framework;
using XmlDocInspections.Plugin.Tests.Integrative.Highlighting;

namespace XmlDocInspections.Plugin.Tests.Integrative
{
    [TestFixture]
    [TestAspNetCore20]
    [TestFileExtension(".cshtml")]
    public class SpecialCodeAspNetCoreTests : MissingXmlDocHighlightingTestsBase
    {
        protected override string RelativeTestDataPath => @"Special\AspNetCore";

        protected override void MutateSettings(IContextBoundSettingsStore settingsStore) => EnableAllXmlDocOptions(settingsStore);

        [Test]
        public void InjectDirective() => DoNamedTest();
    }
}
#endif
