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

Task("Publish")
    .IsDependentOn("Test")
    .Does(() => {
        DotNetCorePack("./src/dng.Syndication.csproj", new DotNetCorePackSettings
        {
            Configuration = "Release",
            OutputDirectory = artifactsDir,
            NoBuild = true
        });
    });

Task("Push").
    IsDependentOn("Publish").Does(()=> {

    var nugetServer = EnvironmentVariable("nuget-server") ?? "";
    var nugetApiKey = EnvironmentVariable("nuget-apikey") ?? "";
    if (string.IsNullOrEmpty(nugetServer))
    {
        Console.Write("Nuget-Server not definied." + System.Environment.NewLine);
        return;
    }

    if (string.IsNullOrEmpty(nugetApiKey))
    {
        Console.Write("Nuget-Api not definied." + System.Environment.NewLine);
        return;
    }

    var packages = GetFiles("./artifacts/*.nupkg");
    foreach(var package in packages)
    {
        Console.Write(package);
        NuGetPush(package, new NuGetPushSettings {
            Source = nugetServer,
            ApiKey = nugetApiKey
        });
    }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default").IsDependentOn("Test");

RunTarget(target);
