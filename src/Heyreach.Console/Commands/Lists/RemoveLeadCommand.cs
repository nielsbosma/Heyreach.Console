using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Lists;

public sealed class RemoveLeadSettings : GlobalSettings
{
    [CommandArgument(0, "<id>")]
    [Description("List ID")]
    public int Id { get; set; }

    [CommandOption("--lead <LEAD_ID>")]
    [Description("Lead ID to remove")]
    public required int LeadId { get; set; }
}

[Description("Remove a lead from a list")]
public sealed class RemoveLeadCommand : HeyreachCommand<RemoveLeadSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, RemoveLeadSettings settings)
    {
        var result = await client.PostAsync("/list/DeleteLeadFromList", new
        {
            listId = settings.Id,
            leadId = settings.LeadId
        });
        return ToObject(result);
    }
}
