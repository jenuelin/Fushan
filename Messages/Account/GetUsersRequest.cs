namespace Messages.Account
{
    public class GetUsersRequest : PageableRequest
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
    }
}
