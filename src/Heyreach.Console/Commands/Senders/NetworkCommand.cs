using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Senders;

public sealed class NetworkSettings : PaginatedSettings
{
    [CommandArgument(0, "<id>")]
    [Description("Sender account ID")]
    public int Id { get; set; }
}

[Description("Get network connections for a sender account")]
public sealed class NetworkCommand : HeyreachCommand<NetworkSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, NetworkSettings settings)
    {
        var result = await client.PostAsync("/myNetwork/GetMyNetworkForSender", new
        {
            senderId = settings.Id,
            offset = settings.Offset,
            limit = settings.Limit
        });
        return ToObject(result);
    }
}
