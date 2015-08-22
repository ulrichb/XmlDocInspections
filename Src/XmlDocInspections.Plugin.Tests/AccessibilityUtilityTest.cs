using JetBrains.ReSharper.Psi;
using NUnit.Framework;
using XmlDocInspections.Plugin.Settings;

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
            var result = AccessibilityUtility.IsAccessibilityConfigured(AccessibilityDomain.AccessibilityDomainType.NONE, doNotCare);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void FormatAccessibilityDomainType_WithUnknownAccessibility()
        {
            var result = AccessibilityUtility.FormatAccessibilityDomainType(AccessibilityDomain.AccessibilityDomainType.NONE);

            Assert.That(result, Is.EqualTo("<unknown>"));
        }
    }
}