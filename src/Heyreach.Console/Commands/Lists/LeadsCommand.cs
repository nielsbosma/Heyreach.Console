using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Lists;

public sealed class LeadsSettings : PaginatedSettings
{
    [CommandArgument(0, "<id>")]
    [Description("List ID")]
    public int Id { get; set; }

    [CommandOption("--keyword")]
    [Description("Filter leads by keyword")]
    public string? Keyword { get; set; }
}

[Description("Get leads from a list")]
public sealed class LeadsCommand : HeyreachCommand<LeadsSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, LeadsSettings settings)
    {
        var body = new Dictionary<string, object>
        {
            ["listId"] = settings.Id,
            ["offset"] = settings.Offset,
            ["limit"] = settings.Limit
        };
        if (!string.IsNullOrWhiteSpace(settings.Keyword))
            body["keyword"] = settings.Keyword;

        var result = await client.PostAsync("/list/GetLeadsFromList", body);
        return ToObject(result);
    }
}
