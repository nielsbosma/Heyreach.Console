using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Conversations;

public sealed class SendSettings : GlobalSettings
{
    [CommandOption("--lead <LEAD_ID>")]
    [Description("Lead ID to send message to")]
    public required int LeadId { get; set; }

    [CommandOption("--message <MESSAGE>")]
    [Description("Message text")]
    public required string Message { get; set; }

    [CommandOption("--sender-id")]
    [Description("Sender account ID")]
    public int? SenderId { get; set; }
}

[Description("Send a message to a lead")]
public sealed class SendCommand : HeyreachCommand<SendSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, SendSettings settings)
    {
        var body = new Dictionary<string, object>
        {
            ["leadId"] = settings.LeadId,
            ["message"] = settings.Message
        };
        if (settings.SenderId.HasValue)
            body["senderId"] = settings.SenderId.Value;

        // Note: This endpoint may not be available in all API tiers
        var result = await client.PostAsync("/conversation/SendMessage", body);
        return ToObject(result);
    }
}
