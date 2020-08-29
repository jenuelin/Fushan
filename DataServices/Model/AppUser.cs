using System;
using Configration.Enums;
using Microsoft.AspNetCore.Identity;

namespace DataServices.Model
{
    public class AppUser : IdentityUser<Guid>
    {
        /// <summary>
        /// 員工編號
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 員工姓名
        /// </summary>
        public override string UserName { get; set; }
        /// <summary>
        /// 性別
        /// </summary>
        public SexEnum Sex { get; set; }
        /// <summary>
        /// 部門
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// 部門
        /// </summary>
        public Department Department { get; set; }
        /// <summary>
        /// 職級
        /// </summary>
        public string Rank { get; set; }
        /// <summary>
        /// 職等
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 員工類別
        /// </summary>
        public EmployeeCategoryEnum EmployeeCategory { get; set; }
        /// <summary>
        /// 就職狀態
        /// </summary>
        public EmploymentStatusEnum EmploymentStatus { get; set; }
        /// <summary>
        /// 到職日
        /// </summary>
        public string OnTheJobDay { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        public StatusEnum Status { get; set; }
        /// <summary>
        /// 離職日
        /// </summary>
        public string ResignationDay { get; set; }
        /// <summary>
        /// 手機
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 公司電話
        /// </summary>
        public string WorkPhone { get; set; }
        /// <summary>
        /// 身分證號
        /// </summary>
        public string IDNumber { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 國籍
        /// </summary>
        public string Nationality { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        public string Memo { get; set; }
        // Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string CreatedByUsername { get; set; }
        public string UpdatedByUsername { get; set; }
    }
}
