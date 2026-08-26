# AI Coding Agent Handbook - Qowaiv Analyzers

Welcome! This document provides information, commands, and conventions for AI coding agents working on the Qowaiv static code analyzers codebase.

---

## 1. Project Overview

Qowaiv Analyzers is a Roslyn-based static code analysis suite for C#, targeting the .NET ecosystem.
* **Analyzer Project**: `Qowaiv.CodeAnalysis.CSharp/` (Targets `netstandard2.0`)
* **Test Specs Project**: `Specs/` (Targets `net10.0`)
* **Core Technologies**: Roslyn SDK (`Microsoft.CodeAnalysis.CSharp.Workspaces`), NUnit for testing, StyleCop.Analyzers.

---

## 2. Core Commands

Always run these commands from the root directory (`C:\code\qowaiv-analyzers`):

* **Build Solution**:
  ```powershell
  dotnet build qowaiv-analyzers.slnx
  ```
* **Run Tests**:
  ```powershell
  dotnet test Specs\Specs.csproj
  ```

---

## 3. Directory Structure & Key Paths

* **`Qowaiv.CodeAnalysis.CSharp/`**: Main analyzer implementation.
  * **`Rules/`**: Analyzer implementations. Each rule is a class inheriting from `DiagnosticAnalyzer` (e.g., `PreventPrimitiveObsession.cs`).
  * **`Shared/`**: Shared code and shared models/helpers.
  * **`Syntax/`**: Custom syntax nodes and abstractions to simplify Roslyn syntax tree walking.
* **`Specs/`**: Testing suite.
  * **`Rules/`**: Analyzer verification tests.
  * **`Fixes/`**: Code fix provider verification tests.
  * **`Cases/`**: Code snippets in C# (`.cs`, `.ToFix.cs`, `.Fixed.cs`) containing expected/asserted diagnostics.
* **`rules/`**: Documentation files for each rule named `QWxxxx.md`.

---

## 4. How the Testing Model Works

The project uses a powerful testing framework that parses comments directly inside test case files to assert diagnostic outputs:

### 4.1. Analyzer Verification (`Specs/Cases/...`)
For analyzer rules, create a `.cs` file inside `Specs/Cases/` that uses custom comments to assert diagnostics:
* Use `// Noncompliant {{Message}}` on the line where a diagnostic is expected.
* Highlight the exact source code span on the line below using carets: `// ^^^^^^^^^^^^^`
* Use `// Compliant` or `// Compliant {{Reason}}` for positive cases.

Example case file (`Specs/Cases/MyRule.cs`):
```csharp
namespace Noncompliant
{
    public class NotSealedClass { } // Noncompliant {{Seal this class or make it explicitly inheritable.}}
    //           ^^^^^^^^^^^^^^
}
```

Corresponding Spec class (`Specs/Rules/My_Rule.cs`):
```csharp
namespace Rules.My_Rule;

public class Verify
{
    [Test]
    public void Rule()
        => new MyRuleAnalyzer()
        .ForCS()
        .AddSource(@"Cases/MyRule.cs")
        .Verify();
}
```

### 4.2. Code Fix Verification (`Specs/Fixes/...`)
To verify a code fix, define two files:
1. `Specs/Cases/MyRule.ToFix.cs`: The original non-compliant code.
2. `Specs/Cases/MyRule.Fixed.cs`: The expected refactored code after applying the fix.

Corresponding Spec class (`Specs/Fixes/My_Rule_Fix.cs`):
```csharp
namespace Fixes.My_Rule_Fix;

public class Fixes
{
    [Test]
    public void Code()
        => new MyRuleAnalyzer()
        .ForCS()
        .AddSource(@"Cases/MyRule.ToFix.cs")
        .ForCodeFix<MyRuleCodeFixProvider>()
        .AddSource(@"Cases/MyRule.Fixed.cs")
        .Verify();
}
```

---

## 5. Coding Conventions & Best Practices

1. **Follow StyleCop**: The codebase has strict style guidelines. Avoid warnings such as `SA1507` (multiple blank lines in a row), `SA1000` (missing spaces after keywords like `if`), etc.
2. **Be Selective**: Only target necessary syntax nodes in your analyzers. Walk syntax tree nodes efficiently.
3. **Register New Rules**:
   - Add documentation file in `rules/QWxxxx.md`.
   - Update `README.md` to reference the new rule.
   - Maintain the diagnostic severity and category conventions.
