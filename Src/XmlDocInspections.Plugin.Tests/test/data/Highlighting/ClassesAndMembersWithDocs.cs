using System;

#pragma warning disable 67
#pragma warning disable 169
#pragma warning disable 649

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedType.Local
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable EventNeverSubscribedTo.Local
// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedParameter.Local
// ReSharper disable EventNeverSubscribedTo.Global

namespace XmlDocInspections.Sample.Highlighting
{
    /// <summary>
    /// Some doc.
    /// </summary>
    public class PublicClassWithDocs : IToBeExplicitlyImplementedInterface
    {
        /// <summary>
        /// Some doc.
        /// </summary>
        public string PublicField;

        /// <summary>
        /// Some doc.
        /// </summary>
        internal string InternalField;

        /// <summary>
        /// Some doc.
        /// </summary>
        protected internal string ProtectedInternalField;

        /// <summary>
        /// Some doc.
        /// </summary>
        protected string ProtectedField;

        /// <summary>
        /// Some doc.
        /// </summary>
        private string PrivateField;

        //

        /// <summary>
        /// Some doc.
        /// </summary>
        public PublicClassWithDocs()
        {
        }

        /// <summary>
        /// Some doc.
        /// </summary>
        /// <param name="a"></param>
        public PublicClassWithDocs(string a)
        {
        }

        //

        /// <summary>
        /// Some doc.
        /// </summary>
        public string PublicProperty { get; set; }

        /// <summary>
        /// Some doc.
        /// </summary>
        internal string InternalProperty { get; set; }

        /// <summary>
        /// Some doc.
        /// </summary>
        protected internal string ProtectedInternalProperty { get; set; }

        /// <summary>
        /// Some doc.
        /// </summary>
        protected string ProtectedProperty { get; set; }

        /// <summary>
        /// Some doc.
        /// </summary>
        private string PrivateProperty { get; set; }

        //

        /// <summary>
        /// Some doc.
        /// </summary>
        /// <param name="a"></param>
        public void PublicMethod(string a)
        {
        }

        /// <summary>
        /// Some doc.
        /// </summary>
        public void PublicMethodWithoutParamDocs(string a)
        {
        }

        /// <summary>
        /// Some doc.
        /// </summary>
        void IToBeExplicitlyImplementedInterface.Method()
        {
        }

        /// <summary>
        /// Some doc.
        /// </summary>
        internal void InternalMethod()
        {
        }

        /// <summary>
        /// Some doc.
        /// </summary>
        protected void ProtectedMethod()
        {
        }

        /// <summary>
        /// Some doc.
        /// </summary>
        protected internal void ProtectedInternalMethod()
        {
        }

        /// <summary>
        /// Some doc.
        /// </summary>
        private void PrivateMethod()
        {
        }

        /// <summary>
        /// Some doc.
        /// </summary>
        public class PublicNestedClass
        {
        }

        /// <summary>
        /// Some doc.
        /// </summary>
        internal class InternalNestedClass
        {
        }

        /// <summary>
        /// Some doc.
        /// </summary>
        protected internal class ProtectedInternalNestedClass
        {
        }

        /// <summary>
        /// Some doc.
        /// </summary>
        protected class ProtectedNestedClass
        {
        }

        /// <summary>
        /// Some doc.
        /// </summary>
        private class PrivateNestedClass
        {
        }

        //

        /// <summary>
        /// Some doc.
        /// </summary>
        public event EventHandler PublicEvent;

        /// <summary>
        /// Some doc.
        /// </summary>
        internal event EventHandler InternalEvent;

        /// <summary>
        /// Some doc.
        /// </summary>
        protected internal event EventHandler ProtectedInternalEvent;

        /// <summary>
        /// Some doc.
        /// </summary>
        protected event EventHandler ProtectedEvent;

        /// <summary>
        /// Some doc.
        /// </summary>
        private event EventHandler PrivateEvent;

        //

        /// <summary>
        /// Some doc.
        /// </summary>
        public static PublicClassWithDocs operator +(PublicClassWithDocs left, PublicClassWithDocs right)
        {
            return new PublicClassWithDocs();
        }
    }
}
