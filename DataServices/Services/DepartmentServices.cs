using System;
using System.Linq;
using System.Threading.Tasks;
using DataServices.Db;
using DataServices.Model;
using Microsoft.EntityFrameworkCore;

namespace DataServices.Services
{
    public interface IDepartment
    {
        Task<Department> GetDepartment(Guid id);
        Task<Department[]> GetDepartments();

        Task CreateDepartment(Department request);

        Task UpdateDepartment(Department request);

        Task DeleteDepartment(Guid id);
    }
    public class DepartmentServices : IDepartment
    {
        private readonly IRepository _repository;
        private readonly INewRedisRepository _newRedisRepository;

        private static readonly string prefix = "Departments";

        public DepartmentServices(IRepository repository, INewRedisRepository newRedisRepository)
        {
            _repository = repository;
            _newRedisRepository = newRedisRepository;
        }

        Task<Department> IDepartment.GetDepartment(Guid id)
        {
            return _repository.Departments.FirstOrDefaultAsync(m => m.Id == id);
        }

        Task<Department[]> IDepartment.GetDepartments()
        {
            return _repository.Departments.AsNoTracking().ToArrayAsync();
        }

        Task IDepartment.CreateDepartment(Department department)
        {
            _repository.Add(department);
            return _repository.SaveChangesAsync();
        }

        Task IDepartment.UpdateDepartment(Department department)
        {
            _repository.Entry(department).State = EntityState.Modified;
            return _repository.SaveChangesAsync();
        }

        Task IDepartment.DeleteDepartment(Guid id)
        {
            var department = _repository.Departments.FirstOrDefault(m => m.Id == id);
            _repository.Delete(department);
            return _repository.SaveChangesAsync();
        }
    }
}
