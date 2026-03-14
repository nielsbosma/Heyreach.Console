using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Campaigns;

public sealed class ListSettings : PaginatedSettings
{
    [CommandOption("--status")]
    [Description("Filter by status: active, paused, draft, completed")]
    public string? Status { get; set; }
}

[Description("List all campaigns")]
public sealed class ListCommand : HeyreachCommand<ListSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, ListSettings settings)
    {
        var body = new Dictionary<string, object>
        {
            ["offset"] = settings.Offset,
            ["limit"] = settings.Limit
        };
        if (!string.IsNullOrWhiteSpace(settings.Status))
            body["status"] = settings.Status;

        var result = await client.PostAsync("/campaign/GetAll", body);
        return ToObject(result);
    }
}
