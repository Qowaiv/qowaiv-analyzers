using Microsoft.CodeAnalysis;
using Qowaiv.CodeAnalysis.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;

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
            var assembly = node.GetType().Assembly;
            if(!cache.TryGetValue(assembly, out var cached))
            {
                if (assembly.FullName.Contains(".CSharp"))
                {
                    cached = TypedActivator.Create<SyntaxNodes>(type => type.Assembly.FullName.Contains(".CSharp"));
                    cache[assembly] = cached;
                }
                else if (assembly.FullName.Contains(".VisualBasic"))
                {
                    cached = TypedActivator.Create<SyntaxNodes>(type => type.Assembly.FullName.Contains(".VisualBasic"));
                    cache[assembly] = cached;
                }
            }
            return cached;
        }

        private static readonly Dictionary<Assembly, SyntaxNodes> cache = new Dictionary<Assembly, SyntaxNodes>();
    }
}
