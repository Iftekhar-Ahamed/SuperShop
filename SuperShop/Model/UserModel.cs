namespace SuperShop.Model
{
    public class UserModel
    {
        public long Id { get; set; }
        public long UserTypeId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public string? ConnectionId { get; set; }
        public bool IsActive { get; set; }
        public string? Password { get; set; }
    }
}
