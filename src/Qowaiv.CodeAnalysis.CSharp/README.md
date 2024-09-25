# Qowaiv (static code) analyzers
Contains [Roslyn](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/) (static code) [diagnostic analyzers](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.diagnostics.diagnosticanalyzer).

## Rules
* [**QW0001** - Use a testable Time Provider](rules/QW0001.md)
* [**QW0003** - Decorate pure functions](rules/QW0003.md)
* [**QW0004** - Characters with Trojan Horse potential are not allowed](rules/QW0004.md)
* [**QW0005** - Seal concrete classes unless designed for inheritance](rules/QW0005.md)
* [**QW0006** - Only unsealed concrete classes should be decorated as inheritable](rules/QW0006.md)
* [**QW0007** - Use file-scoped namespace declarations](rules/QW0007.md)
* [**QW0008** - Define properties as not-nullable for types with a defined empty state](rules/QW0008.md)
* [**QW0009** - Define properties as not-nullable for enums with a defined none/empty value](rules/QW0009.md)
* [**QW0010** - Use System.DateOnly instead of Qowaiv.Date](rules/QW0010.md)
* [**QW0011** - Define properties as immutables](rules/QW0011.md)
* [**QW0012** - Use immutable types for properties](rules/QW0012.md)
* [**QW0013** - Use Qowaiv decimal rounding](rules/QW0013.md)
* [**QW0014** - Define global using statements separately](rules/QW0014.md)
* [**QW0015** - Define global using statements in single file](rules/QW0015.md)
* [**QW0016** - Prefer regular over positional properties](rules/QW0016.md)
* [**QW0017** - Apply arithmetic operations on non-nullables only](rules/QW0017.md)

## Code fixes
* Use Qowaiv.Clock ([QW0001](rules/QW0001.md), [S6354](https://rules.sonarsource.com/csharp/RSPEC-6354))
* Seal class ([QW0005](rules/QW0005.md))
* Change property type to not-nullable ([QW0008](rules/QW0008.md), [QW0009](rules/QW0009.md))
* Change type System.DateOnly ([QW0010](rules/QW0010.md))
* Apply suggestions of obsolete code attribute ([CS0618, CS0619])/rules/ObsoleteCode.md))
* Use Qowaiv round extensions ([QW0013](rules/QW0013.md))
* Convert positional properties ([QW0016](rules/QW0016.md))
