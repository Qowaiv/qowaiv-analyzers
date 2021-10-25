﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Qowaiv.CodeAnalysis.Diagnostics;
using Qowaiv.CodeAnalysis.Syntax;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace Qowaiv.CodeAnalysis.Rules
{
    public abstract class ParseShouldNotFail<TSyntaxKind> : DiagnosticAnalyzer
        where TSyntaxKind : struct
    {
        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Description.ParseShouldNotFail;

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(c =>
            {
                var invocation = c.Node.InvocationExpression();
                if (c.Node.Name() == nameof(Parse)
                    && invocation.Arguments.Count() == 1
                    && c.SemanticModel.GetConstantValue(invocation.Expressions.First()).Value is string literal
                    && c.SemanticModel.GetSymbolInfo(c.Node).Symbol is IMethodSymbol method
                    && method.IsStatic
                    && SymbolEqualityComparer.Default.Equals(method.ContainingType, method.ReturnType)
                    && Parse(method, literal) is { } failure)
                {
                    c.ReportDiagnostic(this, c.Node, failure);
                }
            },
            SyntaxKinds.Of<TSyntaxKind>().InvocationExpression);
        }

        private static string Parse(IMethodSymbol symbol, string value)
        {
            if (symbol.GetMethodInfo() is { } method)
            {
                try
                {
                    method.Invoke(null, new object[] { value });
                }
                catch (TargetInvocationException x)
                {
                    return x.InnerException.Message ?? "Parsing failed.";
                }
            }
            return null;
        }

        private static bool IsParse(MethodInfo method)
            => method.Name == nameof(Parse)
            && method.GetParameters().Length == 1
            && method.GetParameters()[0].ParameterType == typeof(string)
            && method.ReturnType == method.DeclaringType;
    }
}
