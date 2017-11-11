// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Local

namespace XmlDocInspections.Sample.Highlighting
{
    /// <summary>Some doc.</summary>
    public class BaseClassWithDocs
    {
        /// <summary>Some doc.</summary>
        public BaseClassWithDocs(string param)
        {
        }

        /// <summary>Some doc.</summary>
        public string Method() => "";

        /// <summary>Some doc.</summary>
        public virtual string OverridableMethod() => "";
    }

    public class IntermediateDerivedFromBaseClassWithDocs : BaseClassWithDocs
    {
        public IntermediateDerivedFromBaseClassWithDocs(string param) : base(param)
        {
        }
    }

    public class DerivedFromBaseClassWithDocs : IntermediateDerivedFromBaseClassWithDocs
    {
        public DerivedFromBaseClassWithDocs(string param) : base(param)
        {
        }

        public new string Method() => "";

        public override string OverridableMethod() => "";
    }

    //

    public class BaseClassWithoutDocs
    {
        public virtual string OverridableMethod() => "";
    }

    public class DerivedFromBaseClassWithoutDocs : BaseClassWithoutDocs
    {
        // Here we also expect no warning because it would get out of date, when the docs are added to the base member:
        public override string OverridableMethod() => "";
    }

    //

    public interface IInterfaceWithoutDocs
    {
        string Method();
    }

    public interface IInterfaceWithoutDocsToBeExplicitlyImplemented
    {
        string Method();
    }

    public class DerivedFromInterfaceWithoutDocs : IInterfaceWithoutDocs, IInterfaceWithoutDocsToBeExplicitlyImplemented
    {
        public string Method() => "";

        string IInterfaceWithoutDocsToBeExplicitlyImplemented.Method() => "";
    }
}
