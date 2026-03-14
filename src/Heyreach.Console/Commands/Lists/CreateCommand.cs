using System.ComponentModel;
using Heyreach.Console.Infrastructure;
using Spectre.Console.Cli;

namespace Heyreach.Console.Commands.Lists;

public sealed class CreateSettings : GlobalSettings
{
    [CommandOption("--name <NAME>")]
    [Description("List name")]
    public required string Name { get; set; }

    [CommandOption("--type")]
    [Description("List type: user or company")]
    [DefaultValue("user")]
    public string Type { get; set; } = "user";
}

[Description("Create an empty list")]
public sealed class CreateCommand : HeyreachCommand<CreateSettings>
{
    protected override async Task<object> ExecuteAsync(HeyreachApiClient client, CreateSettings settings)
    {
        var listType = settings.Type.ToLowerInvariant() switch
        {
            "company" => "COMPANY_LIST",
            _ => "USER_LIST"
        };
        var result = await client.PostAsync("/list/CreateEmptyList", new
        {
            name = settings.Name,
            type = listType
        });
        return ToObject(result);
    }
}
