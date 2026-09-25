param($installPath, $toolsPath, $package, $project)

$analyzersPath = Join-Path $toolsPath "analyzers"
try {
    $project.Object.AnalyzerReferences.Remove((Join-Path $analyzersPath "Qowaiv.CodeAnalysis.CSharp.dll"))
    $project.Object.AnalyzerReferences.Remove((Join-Path $analyzersPath "Qowaiv.CodeAnalysis.CSharp.CodeFixes.dll"))
}
catch {
}
