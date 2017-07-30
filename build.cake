#addin Cake.SemVer

// Enviroment
var isRunningBitrise = Bitrise.IsRunningOnBitrise;
var isRunningOnWindows = IsRunningOnWindows();

// Arguments.
var target = Argument("target", "Default");
var iOSOutputDirectory = "bin/iPhoneSimulator";

var configuration = "Release";

// Define directories.
var solutionFile = new FilePath("Xamarin.LinkedIn.sln");
var androidLibrary = GetFiles("./src/Xamarin.Android.LinkedIn/*.csproj").First();
var iOSLibrary = GetFiles("./src/Xamarin.iOS.LinkedIn/*.csproj").First();
var androidSample = GetFiles("./src/samples/Xamarin.Android.LinkedIn.Sample/*.csproj").First();
var iOSSample = GetFiles("./src/samples/Xamarin.iOS.LinkedIn.Sample/*.csproj").First();
var artifactsDirectory = new DirectoryPath("artifacts");

// Versioning. Used for all the packages and assemblies for now.
var version = CreateSemVer(0, 1, 0);

Setup((context) =>
{
	Information("Bitrise: {0}", isRunningBitrise);
	Information ("Running on Windows: {0}", isRunningOnWindows);
	Information("Configuration: {0}", configuration);
});

Task("Clean")
	.Does(() =>
	{	
		CleanDirectory(artifactsDirectory);

		DotNetBuild(solutionFile, settings => settings
				.SetConfiguration(configuration)
				.WithTarget("Clean")
				.SetVerbosity(Verbosity.Minimal));
	});

Task("Restore")
	.Does(() => 
	{
		NuGetRestore(solutionFile);
	});

Task("Build")
	.IsDependentOn("Build-Android-Library")
	.IsDependentOn("Build-iOS-Library")
	.IsDependentOn("Build-Samples")
	.Does(() => {});

Task("Build-Samples")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() =>  
	{	
		DotNetBuild(androidSample, settings => settings
					.SetConfiguration(configuration)
					.WithTarget("Build")
					.SetVerbosity(Verbosity.Minimal));

		DotNetBuild(iOSSample, settings => settings
					.SetConfiguration(configuration)
					.WithTarget("Build")
					.WithProperty("Platform", "iPhoneSimulator")
					.WithProperty("OutputPath", iOSOutputDirectory)
					.WithProperty("TreatWarningsAsErrors", "false")	
					// For some strange reason, this compiles fine in iPhoneSimulator without AllowUnsafeBlocks from the IDE but here it just won't compile witout it.
					.WithProperty("AllowUnsafeBlocks", "true")	
					.SetVerbosity(Verbosity.Minimal));
	});

Task("Build-Android-Library")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() =>  
	{	
		DotNetBuild(androidLibrary, settings => settings
					.SetConfiguration(configuration)
					.WithTarget("Build")
					.SetVerbosity(Verbosity.Minimal));
	});

Task("Build-iOS-Library")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() =>  
	{	
		DotNetBuild(iOSLibrary, settings => settings
					.SetConfiguration(configuration)
					.WithTarget("Build")
					.SetVerbosity(Verbosity.Minimal));
	});	

Task ("NuGet")
	.IsDependentOn("Build")
	.WithCriteria(isRunningBitrise)
	.Does (() =>
	{
		Information("Nuget version: {0}", version);
		
  		var nugetVersion = Bitrise.Environment.Repository.GitBranch == "master" ? version.ToString() : version.Change(prerelease: "pre" + Bitrise.Environment.Build.BuildNumber).ToString();

		NuGetPack ("./nuspec/Xamarin.Android.LinkedIn.nuspec", 
			new NuGetPackSettings 
				{ 
					Version = nugetVersion,
					Verbosity = NuGetVerbosity.Normal,
					OutputDirectory = artifactsDirectory,
					BasePath = "./",
					ArgumentCustomization = args => args.Append("-NoDefaultExcludes")		
				});	

		NuGetPack ("./nuspec/Xamarin.iOS.LinkedIn.nuspec", 
			new NuGetPackSettings 
				{ 
					Version = nugetVersion,
					Verbosity = NuGetVerbosity.Normal,
					OutputDirectory = artifactsDirectory,
					BasePath = "./",
					ArgumentCustomization = args => args.Append("-NoDefaultExcludes")		
				});	
	});

Task("Default")
	.IsDependentOn("NuGet")
	.Does(() => {});

RunTarget(target);