namespace Messages
{
    public class PageableResponse: MessageResponse
    {
        public int Count { get; set; }
        public int PagePageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
