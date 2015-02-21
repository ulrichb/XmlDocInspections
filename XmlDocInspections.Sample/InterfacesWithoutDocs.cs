namespace XmlDocInspections.Sample
{
    public interface IPublicInterfacesWithoutDocs
    {
    }

    internal interface IInternalInterfacesWithoutDocs
    {
    }

    // ReSharper disable once MissingXmlDocHighlighting
    public class PublicClassWithNestedInterfaces
    {
        public interface IPublicNested
        {
        }

        internal interface IInternalNested
        {
        }

        protected interface IProtectedNested
        {
        }

        private interface IPrivateNested
        {
        }
    }
}