using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Heyreach.Console.Infrastructure;

public class GlobalSettings : CommandSettings
{
    [CommandOption("--api-key")]
    [Description("HeyReach API key (overrides HEYREACH_API_KEY env var)")]
    public string? ApiKey { get; set; }

    private static readonly HashSet<string> ValidFormats = new(StringComparer.OrdinalIgnoreCase) { "yaml", "json", "table" };

    [CommandOption("--format")]
    [Description("Output format: yaml, json, or table")]
    [DefaultValue("yaml")]
    public string Format { get; set; } = "yaml";

    public override ValidationResult Validate()
    {
        if (!ValidFormats.Contains(Format))
            return ValidationResult.Error($"Invalid format '{Format}'. Must be one of: yaml, json, table");
        return base.Validate();
    }

    [CommandOption("--no-color")]
    [Description("Disable colored output")]
    public bool NoColor { get; set; }

    [CommandOption("--verbose")]
    [Description("Print HTTP method, URL, and status code to stderr")]
    public bool Verbose { get; set; }

    public string ResolveApiKey()
    {
        if (!string.IsNullOrWhiteSpace(ApiKey))
            return ApiKey;

        var envKey = Environment.GetEnvironmentVariable("HEYREACH_API_KEY");
        if (!string.IsNullOrWhiteSpace(envKey))
            return envKey;

        throw new HeyreachException("HEYREACH_API_KEY not set. Provide --api-key or set the HEYREACH_API_KEY environment variable.");
    }
}

public class PaginatedSettings : GlobalSettings
{
    [CommandOption("--limit")]
    [Description("Number of items to return")]
    [DefaultValue(10)]
    public int Limit { get; set; } = 10;

    [CommandOption("--offset")]
    [Description("Number of items to skip")]
    [DefaultValue(0)]
    public int Offset { get; set; }
}
