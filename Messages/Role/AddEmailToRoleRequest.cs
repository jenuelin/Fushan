using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Role
{
    public class AddEmailToRoleRequest
    {
        public string UserEmail { get; set; }
        public string RoleName { get; set; }
    }
}
