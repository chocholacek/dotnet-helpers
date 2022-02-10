using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace DotnetHelperTool.Tools
{
    public class JsonPath : CLIToolSetupBase, ICLIToolSetup
    {
        public JsonPath() : base("json", "jsonpath related functionality")
        {
        }

        public override void Register()
        {
            var file = new Option<FileInfo>("--file").ExistingOnly();
            var text = new Option<string>("--text");
            var path = new Option<string>("--path") { IsRequired = true };

            file.MutuallyExclusiveWith(text);

            Command.AddOptions(file, text, path);

            Command.SetHandler<FileInfo?, string?, string>(HandleJsonPath, file, text, path);
        }

        private void HandleJsonPath(FileInfo? file, string? text, string path)
        {
            var data = text;
            if (file is { })
            {
                data = File.ReadAllText(file.FullName);
            }

            var json = JObject.Parse(data!);

            Console.WriteLine(json.SelectToken(path));

        }
    }
}