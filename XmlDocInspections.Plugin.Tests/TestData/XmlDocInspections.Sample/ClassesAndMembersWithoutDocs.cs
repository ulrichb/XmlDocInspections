using System;
using XmlDocInspections.Sample.Utilities;

namespace XmlDocInspections.Sample
{
    public class PublicClassWithoutDocs : IExplicitlyImplementedInterface
    {
        public string PublicField;

        internal string InternalField;

        protected internal string ProtectedInternalField;

        protected string ProtectedField;

        private string PrivateField;

        //

        public string PublicProperty { get; set; }

        internal string InternalProperty { get; set; }

        protected internal string ProtectedInternalProperty { get; set; }

        protected string ProtectedProperty { get; set; }

        private string PrivateProperty { get; set; }

        //

        public void PublicMethod(string a)
        {
        }

        void IExplicitlyImplementedInterface.Method()
        {
        }

        internal void InternalMethod()
        {
        }

        protected void ProtectedMethod()
        {
        }

        protected internal void ProtectedInternalMethod()
        {
        }

        private void PrivateMethod()
        {
        }

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

        //

        public event EventHandler PublicEvent;

        internal event EventHandler InternalEvent;

        protected internal event EventHandler ProtectedInternalEvent;

        protected event EventHandler ProtectedEvent;

        private event EventHandler PrivateEvent;

        //

        public static PublicClassWithoutDocs operator +(PublicClassWithoutDocs left, PublicClassWithoutDocs right)
        {
            return null;
        }
    }

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