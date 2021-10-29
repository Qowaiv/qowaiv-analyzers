using Microsoft.CodeAnalysis;

namespace Qowaiv.CodeAnalysis
{
    public partial class SystemType
    {
        public static readonly SystemType System_DateTime = New(typeof(System.DateTime), SpecialType.System_DateTime);
        public static readonly SystemType System_Void = New(typeof(void), SpecialType.System_Void);

        public static readonly SystemType System_IDisposable = typeof(System.IDisposable);
        public static readonly SystemType System_ObsoleteAttribute = typeof(System.ObsoleteAttribute);

        public static readonly SystemType System_Diagnostics_Contracts_PureAttribute = typeof(System.Diagnostics.Contracts.PureAttribute);
        public static readonly SystemType System_Threading_Task = typeof(System.Threading.Tasks.Task);
    }
}
