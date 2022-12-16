namespace FrankThePOSsim;

public class TransactionLogItem
{
    public TransactionResponse? Response { get; set; }
    public string? Endpoint { get; init; }
    public Transaction? Transaction { get; init; }
    public string? Timestamp { get; init; }
    public string? Url { get; init; }
    public string? Payload { get; init; }
}
