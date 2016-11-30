//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");   

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var ErrorDetail = new List<string>();

//////////////////////////////////////////////////////////////////////
// SET PACKAGE VERSION
//////////////////////////////////////////////////////////////////////

var version = "0.9.2";
var modifier = "";

var isAppveyor = BuildSystem.IsRunningOnAppVeyor;
var dbgSuffix = configuration == "Debug" ? "-dbg" : "";
var packageVersion = version + modifier + dbgSuffix;

//////////////////////////////////////////////////////////////////////
// SUPPORTED FRAMEWORKS
//////////////////////////////////////////////////////////////////////

var WindowsFrameworks = new string[] {
    "net-4.5", "portable" };

var LinuxFrameworks = new string[] {
    "net-4.5" };

var AllFrameworks = IsRunningOnWindows() ? WindowsFrameworks : LinuxFrameworks;
//var AllFrameworks = WindowsFrameworks;

//////////////////////////////////////////////////////////////////////
// DEFINE RUN CONSTANTS
//////////////////////////////////////////////////////////////////////

var PROJECT_DIR = Context.Environment.WorkingDirectory.FullPath + "/";
var PACKAGE_DIR = PROJECT_DIR + "package/";
var BIN_DIR = PROJECT_DIR + "bin/" + configuration + "/";
var IMAGE_DIR = PROJECT_DIR + "images/";

var SOLUTION_FILE = IsRunningOnWindows()
    ? "./CenturyLinkCloudSDK.sln"
    : "./CenturyLinkCloudSDK.linux.sln";
//var SOLUTION_FILE = "./CenturyLinkCloudSDK.sln";

// Package sources for nuget restore
var PACKAGE_SOURCE = new string[]
    {
        "https://www.nuget.org/api/v2",
        //  "https://www.myget.org/F/nunit/api/v2"
};

// Test Assemblies
var SDK_TESTS = "CenturyLinkCloudSDK.Tests.dll";
var INTEGRATION_TESTS = "CenturyLinkCloudSDK.IntegrationTests.dll";

// Packages
var SRC_PACKAGE = PACKAGE_DIR + "CenturyLinkCloudSDK-" + version + modifier + "-src.zip";
var ZIP_PACKAGE = PACKAGE_DIR + "CenturyLinkCloudSDK-" + packageVersion + ".zip";

bool isDotNetCoreInstalled = false;

var packages = new string[]{
    "src/CenturyLinkCloudSDK/packages.config",
    "src/CenturyLinkCloudSDK.Integration.Tests/packages.config",
    "src/CenturyLinkCloudSDK.Tests/packages.config",
};

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(context =>
{
    Information("Building version {0} of CenturyLink Cloud SDK for .NET.", packageVersion);

    isDotNetCoreInstalled = CheckIfDotNetCoreInstalled();
});

//////////////////////////////////////////////////////////////////////
// CLEAN
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Description("Deletes all files in the BIN directory")
    .Does(() =>
    {
        CleanDirectory(BIN_DIR);
        CleanDirectory(PACKAGE_DIR);
        CleanDirectory(IMAGE_DIR);
});

//////////////////////////////////////////////////////////////////////
// INITIALIZE FOR BUILD
//////////////////////////////////////////////////////////////////////

Task("InitializeBuild")
    .Description("Initializes the build")
    .Does(() =>
    {
        foreach(var package in packages)
        {
            Information("Restoring NuGet package " + package);
            NuGetRestore(package, new NuGetRestoreSettings
            {
                PackagesDirectory = "./packages/",
                Source = PACKAGE_SOURCE
            });
        }

        if(isDotNetCoreInstalled)
        {
            Information("Restoring .NET Core packages");
            StartProcess("dotnet", new ProcessSettings
            {
                Arguments = "restore"
            });
        }

        if (isAppveyor)
        {
            var tag = AppVeyor.Environment.Repository.Tag;

            if (tag.IsTag)
            {
                packageVersion = tag.Name;
            }
            else
            {
                var buildNumber = AppVeyor.Environment.Build.Number.ToString("00000");
                var branch = AppVeyor.Environment.Repository.Branch;
                var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;

                if (branch == "master" && !isPullRequest)
                {
                    packageVersion = version + "-dev-" + buildNumber + dbgSuffix;
                }
                else
                {
                    var suffix = "-ci-" + buildNumber + dbgSuffix;

                    if (isPullRequest)
                        suffix += "-pr-" + AppVeyor.Environment.PullRequest.Number;
                    else if (AppVeyor.Environment.Repository.Branch.StartsWith("release", StringComparison.OrdinalIgnoreCase))
                        suffix += "-pre-" + buildNumber;
                    else
                        suffix += "-" + branch;

                    // Nuget limits "special version part" to 20 chars. Add one for the hyphen.
                    if (suffix.Length > 21)
                        suffix = suffix.Substring(0, 21);

                    packageVersion = version + suffix;
                }
            }

            AppVeyor.UpdateBuildVersion(packageVersion);
        }
});

//////////////////////////////////////////////////////////////////////
// BUILD SDK
//////////////////////////////////////////////////////////////////////

Task("Build45")
    .Description("Builds the .NET 4.5 version of the SDK")
    .Does(() =>
    {
        BuildProject("src/CenturyLinkCloudSDK/CenturyLinkCloudSDK-4.5.csproj", configuration);
        if (IsRunningOnWindows())
        {
            BuildProject("src/CenturyLinkCloudSDK.Tests/CenturyLinkCloudSDK.Tests-4.5.csproj", configuration);
            BuildProject("src/CenturyLinkCloudSDK.Integration.Tests/CenturyLinkCloudSDK.IntegrationTests-4.5.csproj", configuration);
        }
});

Task("BuildPortable")
    .Description("Builds the PCL version of the SDK")
    .WithCriteria(IsRunningOnWindows())
    .Does(() =>
    {
        BuildProject("src/CenturyLinkCloudSDK/CenturyLinkCloudSDK-portable.csproj", configuration);
        BuildProject("src/CenturyLinkCloudSDK.Tests/CenturyLinkCloudSDK.Tests-portable.csproj", configuration);
        BuildProject("src/CenturyLinkCloudSDK.Integration.Tests/CenturyLinkCloudSDK.IntegrationTests-portable.csproj", configuration);
});

//////////////////////////////////////////////////////////////////////
// TEST
//////////////////////////////////////////////////////////////////////

Task("CheckForError")
    .Description("Checks for errors running the test suites")
    .Does(() => CheckForError(ref ErrorDetail));

//////////////////////////////////////////////////////////////////////
// TEST FRAMEWORK
//////////////////////////////////////////////////////////////////////

Task("Test45")
    .Description("Tests the .NET 4.5 version of the SDK")
    .IsDependentOn("Build45")
    .OnError(exception => { ErrorDetail.Add(exception.Message); })
    .Does(() =>
    {
        var runtime = "net-4.5";
        var dir = BIN_DIR + runtime + "/";
        RunTest(dir, SDK_TESTS, runtime, ref ErrorDetail);
        //RunTest(dir, INTEGRATION_TESTS, runtime, ref ErrorDetail);
});

Task("TestPortable")
    .Description("Tests the PCL version of the SDK")
    .WithCriteria(IsRunningOnWindows())
    .IsDependentOn("BuildPortable")
    .OnError(exception => { ErrorDetail.Add(exception.Message); })
    .Does(() =>
    {
        var runtime = "portable";
        var dir = BIN_DIR + runtime + "/";
        RunTest(dir, SDK_TESTS, runtime, ref ErrorDetail);
        //RunTest(dir, INTEGRATION_TESTS, runtime, ref ErrorDetail);
});

//////////////////////////////////////////////////////////////////////
// PACKAGE
//////////////////////////////////////////////////////////////////////

var RootFiles = new FilePath[]
{
    "LICENSE"
    //"NOTICES.txt",
    //"CHANGES.txt"
};

var FrameworkFiles = new FilePath[]
{
    "CenturyLinkCloudSDK.dll"
};

Task("PackageSource")
    .Description("Creates a ZIP file of the source code")
    .Does(() =>
    {
        CreateDirectory(PACKAGE_DIR);
        RunGitCommand(string.Format("archive -o {0} HEAD", SRC_PACKAGE));
});

Task("CreateImage")
    .Description("Copies all files into the image directory")
    .Does(() =>
    {
        var currentImageDir = IMAGE_DIR + "CenturyLinkCloudSDK-" + packageVersion + "/";
        var imageBinDir = currentImageDir + "bin/";

        CleanDirectory(currentImageDir);

        CopyFiles(RootFiles, currentImageDir);

        CreateDirectory(imageBinDir);
        Information("Created directory " + imageBinDir);

        foreach (var runtime in AllFrameworks)
        {
            var targetDir = imageBinDir + Directory(runtime);
            var sourceDir = BIN_DIR + Directory(runtime);
            CreateDirectory(targetDir);
            foreach (FilePath file in FrameworkFiles)
            {
                var sourcePath = sourceDir + "/" + file;
                if (FileExists(sourcePath))
                    CopyFileToDirectory(sourcePath, targetDir);
            }
        }
});

Task("PackageFramework")
    .Description("Creates NuGet packages of the frameSDKwork")
    .IsDependentOn("CreateImage")
    .Does(() =>
    {
        var currentImageDir = IMAGE_DIR + "CenturyLinkCloudSDK-" + packageVersion + "/";

        CreateDirectory(PACKAGE_DIR);

        NuGetPack("nuget/CenturyLinkCloudSDK.nuspec", new NuGetPackSettings()
        {
            Version = packageVersion,
            BasePath = currentImageDir,
            OutputDirectory = PACKAGE_DIR
        });
});

Task("PackageZip")
    .Description("Creates a ZIP file of the SDK")
    .IsDependentOn("CreateImage")
    .Does(() =>
    {
        CreateDirectory(PACKAGE_DIR);

        var currentImageDir = IMAGE_DIR + "CenturyLinkCloudSDK-" + packageVersion + "/";

        var zipFiles =
            GetFiles(currentImageDir + "*.*") +
            GetFiles(currentImageDir + "bin/net-4.5/*.*") +
            GetFiles(currentImageDir + "bin/portable/*.*");
        Zip(currentImageDir, File(ZIP_PACKAGE), zipFiles);
});

//////////////////////////////////////////////////////////////////////
// UPLOAD ARTIFACTS
//////////////////////////////////////////////////////////////////////

Task("UploadArtifacts")
    .Description("Uploads artifacts to AppVeyor")
    .IsDependentOn("Package")
    .Does(() =>
    {
        UploadArtifacts(PACKAGE_DIR, "*.nupkg");
        UploadArtifacts(PACKAGE_DIR, "*.zip");
});

//////////////////////////////////////////////////////////////////////
// SETUP AND TEARDOWN TASKS
//////////////////////////////////////////////////////////////////////

Teardown(context => CheckForError(ref ErrorDetail));

//////////////////////////////////////////////////////////////////////
// HELPER METHODS - GENERAL
//////////////////////////////////////////////////////////////////////

bool CheckIfDotNetCoreInstalled()
{
    try
    {
        Information("Checking if .NET Core SDK is installed");
        StartProcess("dotnet", new ProcessSettings
        {
            Arguments = "--version"
        });
    }
    catch(Exception)
    {
        Warning(".NET Core SDK is not installed. It can be installed from https://www.microsoft.com/net/core");
        return false;
    }
    return true;
}

void RunGitCommand(string arguments)
{
    StartProcess("git", new ProcessSettings()
    {
        Arguments = arguments
    });
}

void UploadArtifacts(string packageDir, string searchPattern)
{
    foreach(var zip in System.IO.Directory.GetFiles(packageDir, searchPattern))
        AppVeyor.UploadArtifact(zip);
}

void CheckForError(ref List<string> errorDetail)
{
    if(errorDetail.Count != 0)
    {
        var copyError = new List<string>();
        copyError = errorDetail.Select(s => s).ToList();
        errorDetail.Clear();
        throw new Exception("One or more unit tests failed, breaking the build.\n"
                              + copyError.Aggregate((x,y) => x + "\n" + y));
    }
}

//////////////////////////////////////////////////////////////////////
// HELPER METHODS - BUILD
//////////////////////////////////////////////////////////////////////

void BuildProject(string projectPath, string configuration)
{
    DotNetBuild(projectPath, settings =>
        settings.SetConfiguration(configuration)
        .SetVerbosity(Verbosity.Minimal)
        .WithTarget("Build")
        .WithProperty("NodeReuse", "false"));
}

//////////////////////////////////////////////////////////////////////
// HELPER METHODS - TEST
//////////////////////////////////////////////////////////////////////

void RunTest(DirectoryPath workingDir, string testAssembly, string framework, ref List<string> errorDetail)
{
    var testFile = workingDir + "/" + testAssembly;
    MSTest(testFile);
}

void RunDotnetCoreTests(FilePath exePath, DirectoryPath workingDir, string framework, ref List<string> errorDetail)
{
    RunDotnetCoreTests(exePath, workingDir, null, framework, ref errorDetail);
}

void RunDotnetCoreTests(FilePath exePath, DirectoryPath workingDir, string arguments, string framework, ref List<string> errorDetail)
{
    int rc = StartProcess(
        "dotnet",
        new ProcessSettings()
        {
            Arguments = exePath + " " + arguments,
            WorkingDirectory = workingDir
        });

    if (rc > 0)
        errorDetail.Add(string.Format("{0}: {1} tests failed", framework, rc));
    else if (rc < 0)
        errorDetail.Add(string.Format("{0} returned rc = {1}", exePath, rc));
}

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Rebuild")
    .Description("Rebuilds all versions of the SDK")
    .IsDependentOn("Clean")
    .IsDependentOn("Build");

Task("Build")
    .Description("Builds all versions of the SDK")
    .IsDependentOn("InitializeBuild")
    .IsDependentOn("Build45")
// NOTE: The following tasks use Criteria and will be skipped on Linux
    .IsDependentOn("BuildPortable");

Task("Test")
    .Description("Builds and tests all versions of the SDK")
    .IsDependentOn("Build")
    .IsDependentOn("Test45")
// NOTE: The following tasks use Criteria and will be skipped on Linux
    .IsDependentOn("TestPortable");

Task("Package")
    .Description("Packages all versions of the SDK")
    .IsDependentOn("CheckForError")
    .IsDependentOn("PackageFramework")
    .IsDependentOn("PackageZip");

Task("Appveyor")
    .Description("Builds, tests and packages on AppVeyor")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Package")
    .IsDependentOn("UploadArtifacts");

Task("Travis")
    .Description("Builds and tests on Travis")
    .IsDependentOn("Build")
    .IsDependentOn("Test");

Task("Default")
    .Description("Builds all versions of the framework")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);