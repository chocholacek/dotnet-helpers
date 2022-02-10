using System.Text.RegularExpressions;

namespace DotnetHelperTool.Tools;

public class RegexMatcher : CLIToolSetupBase, ICLIToolSetup
{
    public RegexMatcher() : base("regex", "regex related functionality")
    {
    }

    public override void Register()
    {
        var input = new Option<string>(new[] { "-i", "--input" });
        var inputFile = new Option<FileInfo>(new[] { "-f", "--file"}).ExistingOnly();
        var pattern = new Option<string>(new[] { "-p", "--pattern" }) { IsRequired = true };

        input.MutuallyExclusiveWith(inputFile);

        var match = new Command("match")
        {
            input,
            inputFile,
            pattern,
        };

        match.OneOfIsrequired(input, inputFile);

        var replaceWith = new Option<string>(new[] { "-r", "--replace" }) { IsRequired = true };
        var overwrite = new Option<bool>(new [] { "--overwrite" }, "saves replace in the input file"); 
        
        overwrite.MustBeUsedWith(inputFile);

        var replace = new Command("replace")
        {
            input,
            inputFile,
            pattern,
            replaceWith,
            overwrite
        };

        replace.OneOfIsrequired(input, inputFile);

        match.SetHandler<string, FileInfo, string>(HandleMatch, input, inputFile, pattern);
        replace.SetHandler<string, FileInfo, string, string, bool>(HandleReplace, input, inputFile, pattern, replaceWith, overwrite);

        Command.AddCommands(match, replace);
    }

    private void HandleReplace(string input, FileInfo inputFile, string pattern, string replace, bool overwrite)
    {
        var data = input;

        if (inputFile is not null)
        {
            data = File.ReadAllText(inputFile.FullName);
            if (overwrite)
            {
                File.WriteAllText(inputFile.FullName, Regex.Replace(data, pattern, replace));
                Console.WriteLine($"{inputFile.FullName} updated.");
                return;
            }
        }

        Console.WriteLine(Regex.Replace(data, pattern, replace));
    }

    private void HandleMatch(string input, FileInfo inputFile, string pattern)
    {
        var data = input;

        if (inputFile is not null)
        {
            data = File.ReadAllText(inputFile.FullName);
        }

        var match = Regex.Match(data, pattern);

        if (match.Success is false)
        {
            Console.WriteLine($"No match!");
        }

        Console.WriteLine(match.Value);
        var groupNum = 0;
        foreach (Group group in match.Groups)
        {
            Console.WriteLine($"Group #{groupNum++}: {group.ValueSpan}");
        }
    }
}