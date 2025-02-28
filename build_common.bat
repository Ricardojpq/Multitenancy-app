dotnet restore Common\Utils --interactive 
dotnet build Common\Utils\Utils.sln -c release
dotnet restore Common\SharedKernel --interactive 
dotnet build Common\SharedKernel\SharedKernel.sln -c release
dotnet restore Common\ResourcesLibrary --interactive 
dotnet build Common\ResourcesLibrary\ResourcesLibrary.sln -c release
dotnet restore Common\FileManagement --interactive
dotnet build Common\FileManagement\FileManagement.sln -c release
dotnet restore Common\ViewModels --interactive
dotnet build Common\ViewModels\ViewModels.sln -c release
dotnet restore Common\WebUtils --interactive
dotnet build Common\WebUtils\WebUtils.sln -c release