namespace FrankThePOSsim;

public class TransactionLogItem
{
    public string? Request { get; init; }
    public TransactionResponse? Response { get; set; }
    public string? Endpoint { get; init; }
    public Transaction? Transaction { get; init; }
}
