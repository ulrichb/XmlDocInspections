namespace XmlDocInspections.Sample
{
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
}