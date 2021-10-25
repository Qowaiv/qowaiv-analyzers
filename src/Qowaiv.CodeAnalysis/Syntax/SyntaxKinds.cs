using Qowaiv.CodeAnalysis.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Qowaiv.CodeAnalysis.Syntax
{
    public static class SyntaxKinds
    {
        public static SyntaxKinds<TSyntaxKind> Of<TSyntaxKind>() where TSyntaxKind: struct
        {
            if (!cache.TryGetValue(typeof(TSyntaxKind), out var cached))
            {
                cached = TypedActivator.Create<SyntaxKinds<TSyntaxKind>>(type => true);
                cache[typeof(TSyntaxKind)] = cached;
            }
            return (SyntaxKinds<TSyntaxKind>)cached;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly Dictionary<Type, object> cache = new Dictionary<Type, object>();
    }

    public abstract class SyntaxKinds<TSyntaxKind> where TSyntaxKind : struct
    {
        public abstract TSyntaxKind IdentifierName { get; }
    }
}
