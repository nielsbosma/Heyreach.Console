using System.Text.Json;
using Spectre.Console.Cli;

namespace Heyreach.Console.Infrastructure;

public abstract class HeyreachCommand<TSettings> : AsyncCommand<TSettings> where TSettings : GlobalSettings
{
    public sealed override async Task<int> ExecuteAsync(CommandContext context, TSettings settings, CancellationToken cancellation)
    {
        try
        {
            var apiKey = settings.ResolveApiKey();
            using var client = new HeyreachApiClient(apiKey, settings.Verbose);
            var result = await ExecuteAsync(client, settings);
            OutputHelper.Write(result, settings.Format);
            return 0;
        }
        catch (HeyreachApiException ex)
        {
            OutputHelper.WriteError(ex.Message, ex.StatusCode);
            return ex.StatusCode;
        }
        catch (HeyreachException ex)
        {
            OutputHelper.WriteError(ex.Message, 1);
            return 1;
        }
        catch (HttpRequestException ex)
        {
            OutputHelper.WriteError($"Connection failed: {ex.Message}", 2);
            return 2;
        }
    }

    protected abstract Task<object> ExecuteAsync(HeyreachApiClient client, TSettings settings);

    protected static object ToObject(JsonElement element) => ConvertElement(element);

    private static object ConvertElement(JsonElement element) => element.ValueKind switch
    {
        JsonValueKind.Object => element.EnumerateObject()
            .ToDictionary(p => p.Name, p => ConvertElement(p.Value)),
        JsonValueKind.Array => element.EnumerateArray()
            .Select(ConvertElement).ToList(),
        JsonValueKind.String => element.GetString()!,
        JsonValueKind.Number => element.TryGetInt64(out var l) ? l : element.GetDouble(),
        JsonValueKind.True => true,
        JsonValueKind.False => false,
        _ => (object)null!
    };
}
