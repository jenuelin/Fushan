﻿namespace Messages
{
    public class GetUsersRequest : PageableRequest
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
    }
}
