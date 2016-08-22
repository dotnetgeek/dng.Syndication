var target = Argument("target", "Default");

#tool "xunit.runner.console"


Task("Clean")
	.Does(() =>
{
	CleanDirectories("./src/**/obj");
	CleanDirectories("./src/**/bin");
	CleanDirectories("./tests/**/bin");
	CleanDirectories("./tests/**/obj");
	CleanDirectory("./output");
});

Task("Restore-NuGet-Packages")
  .IsDependentOn("Clean")
  .Does(() =>
{

  NuGetRestore("./dng.Syndication.sln");
});

Task("Build")
.IsDependentOn("Restore-NuGet-Packages")
.Does(() =>
{
	MSBuild("./src/dng.Syndication.csproj", new MSBuildSettings()
	.SetConfiguration("Release")
	.SetMSBuildPlatform(MSBuildPlatform.Automatic)
	.SetVerbosity(Verbosity.Minimal)
	.WithProperty("OutDir", "./../output/buildresults/4-6-2")
	);
});

Task("Test")
  .IsDependentOn("Build")
  .Does(() => 
  {

	MSBuild("./tests/dng.Syndication.Tests.csproj", new MSBuildSettings()
	.SetConfiguration("Release")
	.SetMSBuildPlatform(MSBuildPlatform.Automatic)
	.SetVerbosity(Verbosity.Minimal)
	);

	var xunitReport = Directory("./output/xunit/4-6-2");

	CreateDirectory(xunitReport);
 
	
	XUnit2("./tests/**/bin/Release/*.Tests.dll", new XUnit2Settings 
	{
	  ToolPath = "./tools/xunit.runner.console/tools/xunit.console.x86.exe",	
	  HtmlReport = true,
	  OutputDirectory = xunitReport
	});
  }).Finally(() => 
  {
  });


Task("Build-4-5-2")
.IsDependentOn("Restore-NuGet-Packages")
.Does(() =>
{
	MSBuild("./src/dng.Syndication.csproj", new MSBuildSettings()
	.SetConfiguration("Release-4-5-2")
	.SetMSBuildPlatform(MSBuildPlatform.Automatic)
	.SetVerbosity(Verbosity.Minimal)
	.WithProperty("OutDir", "./../output/buildresults/4-5-2")
	);
});

Task("Test-4-5-2")
  .IsDependentOn("Build-4-5-2")
  .Does(() => 
  {

	MSBuild("./tests/dng.Syndication.Tests.csproj", new MSBuildSettings()
	.SetConfiguration("Release-4-5-2")
	.SetMSBuildPlatform(MSBuildPlatform.Automatic)
	.SetVerbosity(Verbosity.Minimal)
	);

	var xunitReport = Directory("./output/xunit/4-5-2");

	CreateDirectory(xunitReport);
 
	
	XUnit2("./tests/**/bin/Release-4-5-2/*.Tests.dll", new XUnit2Settings 
	{
	  ToolPath = "./tools/xunit.runner.console/tools/xunit.console.x86.exe",	
	  HtmlReport = true,
	  OutputDirectory = xunitReport
	});
  }).Finally(() => 
  {
  });

Task("Pack")
  .IsDependentOn("Test")
  .IsDependentOn("Test-4-5-2")
  .Does(() =>
  { 
	CreateDirectory("./output/nuget");

	NuGetPack("./dng.Syndication.nuspec", new NuGetPackSettings
	{ 
	  OutputDirectory = "./output/nuget"
	});
  });

Task("Default").IsDependentOn("Pack");

RunTarget(target);
