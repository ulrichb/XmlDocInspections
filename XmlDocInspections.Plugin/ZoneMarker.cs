#if RESHARPER9

using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi.CSharp;

namespace XmlDocInspections.Plugin
{
    /// <summary>
    /// ReSharper platform zome marker.
    /// </summary>
    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>
    {
    }
}

#endif