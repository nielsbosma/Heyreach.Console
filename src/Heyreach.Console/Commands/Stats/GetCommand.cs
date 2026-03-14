using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Stats;

public sealed class GetSettings : GlobalSettings
{
    [CommandOption("--from")]
    [Description("Start date (yyyy-MM-dd)")]
    public string? From { get; set; }

    [CommandOption("--to")]
    [Description("End date (yyyy-MM-dd)")]
    public string? To { get; set; }

    [CommandOption("--campaign-id")]
    [Description("Filter by campaign ID")]
    public int? CampaignId { get; set; }

    [CommandOption("--sender-id")]
    [Description("Filter by sender account ID")]
    public int? SenderId { get; set; }
}

[Description("Get overall stats")]
public sealed class GetCommand : HeyreachCommand<GetSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, GetSettings settings)
    {
        var body = new Dictionary<string, object>
        {
            ["accountIds"] = settings.SenderId.HasValue ? new[] { settings.SenderId.Value } : Array.Empty<int>(),
            ["campaignIds"] = settings.CampaignId.HasValue ? new[] { settings.CampaignId.Value } : Array.Empty<int>()
        };

        if (!string.IsNullOrWhiteSpace(settings.From))
        {
            if (!DateOnly.TryParseExact(settings.From, "yyyy-MM-dd", out _))
                throw new HeyreachException("--from must be in yyyy-MM-dd format");
            body["from"] = settings.From;
        }
        if (!string.IsNullOrWhiteSpace(settings.To))
        {
            if (!DateOnly.TryParseExact(settings.To, "yyyy-MM-dd", out _))
                throw new HeyreachException("--to must be in yyyy-MM-dd format");
            body["to"] = settings.To;
        }

        var result = await client.PostAsync("/stats/GetOverallStats", body);
        return ToObject(result);
    }
}
