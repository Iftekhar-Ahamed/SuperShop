﻿namespace SuperShop.Model
{
    public class PaginationModel
    {
        public dynamic data { get; set; }
        public long total { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
    }
}
