namespace DotnetHelperTool.Tools;

public class Base64Converter : CLIToolSetupBase, ICLIToolSetup
{
    public Base64Converter() : base("base64", "encoding and decoding base64 strings")
    {
    }

    public override void Register()
    {
        var arg = new Argument<string>("text", "input text");
        var encoding = new Option<EncodingType>(new[] { "-e", "--encoding"}, () => EncodingType.UTF8);

        var encode = new Command("encode", "encodes data to base64 string")
        {
            arg,
            encoding
        };

        var decode = new Command("decode", "decodes base64 string")
        {
            arg,
            encoding
        };

        encode.SetHandler<string, EncodingType>(HandleEncode, arg, encoding);
        decode.SetHandler<string, EncodingType>(HandleDecode, arg, encoding);

        Command.AddCommands(encode, decode);
    }

    private void HandleDecode(string data, EncodingType encoding)
    {
        var bytes = Convert.FromBase64String(data);
        Console.WriteLine(encoding.ToEncoding().GetString(bytes));
    }

    private void HandleEncode(string data, EncodingType encoding)
    {
        var bytes = encoding.ToEncoding().GetBytes(data);
        Console.WriteLine(Convert.ToBase64String(bytes));
    }
}