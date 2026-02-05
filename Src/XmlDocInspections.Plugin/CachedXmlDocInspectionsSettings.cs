using System.Collections.Generic;
using JetBrains.Metadata.Reader.Impl;
using XmlDocInspections.Plugin.Settings;

namespace XmlDocInspections.Plugin;

public class CachedXmlDocInspectionsSettings
{
    public CachedXmlDocInspectionsSettings(XmlDocInspectionsSettings value, List<ClrTypeName> includeAttributeClrTypeNames)
    {
        Value = value;
        IncludeAttributeClrTypeNames = includeAttributeClrTypeNames;
    }

    public XmlDocInspectionsSettings Value { get; }

    public IReadOnlyCollection<ClrTypeName> IncludeAttributeClrTypeNames { get; }
}