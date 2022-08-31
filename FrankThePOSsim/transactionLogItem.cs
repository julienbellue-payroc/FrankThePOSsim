namespace FrankThePOSsim
{
    public class TransactionLogItem
    {
        public string? Request { get; set; }
        public Response? Response { get; set; }
        public string? Endpoint { get; set; }
        public Transaction? Transaction { get; set; }
    }
}
