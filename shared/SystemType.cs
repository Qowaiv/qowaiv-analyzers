using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace Qowaiv.CodeAnalysis
{
    public sealed partial class SystemType
    {
        private SystemType(string fullName, SpecialType specialType = SpecialType.None)
        {
            FullName = fullName;
            ShortName = fullName.Split('.').Last();
            Type = specialType;
        }

        public string FullName { get; }
        public string ShortName { get; }
        public SpecialType Type { get; }

        internal bool Matches(string fullName) 
            => Type == SpecialType.None 
            && FullName == fullName;

        internal bool Matches(SpecialType specialType) 
            => specialType != SpecialType.None
            && Type == specialType;

        /// <inheritdoc />
        public override string ToString() => FullName;

        /// <summary>Casts a <see cref="System.Type"/> to a <see cref="SystemType"/>.</summary>
        public static implicit operator SystemType(Type type) => new SystemType(type.FullName, default);
        private static SystemType New(Type type, SpecialType specialType) => new SystemType(type.FullName, specialType);
    }
}
