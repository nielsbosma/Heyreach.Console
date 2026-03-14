using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Conversations;

public sealed class ListSettings : PaginatedSettings
{
    [CommandOption("--campaign-id")]
    [Description("Filter by campaign ID")]
    public int? CampaignId { get; set; }

    [CommandOption("--sender-id")]
    [Description("Filter by sender account ID")]
    public int? SenderId { get; set; }
}

[Description("List conversations")]
public sealed class ListCommand : HeyreachCommand<ListSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, ListSettings settings)
    {
        var body = new Dictionary<string, object>
        {
            ["offset"] = settings.Offset,
            ["limit"] = settings.Limit
        };
        if (settings.CampaignId.HasValue)
            body["campaignId"] = settings.CampaignId.Value;
        if (settings.SenderId.HasValue)
            body["senderId"] = settings.SenderId.Value;

        // Note: This endpoint may not be available in all API tiers
        var result = await client.PostAsync("/conversation/GetConversationsV2", body);
        return ToObject(result);
    }
}
