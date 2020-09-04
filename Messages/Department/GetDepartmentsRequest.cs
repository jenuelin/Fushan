namespace Messages.Department
{
    public class GetDepartmentsRequest : PageableRequest
    {
        public string DepartmentId { get; set; }
        public string Name { get; set; }
    }
}
