namespace Messages
{
    public class GetUsersRequest
    {
        public MemberRequestData Data { get; set; }
    }

    public class MemberRequestData
    {
        public int Id { get; set; }
    }
}
