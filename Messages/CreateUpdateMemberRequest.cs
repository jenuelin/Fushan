namespace Messages
{
    public class CreateUpdateMemberRequest
    {
        public MemberRequest Data { get; set; }
    }
    public class MemberRequest
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public GameModel[] Games { get; set; }
    }

    public class GameModel
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
