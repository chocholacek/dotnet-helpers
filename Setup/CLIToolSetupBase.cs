namespace DotnetHelperTool.Setup
{
    public abstract class CLIToolSetupBase : ICLIToolSetup
    {
        protected Command Command { get; init; }

        protected CLIToolSetupBase(string commandName, string? description = default)
        {
            Command = new(commandName, description);
        }

        public abstract void Register();

        public void Build(RootCommand root) => root.AddCommand(Command);
    }
}