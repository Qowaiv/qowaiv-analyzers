param($installPath, $toolsPath, $package, $project)

$analyzersPath = Split-Path -Path $toolsPath -Parent
$analyzersPath = Join-Path $toolsPath "analyzers"
$analyzerFilePath = Join-Path $analyzersPath "Qowaiv.CodeAnalysis.CSharp.dll"
try {
    $project.Object.AnalyzerReferences.Remove($analyzerFilePath)
}
catch {
}
