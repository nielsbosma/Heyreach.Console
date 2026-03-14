using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Webhooks;

public sealed class GetSettings : GlobalSettings
{
    [CommandArgument(0, "<id>")]
    [Description("Webhook ID")]
    public int Id { get; set; }
}

[Description("Get webhook details")]
public sealed class GetCommand : HeyreachCommand<GetSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, GetSettings settings)
    {
        var result = await client.GetAsync($"/webhook/GetById?webhookId={settings.Id}");
        return ToObject(result);
    }
}
