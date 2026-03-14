using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Lists;

public sealed class AddLeadSettings : GlobalSettings
{
    [CommandArgument(0, "<id>")]
    [Description("List ID")]
    public int Id { get; set; }

    [CommandOption("--linkedin-url <URL>")]
    [Description("LinkedIn profile URL of the lead")]
    public required string LinkedinUrl { get; set; }
}

[Description("Add a lead to a list")]
public sealed class AddLeadCommand : HeyreachCommand<AddLeadSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, AddLeadSettings settings)
    {
        var result = await client.PostAsync("/list/AddLeadsToListV2", new
        {
            listId = settings.Id,
            leads = new[] { new { linkedinUrl = settings.LinkedinUrl } }
        });
        return ToObject(result);
    }
}
