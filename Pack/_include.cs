using System;

namespace Pack;

/// <summary>Esnures a depedency to the projects to ship.</summary>
internal static class Include
{
    public static readonly Type Rules = typeof(Qowaiv.CodeAnalysis.Rule);
    public static readonly Type Fixes = typeof(Qowaiv.CodeAnalysis.CodeFixes.Diagnostics.CodeFix);
}
