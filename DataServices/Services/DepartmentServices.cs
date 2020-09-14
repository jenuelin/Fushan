using DataServices.Db;
using DataServices.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataServices.Services
{
    public interface IDepartment
    {
        Task<Department> GetDepartmentAsync(Guid id, Func<DbSet<Department>, IQueryable<Department>> preQuery = null);
        Task<Department[]> GetDepartmentsAsync(Expression<Func<Department, bool>> expression);
        Task<Department[]> GetDepartmentsAsync();

        IQueryable<Department> GetDepartmentsQuery();

        Task CreateDepartmentAsync(Department request);

        Task UpdateDepartmentAsync(Department request);

        Task DeleteDepartmentAsync(Guid id);
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

        Task<Department> IDepartment.GetDepartmentAsync(Guid id, Func<DbSet<Department>, IQueryable<Department>> preQuery)
        {
            if (preQuery == null) preQuery = c => c;
            return preQuery(_repository.Departments).FirstOrDefaultAsync(m => m.Id == id);
        }

        Task<Department[]> IDepartment.GetDepartmentsAsync(Expression<Func<Department, bool>> expression)
        {
            return _repository.Departments.Where(expression).AsNoTracking().ToArrayAsync();
        }

        Task IDepartment.CreateDepartmentAsync(Department department)
        {
            _repository.Add(department);
            return _repository.SaveChangesAsync();
        }

        Task IDepartment.UpdateDepartmentAsync(Department department)
        {
            _repository.Entry(department).State = EntityState.Modified;
            return _repository.SaveChangesAsync();
        }

        Task IDepartment.DeleteDepartmentAsync(Guid id)
        {
            var department = _repository.Departments.FirstOrDefault(m => m.Id == id);
            _repository.Delete(department);
            return _repository.SaveChangesAsync();
        }

        Task<Department[]> IDepartment.GetDepartmentsAsync()
        {
            return _repository.Departments.AsNoTracking().ToArrayAsync();
        }

        IQueryable<Department> IDepartment.GetDepartmentsQuery()
        {
            return _repository.Departments;
        }
    }
}
