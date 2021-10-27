using System;

namespace Qowaiv.CodeAnalysis.Diagnostics
{
    public enum Category
    {
        Testabilty,

        [Display("Runtime Error")]
        RuntimeError,
    }
}
