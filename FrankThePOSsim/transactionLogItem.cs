namespace FrankThePOSsim
{
    public class TransactionLogItem
    {
        public string? Request { get; set; }
        public int? HttpStatusCode { get; set; }
        public string? Response { get; set; }
        public string? Endpoint { get; set; }
        public Transaction? Transaction { get; set; }
    }
}
