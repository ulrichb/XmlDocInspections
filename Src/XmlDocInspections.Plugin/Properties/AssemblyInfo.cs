using System.Reflection;

[assembly: AssemblyTitle(AssemblyConsts.Title)]
//

[assembly: AssemblyVersion("0.0.1.0")]
[assembly: AssemblyFileVersion("0.0.1.0")]
[assembly: AssemblyInformationalVersion("0.0.1.0-dev")]

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