@echo off

pushd %~dp0

if not exist ".\Bin\FAKE\tools\FAKE.exe" (
	.nuget\NuGet.exe install FAKE -OutputDirectory Bin -ExcludeVersion
)

set encoding=utf-8

.\Bin\FAKE\tools\FAKE.exe build.fsx %*

popd