// ReSharper disable UnusedType.Global
// ReSharper disable UnusedType.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local

namespace XmlDocInspections.Sample.Highlighting;

internal class InternalClassWithoutDocs
{
    public string PublicProperty { get; set; }

    internal string InternalProperty { get; set; }

    protected internal string ProtectedInternalProperty { get; set; }

    protected string ProtectedProperty { get; set; }

    private string PrivateProperty { get; set; }

    public class PublicNestedClass
    {
    }

    internal class InternalNestedClass
    {
    }

    protected internal class ProtectedInternalNestedClass
    {
    }

    protected class ProtectedNestedClass
    {
    }

    private class PrivateNestedClass
    {
    }
}
