///https://github.com/appsourcers/ARKit-CoreLocation/blob/bb21ce273f49f42ffb471b3d582a235d9662e976/build.cake
//https://github.com/dotnet/Nerdbank.GitVersioning/ 
//https://cakebuild.net/api/Cake.Common.Tools.DotNetCore.Pack/DotNetCorePackSettings/A4C15415
//https://github.com/search?p=4&q=Cake.GitVersioning&type=Code
//https://github.com/clcrutch/posh-kentico/blob/5b80698884b8b295bfe887182d405fbaf32e832e/build.cake
//https://github.com/AleXr64/Telegram-bot-framework/blob/bbd24269028f2b14268cb071f4a6b8ec93e39751/build/build.cake
//https://github.com/gtbuchanan/repo-template-cs/blob/2ef15622fa41584d967c2e91e347dc327677795e/build.cake

#addin "Cake.GitVersioning&version=3.1.74" 
#addin "Cake.Figlet&version=1.3.1"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

DotNetCoreBuildSettings dotNetCoreBuildSettings; 
DotNetCoreMSBuildSettings msBuildSettings;


//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var artifactsDir = Directory(Argument("artifactsDir", EnvironmentVariable("BUILD_ARTIFACTDIR") ?? "./artifacts"));
var testResultDir = artifactsDir + Directory("test-results");

var gitVersion = GitVersioningGetVersion();

Setup(context =>
{
    Information(Figlet("dng.Syndication"));
    Information("Is Windows {0}", IsRunningOnWindows());

    msBuildSettings = new DotNetCoreMSBuildSettings()
        .SetInformationalVersion(gitVersion.AssemblyInformationalVersion.ToString())
        .SetFileVersion(gitVersion.AssemblyFileVersion.ToString())
        .SetVersion(gitVersion.Version.ToString())
        .WithProperty("PackageVersion", gitVersion.Version.ToString());

    dotNetCoreBuildSettings = new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        MSBuildSettings = msBuildSettings
    };
});

Teardown(context =>
{
    Information("Finished running tasks ✔");
});


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("dotnet-info")
    .Does(() =>
{
    Information("dotnet --info");
    StartProcess("dotnet", new ProcessSettings { Arguments = "--info" });
});

Task("Clean")
    .Does(() =>
{
    CleanDirectories("./src/**/obj");
	CleanDirectories("./src/**/bin");
	CleanDirectories("./tests/**/bin");
	CleanDirectories("./tests/**/obj");
	CleanDirectory(artifactsDir);
    CleanDirectory(testResultDir);

    MSBuild("./dng.Syndication.sln", c =>
		c.SetConfiguration(configuration)
            .WithTarget("Clean"));
});

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    DotNetCoreRestore("./",new DotNetCoreRestoreSettings
    {
        Sources = new [] {
            EnvironmentVariable("nuget-source") ?? "https://api.nuget.org/v3/index.json"
        }
    });
});

Task("Build")
    .IsDependentOn("dotnet-info")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
      DotNetCoreBuild("./src/dng.Syndication.csproj", dotNetCoreBuildSettings);
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    var projects = GetFiles("./tests/**/*.Tests.csproj");
    foreach(var project in projects)
    {
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
    .Does(() => 
{
    DotNetCorePack("./src/dng.Syndication.csproj", new DotNetCorePackSettings
    {
        Configuration = "Release",
        OutputDirectory = artifactsDir,
        NoBuild = true,
        MSBuildSettings = msBuildSettings
    });
});

Task("PushNuget")
    .IsDependentOn("Pack")
    .Does(() => 
{

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

Task("GetVersion")
    .Does(() =>
{
    Information("SemVer:" + gitVersion.SemVer2);
    Information("SemVer1:" + gitVersion.SemVer1);
    Information("AssemblyVersion:" + gitVersion.AssemblyVersion);
    Information("AssemblyFileVersion:" + gitVersion.AssemblyFileVersion);
    Information("AssemblyInformationalVersion:" + gitVersion.AssemblyInformationalVersion);
    Information("Version:" + gitVersion.Version);
    Information("NuGetPackageVersion:" + gitVersion.NuGetPackageVersion);

});


Task("Default").IsDependentOn("Test");

RunTarget(target);
