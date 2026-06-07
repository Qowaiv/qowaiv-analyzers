namespace Qowaiv.CodeAnalysis.Diagnostics;

public enum Category
{
    Bug,

    Design,

    [Display("Runtime Error")]
    RuntimeError,

    Security,

    Testabilty,
}
