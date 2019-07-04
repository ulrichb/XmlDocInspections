using System;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global

namespace XmlDocInspections.Sample.Highlighting
{
    public class ClassWithoutPublicApiAttribute
    {
    }

    [PublicAPI]
    public class ClassWithPublicApiAttribute
    {
        [PublicAPI]
        public void MethodWithAttribute()
        {
        }

        public void MethodWithoutAttribute()
        {
        }
    }

    //

    /// <summary>A sample "RequireDocs" attribute (which is inherited).</summary>
    public class RequireDocsAttribute : Attribute
    {
    }

    public abstract class BaseClassWithPublicApiAttribute
    {
        [PublicAPI /* which is marked as non-inherited */]
        public abstract void MethodWithNonInheritedAttributeOnlyInBase();

        [RequireDocs]
        public abstract void MethodWithInheritedAttributeOnlyInBase();
    }

    public class DeribedClassWithPublicApiAttribute : BaseClassWithPublicApiAttribute
    {
        // Prove that the attribute-rule respects the attribute inheritance rule.

        public override void MethodWithNonInheritedAttributeOnlyInBase()
        {
        }

        public override void MethodWithInheritedAttributeOnlyInBase()
        {
        }
    }
}
