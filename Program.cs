global using DotnetHelperTool.Helpers;
global using DotnetHelperTool.Setup;
global using System.CommandLine;
using DotnetHelperTool;


var builder = new CLIBuilder("Collection of simple CLI commands");

await builder.RegisterConverterSetups()
    .Build()
    .ExecuteAsync(args);
