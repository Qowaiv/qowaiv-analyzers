# Qowaiv (static code) analyzers
Contains [Roslyn](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/) (static code) [diagnostic analyzers](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.diagnostics.diagnosticanalyzer).

## Rules
* [**QW0001** - Use a testable Time Provider](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0001.md)
* [**QW0003** - Decorate pure functions](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0003.md)
* [**QW0004** - Characters with Trojan Horse potential are not allowed](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0004.md)
* [**QW0005** - Seal concrete classes unless designed for inheritance](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0005.md)
* [**QW0006** - Only unsealed concrete classes should be decorated as inheritable](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0006.md)
* [**QW0007** - Use file-scoped namespace declarations](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0007.md)
* [**QW0008** - Define properties as not-nullable for types with a defined empty state](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0008.md)
* [**QW0009** - Define properties as not-nullable for enums with a defined none/empty value](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0009.md)
* [**QW0010** - Use System.DateOnly instead of Qowaiv.Date](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0010.md)
* [**QW0011** - Define properties as immutables](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0011.md)
* [**QW0012** - Use immutable types for properties](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0012.md)
* [**QW0013** - Use Qowaiv decimal rounding](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0013.md)
* [**QW0014** - Define global using statements separately](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0014.md)
* [**QW0015** - Define global using statements in single file](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0015.md)
* [**QW0016** - Prefer regular over positional properties](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0016.md)
* [**QW0017** - Apply arithmetic operations on non-nullables only](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0017.md)
* [**QW0018** - Use Qowaiv.Clock.TimeProvider](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0018.md)
* [**QW0019** - Prefer XML to LINQ over DOM](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0019.md)
* [**QW0020** - Prefer System.Text.Json over Newtonsoft.Json](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0020.md)

### Data Annotation rules
* [**QW0100** - Define only one Required attribute](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0100.md)
* [**QW0101** - Required attribute cannot invalidate value types](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0101.md)
* [**QW0102** - Use compliant validation attributes](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0102.md)
* [**QW0103** - Decorate validation attributes](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0103.md)
* [**QW0104** - Use validates attribute on validation attributes only](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0104.md)

## Code fixes
* Use Qowaiv.Clock ([QW0001](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0001.md), [S6354](https://rules.sonarsource.com/csharp/RSPEC-6354))
* Seal class ([QW0005](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0005.md))
* Change property type to not-nullable ([QW0008](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0008.md), [QW0009](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0009.md))
* Change type System.DateOnly ([QW0010](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0010.md))
* Apply suggestions of obsolete code attribute [CS0618, CS0619](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/ObsoleteCode.md))
* Use Qowaiv round extensions ([QW0013](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0013.md))
* Convert positional properties ([QW0016](https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/QW0016.md))
