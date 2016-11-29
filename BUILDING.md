# Building CenturyLink Cloud .NET SDK

There are two ways to build the SDK: using the solution file in an IDE or through the build script.

## Version Updatdate

Versioning of the solution should follow [semeantic versioning](http://semver.org/). When you update the
version number you need to update the versin number in the following files:
    * build.cake 
    * SDKVersion.cs
When updating the AssemblyVersion and AssemblyFileVersion attributes in SDKVersion.cs make sure that you
you use this the semantic version appended with '.0. For example, if the sematic version is 1.1.0 then
the AssemblyFileVersion and AssemblyVersion attributes should be set to 1.1.0.0.

## Solution Build

The SDK is built using a single Visual Studio solution (CenturyLinkCloudSDK.sln on Windows
and), which may be built with Visual Studio 2015 onwards.

The solutions all place their output in a common bin directory in subdirectories
called net4.5 and portable. Future platform builds will cause new subdirectories to be created.

## Build Script

We use **Cake** (http://cakebuild.net) to build the SDK for distribution. The primary script that controls
building, running tests and packaging is build.cake. We modify build.cake when we need to add new
targets or change the way the build is done. Normally build.cake is not invoked directly but through
build.ps1 (on Windows). These two scripts are provided by the Cake project and ensure that Cake is 
properly installed before trying to run the cake script. This helps the build to work on CI servers 
using newly created agents to run the build and we generally run it the same way on our own machines.

The build shell script and build.cmd script are provided as an easy way to run the above commands.
In addition to passing their arguments through to build.cake, they can supply added arguments
through the CAKE_ARGS environment variable. The rest of this document will assume use of these commands.

There is one case in which use of the CAKE_ARGS environment variable will be essential, if not necessary.
If you are running builds on a 32-bit Windows system, you must always supply the -Experimental argument
to the build. Use set CAKE_ARGS=-Experimental to ensure this is always done and avoid having to type
it out each time.

Key arguments to build.cmd / build:
 * -Target, -t <task>                 The task to run - see below.
 * -Configuration, -c [Release|Debug] The configuration to use (default is Release)
 * -ShowDescription                   Shows all of the build tasks and their descriptions
 * -Experimental, -e                  Use the experimental build of Roslyn

The build.cake script contains a large number of interdependent tasks. The most
important top-level tasks to use are listed here:

```
 * Build               Builds everything. This is the default if no target is given.
 * Rebuild             Cleans the output directory and builds everything
 * Test                Runs all tests. Dependent on Build.
 * Test45              Tests the 4.5 SDK without building first.
 * TestPortable        Tests the portable SDK without building first.
 * Package             Creates all packages without building first. See Note below.
```

For a full list of tasks, run `build.cmd -ShowDescription`.

## Creating Nu-Get Package
 
To build and make available a new package do the following:

* Ensure the version numbers have been updated
* Build the solution using the build script:
```
    .\buils.ps1 -t Build
``` 
* Package the resulting build using this command:
```
    .\build.ps1 -t Package
```
* The NuGet package file will be save to the **package** folder.
* Upload the resulting CenturyLink.Cloud.SDK.*.*.*.*.nupkg file to NuGet

### Notes:
 1. By design, the Package target does not depend on Build. This is to allow re-packaging
    when necessary without changing the binaries themselves. Of course, this means that
    you have to be very careful that the build is up to date before packaging.

 2. For additional targets, refer to the build.cake script itself.