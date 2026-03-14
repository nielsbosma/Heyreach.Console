using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Leads;

public sealed class GetSettings : GlobalSettings
{
    [CommandOption("--linkedin-url <URL>")]
    [Description("LinkedIn profile URL")]
    public required string LinkedinUrl { get; set; }
}

[Description("Get lead details by LinkedIn URL")]
public sealed class GetCommand : HeyreachCommand<GetSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, GetSettings settings)
    {
        var result = await client.PostAsync("/lead/GetLead", new { profileUrl = settings.LinkedinUrl });
        return ToObject(result);
    }
}
