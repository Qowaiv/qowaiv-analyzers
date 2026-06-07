param($installPath, $toolsPath, $package, $project)

if ($project.Object.AnalyzerReferences -eq $null) {
    throw 'This package cannot be installed without an analyzer reference.'
}
if ($project.Type -ne "C#") {
    throw 'This package can only be installed on C# projects.'
}

$analyzersPath = Split-Path -Path $toolsPath -Parent
$analyzersPath = Join-Path $toolsPath "analyzers"
$analyzerFilePath = Join-Path $analyzersPath "Qowaiv.CodeAnalysis.CSharp.dll"
$project.Object.AnalyzerReferences.Add($analyzerFilePath)
