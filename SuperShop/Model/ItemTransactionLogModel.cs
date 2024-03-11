namespace SuperShop.Model
{
    public class ItemTransactionLogModel
    {
        public string ActionChanges { get; set; } = string.Empty;
        public decimal UnitOfAmount { get; set; }
        public decimal UnitPrice { get; set;}
        public decimal Total { get; set; }

    }
}
