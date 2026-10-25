namespace Microsoft.CodeAnalysis.CSharp.Syntax;

public static class ParameterSyntaxExtensions
{
    extension(ParameterSyntax parameter)
    {
        public bool NotNull => parameter.Type is not NullableTypeSyntax;
    }
}
