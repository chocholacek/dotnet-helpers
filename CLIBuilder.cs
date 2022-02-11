using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Reflection;

namespace DotnetHelperTool;

public class CLIBuilder
{
    private readonly RootCommand _rootCommand;

    private readonly List<Type> _setupTypes = new();
    private Parser? _parser;

    public CLIBuilder(string description)
    {
        _rootCommand = new(description);
    }

    public CLIBuilder RegisterConverterSetups() => RegisterConverterSetups(Assembly.GetEntryAssembly()!);

    public CLIBuilder RegisterConverterSetups<T>() => RegisterConverterSetups(typeof(T).Assembly);

    public CLIBuilder RegisterConverterSetups(Assembly assembly)
    {
        var setups = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ICLIToolSetup)) && !t.IsInterface && !t.IsAbstract);
        _setupTypes.AddRange(setups);
        return this;
    }

    public CLIBuilder Build()
    {
        foreach (var setupType in _setupTypes)
        {
            var setup = Activator.CreateInstance(setupType) as ICLIToolSetup;
            setup!.Register();
            setup!.Build(_rootCommand);
        }

        _parser = new CommandLineBuilder(_rootCommand)
            .UseDefaults()
            .Build();

        return this;
    }

    public Task ExecuteAsync(string[] args)
    {
        if (_parser is null)
        {
            throw new InvalidOperationException($"Builder must be built before running {nameof(ExecuteAsync)}");
        }

        return _parser.InvokeAsync(args);
    }
}