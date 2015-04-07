// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------

#r @"bin/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Git
open Fake.AssemblyInfoFile
open Fake.ReleaseNotesHelper
open XUnit2Helper
open System
open System.IO

let project = "ConcurrencyUtils"
//let summary = "Utilities for performing efficient concurrent operations"
//let description = "This library provides a set of classes useful for performing efficient concurrent operations. Includes a port of Java's LongAdder and Striped64 classes"
//let authors = [ "Iulian Margarintescu" ]
//let tags = "concurrency, utilities, LongAdder, AtomicLong, Striped64, Volatile"

let solutionFile  = "ConcurrencyUtilities.sln"
let assemblyInfoFile = ".\SharedAssemblyInfo.cs"

let testAssemblies = "bin/Release/*Tests*.dll"

let gitOwner = "etishor" 
let gitHome = "https://github.com/" + gitOwner
let gitName = project
let gitRaw = environVarOrDefault "gitRaw" "https://raw.github.com/etishor"

let release = LoadReleaseNotes "CHANGELOG.md"

Target "Clean" (fun _ ->
    CleanDirs ["bin\Debug"; "bin\Release"; "packages"]
)

Target "RestoreNuget" <| fun _ -> RestorePackages()

// Generate assembly info files with the right version & up-to-date information
Target "AssemblyInfo" <| fun _ ->
    CreateCSharpAssemblyInfo assemblyInfoFile
            [Attribute.Company "Iulian Margarintescu"
             Attribute.Copyright ("Copyright Iulian Margarintescu © " + DateTime.Now.Year.ToString())
             Attribute.Product project
             Attribute.Description "Utilities for performing efficient concurrent operations"
             Attribute.Culture ""
             Attribute.ComVisible false
             Attribute.Version release.AssemblyVersion
             Attribute.FileVersion release.AssemblyVersion]


let buildWithParams properties =
    solutionFile
    |> build (fun p ->
    { p with
        Verbosity = Some(MSBuildVerbosity.Minimal)
        Targets = ["Rebuild"]
        Properties = properties
    })

Target "BuildDebug" <| fun _ -> buildWithParams ["Configuration", "Debug"]
Target "Build" <| fun _ -> buildWithParams ["Configuration", "Release"]

let findXunit =
    try
        !!(@".\packages\xunit.runner.console.2.0.0*\tools\xunit.console.exe") |> Seq.exactlyOne
    with
    | _ ->
        CleanDir "packages"
        Run "RestoreNuget"
        !!(@".\packages\xunit.runner.console.2.0.0*\tools\xunit.console.exe") |> Seq.exactlyOne

Target "RunTests" (fun _ ->
    !! testAssemblies
    |> xUnit2 (fun p ->
        { p with
            ToolPath = findXunit
            MaxThreads = 4
            TimeOut = TimeSpan.FromMinutes 20.
            Parallel = ParallelOption.Collections })
)

Target "NuGet" <| fun _ ->
    ensureDirectory "./bin/Release/NuGet/"
    ensureDirectory "./bin/NuGet/"

    for nuspec in !! "./src/**/*.nuspec" do
        printfn "Creating nuget packages for %s" nuspec

        let project = Path.GetFileNameWithoutExtension nuspec
        let projectDir = Path.GetDirectoryName nuspec
        let projectFile = (!! (projectDir @@ "*.*sproj")) |> Seq.exactlyOne

        let projectDllName = Path.GetFileNameWithoutExtension projectFile
        let packages = projectDir @@ "packages.config"
        let packageDependencies = if (fileExists packages) then (getDependencies packages) else []
    
        NuGet (fun p ->
            {p with
                Authors = ["Recognos Romania"]
                Project = project
                OutputPath = "./bin/NuGet/"
                Summary = "Data Extraction Platform"
                WorkingDir = "./bin/Release/NuGet"
                Version = release.NugetVersion
                Dependencies = packageDependencies
                Publish = false })
                nuspec

Target "All" DoNothing

"Clean"
  ==> "RestoreNuget"
  ==> "AssemblyInfo"
  ==> "Build"
  ==> "RunTests"
  ==> "NuGet"
  ==> "All"

RunTargetOrDefault "All"