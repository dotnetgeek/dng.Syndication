//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var artifactsDir = "./artifacts"; 
var testResultDir = artifactsDir + "/test-results";
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
//var buildDir = Directory("./src/Example/bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    // Clean solution directories.
    Information("Cleaning directories and files");

    CleanDirectories("./src/**/obj");
	CleanDirectories("./src/**/bin");
	CleanDirectories("./tests/**/bin");
	CleanDirectories("./tests/**/obj");
	CleanDirectories(artifactsDir);
    CleanDirectories(testResultDir);
});


Task("Prepare")
 .IsDependentOn("Clean")
.Does(()=> 
{
    CreateDirectory(artifactsDir);
});


Task("Restore-NuGet-Packages")
    .IsDependentOn("Prepare")
    .Does(() =>
{
    DotNetCoreRestore("./",new DotNetCoreRestoreSettings
    {
        Sources = new [] {
            "https://api.nuget.org/v3/index.json"
        }
    });
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild("./src/dng.Syndication.csproj", settings =>
        settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild("./src/dng.Syndication.csproj", settings =>
        settings.SetConfiguration(configuration));
    }
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    var projects = GetFiles("./tests/**/*.Tests.csproj");
    foreach(var project in projects)
    {
        Console.Write(project);
        DotNetCoreTest (project.ToString(), new DotNetCoreTestSettings  {
            ArgumentCustomization = args =>
                    args.Append("--logger ")
                    .Append("trx;LogFileName=" +
                        System.IO.Path.Combine(
                            MakeAbsolute(Directory(artifactsDir)).FullPath, 
                            project.GetFilenameWithoutExtension().FullPath + ".trx"))
        });
    }
});

Task("Pack")
    .IsDependentOn("Test")
    .Does(() => {
        DotNetCorePack("./src/dng.Syndication.csproj", new DotNetCorePackSettings
        {
            Configuration = "Release",
            OutputDirectory = artifactsDir,
            NoBuild = true
        });
    });

Task("Publish").
    IsDependentOn("Pack").Does(()=> {

    var nugetApiKey = EnvironmentVariable("nuget-apikey") ?? "";
    var nugetApiServer = EnvironmentVariable("nuget-server") ?? "";

    if (string.IsNullOrEmpty(nugetApiKey))
        throw new InvalidOperationException("Could not resolve Nuget API key.");

    if (string.IsNullOrEmpty(nugetApiServer))
        throw new InvalidOperationException("Could not resolve Nuget Server.");

    var packages = GetFiles("./artifacts/*.nupkg");
    foreach(var package in packages)
    {
        NuGetPush(package, new NuGetPushSettings {
            ApiKey = nugetApiKey,
            Source = nugetApiServer
        });
    }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default").IsDependentOn("Test");

RunTarget(target);
