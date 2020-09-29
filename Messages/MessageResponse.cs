namespace Messages
{
    public class MessageResponse
    {
        public bool Valid { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}