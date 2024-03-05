using System.ComponentModel.DataAnnotations;

namespace SuperShop.Model
{
    public class GetAllMenuPermissionModel
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; } = String.Empty;
        public int UserId { get; set; }
        public string UserFullName { get; set; } = String.Empty;
        public bool IsActive { get; set; }
    }
}
