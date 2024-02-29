namespace SuperShop.Model
{
    public class GetDataConfigModel
    {
        public bool? IsActive { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public long NumSearchTerm { get; set; } 
        public string? OrderBy { get; set; }
        public string? OrderColumn { get; set; }
        public long PageNo { get; set; }
        public long PageSize { get; set; }
    }
}
