using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Campaigns;

public sealed class PauseSettings : GlobalSettings
{
    [CommandArgument(0, "<id>")]
    [Description("Campaign ID")]
    public int Id { get; set; }
}

[Description("Pause a running campaign")]
public sealed class PauseCommand : HeyreachCommand<PauseSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, PauseSettings settings)
    {
        var result = await client.PostAsync("/campaign/Pause", new { campaignId = settings.Id });
        return ToObject(result);
    }
}
