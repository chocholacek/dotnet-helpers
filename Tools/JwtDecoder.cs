using System.IdentityModel.Tokens.Jwt;

namespace DotnetHelperTool.Tools;

public class JwtDecoder : CLIToolSetupBase, ICLIToolSetup
{
    public JwtDecoder() : base("jwt", "decoding of jwt tokens")
    {
    }

    public override void Register()
    {
        var token = new Argument<string>("token");

        var decode = new Command("decode")
        {
            token
        };

        decode.SetHandler<string>(HandleDecode, token);
        
        Command.AddCommand(decode);
    }

    private void HandleDecode(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var json = handler.ReadJwtToken(token);

        Console.WriteLine(json);
    }
}