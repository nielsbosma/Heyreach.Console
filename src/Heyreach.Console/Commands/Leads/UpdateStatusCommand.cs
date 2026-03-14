using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Leads;

public sealed class UpdateStatusSettings : GlobalSettings
{
    [CommandOption("--lead <LEAD_ID>")]
    [Description("Lead ID")]
    public required int LeadId { get; set; }

    [CommandOption("--status <STATUS>")]
    [Description("New status: pending, contacted, replied, connected, not_interested, bounced")]
    public required string Status { get; set; }
}

[Description("Update the status of a lead")]
public sealed class UpdateStatusCommand : HeyreachCommand<UpdateStatusSettings>
{
    private static readonly HashSet<string> ValidStatuses = new(StringComparer.OrdinalIgnoreCase)
    {
        "pending", "contacted", "replied", "connected", "not_interested", "bounced"
    };

    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, UpdateStatusSettings settings)
    {
        if (!ValidStatuses.Contains(settings.Status))
            throw new HeyreachException($"Invalid status '{settings.Status}'. Must be one of: {string.Join(", ", ValidStatuses)}");

        var result = await client.PostAsync("/lead/UpdateStatus", new
        {
            leadId = settings.LeadId,
            status = settings.Status
        });
        return ToObject(result);
    }
}
