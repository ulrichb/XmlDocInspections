namespace XmlDocInspections.Sample.QuickFixes.AddDocCommentFix
{
    public class MethodWithAdditionalElementsForDocTemplate
    {
        public string Method{caret}<T>(string a, params object[] p)
        {
            return "";
        }
    }
}
