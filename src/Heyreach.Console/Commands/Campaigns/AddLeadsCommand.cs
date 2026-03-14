using System.ComponentModel;
using System.Text.Json;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Campaigns;

public sealed class AddLeadsSettings : GlobalSettings
{
    [CommandArgument(0, "<id>")]
    [Description("Campaign ID")]
    public int Id { get; set; }

    [CommandOption("--file")]
    [Description("Path to JSON file containing leads array")]
    public string? File { get; set; }
}

[Description("Add leads to a campaign from stdin or --file")]
public sealed class AddLeadsCommand : HeyreachCommand<AddLeadsSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, AddLeadsSettings settings)
    {
        string json;
        if (!string.IsNullOrWhiteSpace(settings.File))
        {
            json = await System.IO.File.ReadAllTextAsync(settings.File);
        }
        else if (!System.Console.IsInputRedirected)
        {
            throw new HeyreachException("Provide leads via stdin or --file");
        }
        else
        {
            json = await System.Console.In.ReadToEndAsync();
        }

        var leads = JsonSerializer.Deserialize<JsonElement>(json);
        if (leads.ValueKind != JsonValueKind.Array)
            throw new HeyreachException("Input must be a JSON array of lead objects");
        var body = new { campaignId = settings.Id, leads };
        var result = await client.PostAsync("/campaign/AddLeadsToCampaign", body);
        return ToObject(result);
    }
}
