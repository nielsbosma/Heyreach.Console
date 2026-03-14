using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Webhooks;

[Description("List all webhooks")]
public sealed class ListCommand : HeyreachCommand<PaginatedSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, PaginatedSettings settings)
    {
        // Note: This endpoint may not be available in all API tiers
        var result = await client.PostAsync("/webhook/GetAll", new
        {
            offset = settings.Offset,
            limit = settings.Limit
        });
        return ToObject(result);
    }
}
