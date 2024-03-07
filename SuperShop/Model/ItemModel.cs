using System.ComponentModel.DataAnnotations;

namespace SuperShop.Model
{
    public class ItemModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemName { get; set; } = string.Empty;
        public int ItemTypeId { get; set; }
        public string? ItemTypeName { get; set; }
        public string? UOM { get; set; }
        public decimal UnitPriceSell { get; set; }
        public decimal UnitPricePurchase { get; set; }
        public decimal StockQuantity { get; set; }
        public bool IsActive { get; set; }
    }
}
