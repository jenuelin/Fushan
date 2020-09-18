using Configration.Enums;
using System;

namespace Messages.Role
{
    public class RoleResponse : PageableResponse<RoleModel>
    {
    }

    public class RoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
        public StatusEnum StatusString { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string CreatedByUsername { get; set; }
        public string UpdatedByUsername { get; set; }
    }
}