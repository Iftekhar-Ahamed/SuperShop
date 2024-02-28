using System.ComponentModel.DataAnnotations;

namespace SuperShop.Model
{
    public class MenuModel
    {
        [Required]
        [StringLength(50)]
        public string MenuName { get; set; } = String.Empty;
        [Required]
        [StringLength(50)]
        public string MenuUrl { get; set; } = String.Empty;
        public bool IsActive { get; set; }

    }
}
