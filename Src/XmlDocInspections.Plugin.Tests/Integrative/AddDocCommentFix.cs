using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using JetBrains.ReSharper.Intentions.CSharp.QuickFixes;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;

namespace XmlDocInspections.Plugin.Tests.Integrative
{
    [TestFixture]
    [TestNetFramework4]
    public class AddDocCommentFixTest : CSharpQuickFixTestBase<AddDocCommentFix>
    {
        protected override string RelativeTestDataPath => @"QuickFixes\AddDocCommentFix";

        [Test]
        public void TestClass()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestSimpleMethod()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestMethodWithAdditionalElementsForDocTemplate()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestField()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestProperty()
        {
            DoNamedTest2();
        }
    }
}
