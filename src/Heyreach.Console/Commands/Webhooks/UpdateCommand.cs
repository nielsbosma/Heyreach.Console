using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Webhooks;

public sealed class UpdateSettings : GlobalSettings
{
    [CommandArgument(0, "<id>")]
    [Description("Webhook ID")]
    public int Id { get; set; }

    [CommandOption("--name")]
    [Description("New webhook name")]
    public string? Name { get; set; }

    [CommandOption("--url")]
    [Description("New callback URL")]
    public string? Url { get; set; }

    [CommandOption("--event")]
    [Description("New event type")]
    public string? Event { get; set; }

    [CommandOption("--active")]
    [Description("Activate or deactivate: true or false")]
    public bool? Active { get; set; }
}

[Description("Update a webhook")]
public sealed class UpdateCommand : HeyreachCommand<UpdateSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, UpdateSettings settings)
    {
        var body = new Dictionary<string, object>
        {
            ["webhookId"] = settings.Id
        };
        if (settings.Name is not null)
            body["name"] = settings.Name;
        if (settings.Url is not null)
            body["url"] = settings.Url;
        if (settings.Event is not null)
            body["eventType"] = settings.Event;
        if (settings.Active.HasValue)
            body["isActive"] = settings.Active.Value;

        var result = await client.PutAsync("/webhook/Update", body);
        return ToObject(result);
    }
}
