namespace FrankThePOSsum
{
    public class TransactionLogItem
    {
        public string? Request { get; set; }
        public int? HttpStatusCode { get; set; }
        public string? Response { get; set; }
    }
}
