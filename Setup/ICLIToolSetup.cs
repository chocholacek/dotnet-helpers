namespace DotnetHelperTool.Setup;

public interface ICLIToolSetup
{
    void Register();
    void Build(RootCommand root);
}