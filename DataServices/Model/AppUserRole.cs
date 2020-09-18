using Microsoft.AspNetCore.Identity;
using System;

namespace DataServices.Model
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public virtual AppUser User { get; set; }
        public virtual Role Role { get; set; }
    }
}