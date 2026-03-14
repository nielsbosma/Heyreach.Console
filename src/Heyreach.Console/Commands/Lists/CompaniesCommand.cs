using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Lists;

public sealed class CompaniesSettings : PaginatedSettings
{
    [CommandArgument(0, "<id>")]
    [Description("Company list ID")]
    public int Id { get; set; }
}

[Description("Get companies from a company list")]
public sealed class CompaniesCommand : HeyreachCommand<CompaniesSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, CompaniesSettings settings)
    {
        var result = await client.PostAsync("/list/GetCompaniesFromList", new
        {
            listId = settings.Id,
            offset = settings.Offset,
            limit = settings.Limit
        });
        return ToObject(result);
    }
}
