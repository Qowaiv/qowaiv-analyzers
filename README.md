![Qowaiv](https://github.com/Qowaiv/qowaiv-analzyzers/blob/main/design/qowaiv-logo_linkedin_100x060.jpg)

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Code of Conduct](https://img.shields.io/badge/%E2%9D%A4-code%20of%20conduct-blue.svg?style=flat)](https://github.com/Qowaiv/qowaiv-analyzers/blob/main/CODE_OF_CONDUCT.md)
![Build Status](https://github.com/Qowaiv/qowaiv-analyzers/workflows/Build%20%26%20Test/badge.svg?branch=main)


| version                                                                   | package                                                                                    |
|---------------------------------------------------------------------------|--------------------------------------------------------------------------------------------|
|![v](https://img.shields.io/badge/version-0.0.4.1-blue.svg?cacheSeconds=3600)|[Qowaiv.Analyzers.CSharp](https://www.nuget.org/packages/Qowaiv.Analyzers.CSharp/)          |

# Qowaiv (static code) analyzers
Contains [Roslyn](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/)
(static code) [diagnostic analyzers](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.diagnostics.diagnosticanalyzer).

## Rules
* [**QW0001** - Use a testable Time Provider](rules/QW0001.md)
* [**QW0003** - Decorate pure functions](rules/QW0003.md)
* [**QW0004** - Characters with Trojan Horse potential are not allowed](rules/QW0004.md)
* [**QW0005** - Seal concrete classes unless designed for inheritance](rules/QW0005.md)
* [**QW0006** - Only concrete classes should be decorated as inheritable](rules/QW0006.md)

