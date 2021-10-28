using Microsoft.CodeAnalysis;

namespace Qowaiv.CodeAnalysis
{
    public partial class SystemType
    {
        public static readonly SystemType System_DateTime = New(typeof(System.DateTime), SpecialType.System_DateTime);
    }
}
