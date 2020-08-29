namespace Messages
{
    public class GetMemberResponse : MessageResponse
    {
        public MemberModel Member { get; set; }

    }

    public class GetMembersResponse : MessageResponse
    {
        public MemberModel[] Members { get; set; }
    }

    public class MemberModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }

        public GameModel[] Games { get; set; }
    }
}
