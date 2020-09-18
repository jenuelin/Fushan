using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DataServices.Model
{
    public class Role : IdentityRole<Guid>
    {
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string CreatedByUsername { get; set; }
        public string UpdatedByUsername { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}