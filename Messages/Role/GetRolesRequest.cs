using System;

namespace Messages.Role
{
    public class GetRolesRequest : PageableRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}