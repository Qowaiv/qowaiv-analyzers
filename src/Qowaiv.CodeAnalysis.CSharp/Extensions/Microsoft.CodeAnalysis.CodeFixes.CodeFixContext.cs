using Microsoft.CodeAnalysis.CodeActions;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.CodeAnalysis.CodeFixes;

/// <summary>Extensions on <see cref="CodeFixContext"/>.</summary>
internal static  class CodeFixContextExtensions
{
    public static async Task<DiagnosticContext?> DiagnosticContext(this CodeFixContext context)
    {
        if(context.Diagnostics.FirstOrDefault() is { } diagnostic)
        {
            var root =await context.Document.GetSyntaxRootAsync(context.CancellationToken);
            return new(context.Document, diagnostic, root!);
        }
        else
        {
            return null;
        }
    }
}
