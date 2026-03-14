namespace Heyreach.Console.Infrastructure;

public class HeyreachException(string message) : Exception(message);

public class HeyreachApiException(string message, int statusCode) : HeyreachException(message)
{
    public int StatusCode { get; } = statusCode;
}
