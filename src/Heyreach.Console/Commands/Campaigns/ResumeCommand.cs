using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Campaigns;

public sealed class ResumeSettings : GlobalSettings
{
    [CommandArgument(0, "<id>")]
    [Description("Campaign ID")]
    public int Id { get; set; }
}

[Description("Resume a paused campaign")]
public sealed class ResumeCommand : HeyreachCommand<ResumeSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, ResumeSettings settings)
    {
        var result = await client.PostAsync("/campaign/Resume", new { campaignId = settings.Id });
        return ToObject(result);
    }
}
