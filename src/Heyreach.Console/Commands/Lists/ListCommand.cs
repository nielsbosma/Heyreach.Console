using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Lists;

[Description("List all lead lists")]
public sealed class ListCommand : HeyreachCommand<PaginatedSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, PaginatedSettings settings)
    {
        var result = await client.PostAsync("/list/GetAll", new
        {
            offset = settings.Offset,
            limit = settings.Limit
        });
        return ToObject(result);
    }
}
