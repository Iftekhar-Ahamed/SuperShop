using System.ComponentModel.DataAnnotations;

namespace SuperShop.Model
{
    public class ItemTransactionTypeModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string TransactionName { get; set; }
        public bool IsActive { get; set; }
    }
}
