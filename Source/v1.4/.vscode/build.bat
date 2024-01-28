echo off

REM remove unnecessary assemblies
DEL .\..\..\1.4\Assemblies\*.*

REM build dll
dotnet build .vscode