using Configration.Enums;
using System;

namespace Messages.User
{
    public class AppUserResponse : PageableResponse
    {
        public AppUserModel[] table { get; set; }
    }

    public class AppUserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserID { get; set; }
        public string Username { get; set; }
        public int Sex { get; set; }
        public SexEnum SexString { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Rank { get; set; }
        public string Level { get; set; }
        public EmployeeCategoryEnum EmployeeCategory { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        //public EmployeeCategoryEnum EmployeeCategoryString { get; set; }
        public EmploymentStatusEnum EmploymentStatus { get; set; }

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
    }
}