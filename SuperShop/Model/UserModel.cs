using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SuperShop.Model
{
    public class UserModel
    {
        public long? Id { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Please Select UserType value bigger than {1}")]
        public long UserTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string UserFullName { get; set; } = string.Empty;

        [StringLength(250)]
        public string? ConnectionId { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [StringLength(50)]
        public string? Password { get; set; }

        public UserModel() { }
        public UserModel(ClaimsPrincipal User)
        {
            try
            {
                this.Id = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                this.UserFullName = User.FindFirstValue(ClaimTypes.Name);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
