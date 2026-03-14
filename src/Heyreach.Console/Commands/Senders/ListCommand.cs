using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Senders;

[Description("List all LinkedIn sender accounts")]
public sealed class ListCommand : HeyreachCommand<PaginatedSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, PaginatedSettings settings)
    {
        // Note: This endpoint may not be available in all API tiers
        var result = await client.PostAsync("/linkedInAccount/GetAll", new
        {
            offset = settings.Offset,
            limit = settings.Limit
        });
        return ToObject(result);
    }
}
