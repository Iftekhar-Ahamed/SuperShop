namespace SuperShop.Model
{
    public class GetDataConfigModel
    {
        public bool? IsActive { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public long NumSearchTerm { get; set; } 
        public string? OrderBy { get; set; }
        public string? OrderColumn { get; set; }
        public int PageNo { get; set; }
        public int OffsetRows { get; set; }
        public int PageSize { get; set; }
    }
}
