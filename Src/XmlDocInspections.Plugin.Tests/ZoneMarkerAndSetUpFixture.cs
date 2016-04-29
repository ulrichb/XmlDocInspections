using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;
using XmlDocInspections.Plugin.Tests;

[assembly: RequiresSTA]

namespace XmlDocInspections.Plugin.Tests
{
    [ZoneDefinition]
    public interface IXmlDocInspectionsTestEnvironmentZone : ITestsZone, IRequire<PsiFeatureTestZone>
    {
    }

    [ZoneMarker]
    public class ZoneMarker : IRequire<IXmlDocInspectionsTestEnvironmentZone>
    {
    }
}

// ReSharper disable once CheckNamespace
[SetUpFixture]
public class TestEnvironmentSetUpFixture : ExtensionTestEnvironmentAssembly<IXmlDocInspectionsTestEnvironmentZone>
{
}