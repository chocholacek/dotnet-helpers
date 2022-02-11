# dotnet-helpers
## supported frameworks
* .NET 6
## how to install
1. run `dotnet pack -c Release`
2. run `dotnet tool install -g --add-source ./nupkg dotnet-helpers`
## verify installation
To verify installation run `dotnet-helpers --version`
## auto-completion
To use auto-completion install [dotnet-suggest](https://www.nuget.org/packages/dotnet-suggest) by running
```
dotnet tool install -g dotnet-suggest
```
and setup machine according to [this documentation](https://github.com/dotnet/command-line-api/blob/main/docs/dotnet-suggest.md)
