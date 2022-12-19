namespace FrankThePOSsim;

public interface ITransactionControl
{
    public Transaction GenerateTransaction(Terminal terminal);
    public string? GetUri();
    public void SetUri(string? uri, Environment environment);
    public void SetControlsFromTransaction(Transaction transaction);
}
