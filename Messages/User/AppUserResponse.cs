using Configration.Enums;
using Messages.Role;
using System;

namespace Messages.User
{
    public class AppUserResponse : PageableResponse<AppUserModel>
    {
    }

    public class AppUserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Sex { get; set; }
        public SexEnum SexString { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Rank { get; set; }
        public string Level { get; set; }
        public string EmployeeCategory { get; set; }
        public EmployeeCategoryEnum EmployeeCategoryString { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        //public EmployeeCategoryEnum EmployeeCategoryString { get; set; }
        public string EmploymentStatus { get; set; }

        public EmploymentStatusEnum EmploymentStatusString { get; set; }

        public string OnTheJobDay { get; set; }
        public StatusEnum Status { get; set; }
        public string ResignationDay { get; set; }
        public string Phone { get; set; }
        public string WorkPhone { get; set; }
        public string IDNumber { get; set; }
        public string Birthday { get; set; }
        public string Nationality { get; set; }
        public string Memo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string CreatedByUsername { get; set; }
        public string UpdatedByUsername { get; set; }
        public UserRoles[] UserRoles { get; set; }
    }

    public class UserRoles
    {
        public AppUserModel User { get; set; }
        public RoleModel Role { get; set; }
    }
}