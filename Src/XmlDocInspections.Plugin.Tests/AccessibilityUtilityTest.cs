using NUnit.Framework;
using XmlDocInspections.Plugin.Settings;
using static JetBrains.ReSharper.Psi.AccessibilityDomain;

namespace XmlDocInspections.Plugin.Tests
{
    [TestFixture]
    public class AccessibilityUtilityTest
    {
        // NOTE: All other cases are covered by the integrative tests

        [Test]
        public void IsAccessibilityConfigured_WithUnknownAccessibility()
        {
            var doNotCare = AccessibilitySettingFlags.All;
            var result = AccessibilityUtility.IsAccessibilityConfigured(AccessibilityDomainType.NONE, doNotCare);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void FormatAccessibilityDomainType_WithUnknownAccessibility()
        {
            var result = AccessibilityUtility.FormatAccessibilityDomainType(AccessibilityDomainType.NONE);

            Assert.That(result, Is.EqualTo("<unknown>"));
        }
    }
}