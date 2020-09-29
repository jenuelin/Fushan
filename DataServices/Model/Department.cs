using Configration.Enums;
using System;
using System.Collections.Generic;

namespace DataServices.Model
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DepartmentId { get; set; }
        public Guid? DepartmentLeaderId { get; set; }
        public AppUser DepartmentLeader { get; set; }
        public Guid? UpperId { get; set; }
        public Department UpperDepartment { get; set; }
        public StatusEnum Status { get; set; }

        /// <summary>
        /// Memo
        /// </summary>
        public string Memo { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string CreatedByUsername { get; set; }
        public string UpdatedByUsername { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}