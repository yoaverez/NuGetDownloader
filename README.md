# NuGetDownloader

`NuGetDownloader` is an application for downloading nugets packages (`.nupkg`) and all of their dependencies.

For example: <br/>
Given a packages `A, B, C, D` such that `A` depends on `B` and `B` depends on `C` and `D`.<br/>
If you run the `NuGetDownloader` to download package `A`, it will download packages `A, B, C, D` since `A` is **directly or indirecly** depends on all of them.

## Install
Download the latest release from: https://github.com/yoaverez/NuGetDownloader/releases.

## Usage

### Download a single or multiple packages and all of their dependencies
```shell
NuGetDownloader.exe run
```
This will download the packages listed in the `appsettings.json` that is located in your current working directory.

If you want to bring a different `appsettings` file you can add the `-p <appsettings-path>` like this:
```shell
NuGetDownloader.exe run -p "C:\Users\<your user>\Downloads\my-appsettings.json"
```

### Generate an example of an appsettings
```shell
NuGetDownloader.exe generate-config
```
This command will generate an example of the `appsettings` file
and put the example file in your current working directory.

If you want to put it in a different location you can add the `-p <appsettings-path>` like this:
```shell
NuGetDownloader.exe generate-config -p "C:\Users\<your user>\Downloads\my-appsettings.json"
```

### Appsettings file
This is an example of an appsettings file:
```json
{
  "OutputDirectoryPath": "./",
  "DownloadRequests": [
    {
      "PackageName": "benchmarkdotnet",
      "PackageVersion": "0.13.12",
      "DownloadBuiltInLibraries": false
    }
  ]
}
```

#### The appsettings file has the following properties:
*   `OutputDirectoryPath` - The path to the directory in which to download all the packages.

    **Note that each package (and it's dependencies) that you wish to download will be in a different directory inside the `OutputDirectoryPath`.**
    
    For example, if you want to download the `benchmarkdotnet` package with version `0.13.12` all the packages will be downloaded to: `OutputDirectoryPath/benchmarkdotnet.0.13.12` directory.

* `DownloadRequests` - A list of objects. Each object represent a single download request. A single download request has the following properties:
    * `PackageName` - The full name of the package that you want to download.
    
    * `PackageVersion` - The wanted version of the above package.

    * `DownloadBuiltInLibraries` - Indicated whether or not to download dependencies of a built-in libraries like `System` or `NetStandard` etc.