using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Campaigns;

public sealed class GetSettings : GlobalSettings
{
    [CommandArgument(0, "<id>")]
    [Description("Campaign ID")]
    public int Id { get; set; }
}

[Description("Get campaign details")]
public sealed class GetCommand : HeyreachCommand<GetSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, GetSettings settings)
    {
        var result = await client.GetAsync($"/campaign/GetById?campaignId={settings.Id}");
        return ToObject(result);
    }
}
