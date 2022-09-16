using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace FrankThePOSsim;

public class TransactionResponse
{
    public string? HttpStatus { get; }
    public string? ResultCode { get; }
    public string? Message { get; }
    public string? FullBody { get; }

    public TransactionResponse(HttpResponseMessage? message)
    {
        if (message == null)
        {
            throw new NullReferenceException();
        }
        HttpStatus = $"{(int)message.StatusCode} - {message.ReasonPhrase}";
        FullBody = message.Content.ReadAsStringAsync().Result;
        
        if (string.IsNullOrWhiteSpace(FullBody)) return;

        try
        {
            var json = JObject.Parse(FullBody);
            Message = (string?)json.SelectToken("Message");
            ResultCode = (string?)json.SelectToken("ResultCode");
        }
        catch
        {
            // JSON failed to parse - not an issue, the full message is there
            // Any error message here would look like it came from the terminal,
            // so ignore silently
        }
    }
}