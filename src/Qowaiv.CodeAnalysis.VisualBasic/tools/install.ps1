param($installPath, $toolsPath, $package, $project)

if ($project.Object.AnalyzerReferences -eq $null) {
    throw 'This package cannot be installed without an analyzer reference.'
}
if ($project.Type -ne "VB.NET") {
    throw 'This package can only be installed on "VB.NET" projects.'
}

$analyzersPath = Split-Path -Path $toolsPath -Parent
$analyzersPath = Join-Path $toolsPath "analyzers"
$analyzerFilePath = Join-Path $analyzersPath "Qowaiv.CodeAnalysis.VisualBasic.dll"
$project.Object.AnalyzerReferences.Add($analyzerFilePath)
