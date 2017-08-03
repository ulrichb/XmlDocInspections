using System.Reflection;

[assembly: AssemblyTitle(AssemblyConsts.Title)]

// ReSharper disable once CheckNamespace
internal static class AssemblyConsts
{
    public const string Title =
            "XML Doc Inspections ReSharper Plugin"
#if DEBUG
            + " (Debug Build)"
#endif
        ;
}
