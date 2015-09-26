namespace XmlDocInspections.Sample.Highlighting
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
        public interface IPublicNestedInterface
        {
        }

        internal interface IInternalNestedInterface
        {
        }

        protected interface IProtectedNestedInterface
        {
        }

        private interface IPrivateNestedInterface
        {
        }
    }
}