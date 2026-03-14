using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Tags;

public sealed class CreateSettings : GlobalSettings
{
    [CommandOption("--name <NAME>")]
    [Description("Tag display name")]
    public required string Name { get; set; }

    [CommandOption("--color")]
    [Description("Tag color (e.g. #FF0000)")]
    public string? Color { get; set; }
}

[Description("Create a workspace tag")]
public sealed class CreateCommand : HeyreachCommand<CreateSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, CreateSettings settings)
    {
        var body = new Dictionary<string, object>
        {
            ["displayName"] = settings.Name
        };
        if (settings.Color is not null)
            body["color"] = settings.Color;

        // Note: This endpoint may not be available in all API tiers
        var result = await client.PostAsync("/tag/Create", body);
        return ToObject(result);
    }
}
