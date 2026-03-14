using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Webhooks;

public sealed class CreateSettings : GlobalSettings
{
    [CommandOption("--name <NAME>")]
    [Description("Webhook name")]
    public required string Name { get; set; }

    [CommandOption("--url <URL>")]
    [Description("Webhook callback URL")]
    public required string Url { get; set; }

    [CommandOption("--event <EVENT>")]
    [Description("Event type (e.g. message_reply, connection_request_accepted)")]
    public required string Event { get; set; }

    [CommandOption("--campaign-id")]
    [Description("Scope to a specific campaign")]
    public int? CampaignId { get; set; }
}

[Description("Create a webhook")]
public sealed class CreateCommand : HeyreachCommand<CreateSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, CreateSettings settings)
    {
        var body = new Dictionary<string, object>
        {
            ["name"] = settings.Name,
            ["url"] = settings.Url,
            ["eventType"] = settings.Event
        };
        if (settings.CampaignId.HasValue)
            body["campaignId"] = settings.CampaignId.Value;

        var result = await client.PostAsync("/webhook/Create", body);
        return ToObject(result);
    }
}
