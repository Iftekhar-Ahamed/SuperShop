using System.ComponentModel.DataAnnotations;

namespace SuperShop.Model
{
    public class ItemTypeModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemTypeName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string UOM { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
