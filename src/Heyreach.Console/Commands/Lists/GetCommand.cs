using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Lists;

public sealed class GetSettings : GlobalSettings
{
    [CommandArgument(0, "<id>")]
    [Description("List ID")]
    public int Id { get; set; }
}

[Description("Get list details")]
public sealed class GetCommand : HeyreachCommand<GetSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, GetSettings settings)
    {
        var result = await client.GetAsync($"/list/GetById?listId={settings.Id}");
        return ToObject(result);
    }
}
