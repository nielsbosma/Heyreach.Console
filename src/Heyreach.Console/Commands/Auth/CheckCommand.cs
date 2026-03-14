using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Auth;

[Description("Validate the API key")]
public sealed class CheckCommand : HeyreachCommand<GlobalSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, GlobalSettings settings)
    {
        await client.GetAsync("/auth/CheckApiKey");
        return new { valid = true, message = "API key is valid" };
    }
}
