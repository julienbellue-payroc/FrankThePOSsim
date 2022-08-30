namespace FrankThePOSsim;

public interface ITransactionControl
{
    public Transaction GenerateTransaction();
    public string? GetUri();
    public void SetUri(string? uri);
    public void SetControlsFromTransaction(Transaction transaction);
}
