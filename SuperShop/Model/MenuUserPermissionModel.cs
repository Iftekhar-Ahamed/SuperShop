using System.ComponentModel.DataAnnotations;

namespace SuperShop.Model
{
    public class MenuUserPermissionModel
    {
        public int Id { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter MenuId value bigger than {1}")]
        public int MenuId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter UserId value bigger than {1}")]
        public int UserId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
