using Microsoft.CodeAnalysis;
using Qowaiv.CodeAnalysis.Reflection;
using System;

namespace Qowaiv.CodeAnalysis.Syntax
{
    public static class SyntaxNodeExensions
    {
        public static TNode Cast<TNode>(this SyntaxNode node) where TNode : SyntaxNode
            => node as TNode 
            ?? throw new InvalidOperationException($"Unexpected {node.GetType().Name}, expected {typeof(TNode).Name}.");

        public static string Name(this SyntaxNode node)
            => node.Abstraction().Name(node);

        public static InvocationExpression InvocationExpression(this SyntaxNode node)
          => node.Abstraction().InvocationExpression(node);

        private static SyntaxNodes Abstraction(this SyntaxNode node)
        {
            var language = node.Language;
            var index = language == LanguageNames.CSharp ? 0 : 1;
            if (cache[index] is { } cached)
            {
                return cached;
            }
            else
            {
                cache[0] = TypedActivator.Create<SyntaxNodes>(type => type.Assembly.FullName.Contains(".CSharp"));
                cache[1] = TypedActivator.Create<SyntaxNodes>(type => type.Assembly.FullName.Contains(".VisualBasic"));
                return cache[index] ?? throw new InvalidOperationException($"SyntaxNodes could not be resolved for {language}.");
            }
        }

        private static readonly SyntaxNodes[] cache = new SyntaxNodes[2];
    }
}
