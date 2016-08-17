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
	.WithProperty("OutDir", "./../output/buildresults")
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

	var xunitReport = Directory("./output/xunit");

	CreateDirectory(xunitReport);
 
	
	XUnit2("./tests/**/bin/Release/*.Tests.dll", new XUnit2Settings 
	{
	  ToolPath = "./tools/xunit.runner.console/tools/xunit.console.x86.exe",	
	  HtmlReport = true,
	  OutputDirectory = xunitReport
	});
  }).Finally(() => 
  {
	//ReportUnit(xunitReport);
  });


Task("Pack")
  .IsDependentOn("Test")
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
