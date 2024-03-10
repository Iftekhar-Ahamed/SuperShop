using System.ComponentModel.DataAnnotations;

namespace SuperShop.Model
{
    public class ItemTransactionModel
    {
        public int Id { get; set; }
        [Required]
        [Range(1,long.MaxValue, ErrorMessage = "Please Select Valid Item")]
        public int ItemId { get; set; }
        [Range(1,2, ErrorMessage = "Please Select Transaction Type")]
        public int TransactionTypeId { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Please enter some amount")]
        public decimal UnitOfAmount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal unitPricePurchase { get; set; }
        public decimal unitPriceSell { get; set; }
        public decimal? Total { get; set; }
        public int? PartNo { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateTimeAction { get; set; }
    }
}
