public static class CommandExtensions
{
    public static Command AddCommands(this Command cmd, params Command[] subCommands) => cmd.AddSymbols(subCommands);

    public static Command AddOptions(this Command cmd, params Option[] options) => cmd.AddSymbols(options);

    public static Command AddArguments(this Command cmd, params Argument[] arguments) => cmd.AddSymbols(arguments);

    public static Option<T> MutuallyExclusiveWith<T>(this Option<T> option, Option other)
    {
        option.AddValidator(res =>
        {
            res.ErrorMessage = res.FindResultFor(other) switch
            {
                null => null,
                { } => $"Options {option.Aliases.First()}, {other.Aliases.First()} are mutually exclusive."
            };
        });
        
        return option;    
    }

    public static Option<T> MustBeUsedWith<T>(this Option<T> option, params Option[] opts)
    {
        option.AddValidator(res =>
        {
            res.ErrorMessage = (opts.Select(res.FindResultFor).Where(r => r is not null).Count() == opts.Count()) switch
            {
                true => null,
                false => $"Option {option.Aliases.First()} is can be only used with {string.Join(", ", opts.Select(o => o.Aliases.First()))}."
            };
        });

        return option;
    }

    public static Command OneOfIsrequired(this Command cmd, params Option[] opts)
    {
        cmd.AddValidator(res =>
        {
            res.ErrorMessage = opts.Select(res.FindResultFor).Where(t => t is not null).Count() switch
            {
                0 => $"One of the options {string.Join(", ", opts.Select(o => o.Aliases.First()))} is required.",
                _ => null
            };
        });

        return cmd;
    }

    public static Command AddSymbols(this Command cmd, params Symbol[] symbols)
    {
        foreach (var symbol in symbols)
        {
            switch (symbol)
            {
                case Option o:
                    cmd.AddOption(o);
                    break;
                case Argument a:
                    cmd.AddArgument(a);
                    break;
                case Command c:
                    cmd.AddCommand(c);
                    break;
                default:
                    throw new ArgumentException($"Unknown symbol {symbol}");
            }
        }

        return cmd;
    }
}