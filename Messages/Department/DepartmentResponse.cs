using Configration.Enums;
using System;

namespace Messages.Department
{
    public class DepartmentResponse : PageableResponse
    {
        public DepartmentModel[] table { get; set; }
    }

    public class DepartmentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DepartmentId { get; set; }
        public Guid? UpperId { get; set; }
        public DepartmentModel UpperDepartment { get; set; }
        public StatusEnum Status { get; set; }
        public StatusEnum StatusString { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string CreatedByUsername { get; set; }
        public string UpdatedByUsername { get; set; }
    }
}
