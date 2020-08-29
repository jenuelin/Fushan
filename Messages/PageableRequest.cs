namespace Messages
{
    public class PageableRequest
    {
        public int Page { get; set; }
        public int Rows { get; set; }
        public string OrderBy { get; set; }
        public bool IsDesc => OrderBy != "ASC";
        public string SortBy { get; set; }
        public bool ShowAll { get; set; }
    }
}
