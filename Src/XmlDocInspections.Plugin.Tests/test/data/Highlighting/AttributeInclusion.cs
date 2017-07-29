using JetBrains.Annotations;

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

    public abstract class BaseClassWithPublicApiAttribute
    {
        [PublicAPI]
        public abstract void MethodWithAttributeOnlyInBase();
    }

    public class DeribedClassWithPublicApiAttribute : BaseClassWithPublicApiAttribute
    {
        // Prove that the attribute-rule works with inherited attributes.

        public override void MethodWithAttributeOnlyInBase()
        {
        }
    }
}
