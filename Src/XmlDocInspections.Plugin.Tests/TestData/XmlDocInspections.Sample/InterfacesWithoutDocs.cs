namespace XmlDocInspections.Sample
{
    public interface IPublicInterfacesWithoutDocs
    {
        string Property { get; }

        void Method();
    }

    internal interface IInternalInterfacesWithoutDocs
    {
        string Property { get; }
        
        void Method();
    }

    // ReSharper disable once MissingXmlDoc
    public class ClassWithNestedInterfacesWithoutDocs
    {
        public interface IPublicNestednterface
        {
        }

        internal interface IInternalNestednterface
        {
        }

        protected interface IProtectedNestednterface
        {
        }

        private interface IPrivateNestednterface
        {
        }
    }
}