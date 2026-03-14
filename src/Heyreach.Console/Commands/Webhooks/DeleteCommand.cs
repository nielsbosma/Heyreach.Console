using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Webhooks;

public sealed class DeleteSettings : GlobalSettings
{
    [CommandArgument(0, "<id>")]
    [Description("Webhook ID")]
    public int Id { get; set; }
}

[Description("Delete a webhook")]
public sealed class DeleteCommand : HeyreachCommand<DeleteSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, DeleteSettings settings)
    {
        var result = await client.DeleteAsync($"/webhook/Delete?webhookId={settings.Id}");
        return ToObject(result);
    }
}
