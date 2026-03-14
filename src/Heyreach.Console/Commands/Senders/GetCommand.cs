using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Senders;

public sealed class GetSettings : GlobalSettings
{
    [CommandArgument(0, "<id>")]
    [Description("Sender account ID")]
    public int Id { get; set; }
}

[Description("Get sender account details")]
public sealed class GetCommand : HeyreachCommand<GetSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, GetSettings settings)
    {
        var result = await client.GetAsync($"/linkedInAccount/GetById?senderId={settings.Id}");
        return ToObject(result);
    }
}
