namespace Messages
{
    public class PageableResponse<T> : MessageResponse
    {
        public int Count { get; set; }
        public int PagePageIndex { get; set; }
        public int TotalPages { get; set; }
        public T[] Table { get; set; }
    }
}