using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Leads;

public sealed class ListsSettings : GlobalSettings
{
    [CommandOption("--linkedin-url <URL>")]
    [Description("LinkedIn profile URL")]
    public required string LinkedinUrl { get; set; }
}

[Description("Get all lists containing a lead")]
public sealed class ListsCommand : HeyreachCommand<ListsSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, ListsSettings settings)
    {
        var result = await client.PostAsync("/list/GetListsForLead", new { profileUrl = settings.LinkedinUrl });
        return ToObject(result);
    }
}
