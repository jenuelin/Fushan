using Configration.Enums;
using System;

namespace Messages
{
    public class CreateUpdateDepartmentRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public Guid? UpperId { get; set; }
        public StatusEnum Status { get; set; }
        public string Memo { get; set; }
    }
}