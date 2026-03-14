using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Campaigns;

public sealed class StopLeadSettings : GlobalSettings
{
    [CommandArgument(0, "<campaign-id>")]
    [Description("Campaign ID")]
    public int CampaignId { get; set; }

    [CommandOption("--lead <LEAD_ID>")]
    [Description("Lead ID to stop")]
    public required int LeadId { get; set; }
}

[Description("Stop a specific lead in a campaign")]
public sealed class StopLeadCommand : HeyreachCommand<StopLeadSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, StopLeadSettings settings)
    {
        var result = await client.PostAsync("/campaign/StopLeadInCampaign", new
        {
            campaignId = settings.CampaignId,
            leadId = settings.LeadId
        });
        return ToObject(result);
    }
}
