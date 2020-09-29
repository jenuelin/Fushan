using DataServices.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace DataServices.Db
{
    public interface IRepository
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        //Task<IEnumerable<T>> Get<T>() where T : class;

        //Task<IEnumerable<T>> Get<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task Add<T>(T entity) where T : class;

        Task Delete<T>(T entity) where T : class;

        Task Update<T>(T entity) where T : class;

        Task RemoveRange<ICollection>(ICollection entities);

        EntityEntry<T> Entry<T>(T entity) where T : class;

        IQueryable<T> GetQueryable<T>() where T : class;

        DbSet<AppUser> AppUsers { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Department> Departments { get; set; }
    }

    public class Repository : IRepository
    {
        private readonly FushanContext _testContext;

        public Repository(FushanContext testContext)
        {
            _testContext = testContext;
        }

        public DbSet<AppUser> AppUsers { get => _testContext.AppUsers; set => AppUsers = value; }
        public DbSet<Role> Roles { get => _testContext.Roles; set => Roles = value; }
        public DbSet<Product> Products { get => _testContext.Products; set => Products = value; }
        public DbSet<Department> Departments { get => _testContext.Departments; set => Departments = value; }

        Task IRepository.Add<T>(T entity)
        {
            _testContext.Set<T>().Add(entity);
            return _testContext.SaveChangesAsync();
        }

        Task IRepository.Delete<T>(T entity)
        {
            _testContext.Set<T>().Remove(entity);
            return _testContext.SaveChangesAsync();
        }

        EntityEntry<T> IRepository.Entry<T>(T entity)
        {
            return _testContext.Entry(entity);
        }

        //Task<IEnumerable<T>> IRepository.Get<T>()
        //{
        //    var obj = _testContext.Set<T>();
        //    return obj.ToArrayAsync();
        //}

        //async Task<IEnumerable<T>> IRepository.Get<T>(Expression<Func<T, bool>> predicate)
        //{
        //    DbSet<T> obj = _testContext.Set<T>();
        //    return await obj.Where(predicate).ToArrayAsync().ConfigureAwait(false);
        //}

        IQueryable<T> IRepository.GetQueryable<T>()
        {
            return _testContext.Set<T>().AsQueryable();
        }

        Task IRepository.RemoveRange<ICollection>(ICollection entities)
        {
            _testContext.RemoveRange(entities);
            return _testContext.SaveChangesAsync();
        }

        int IRepository.SaveChanges()
        {
            return _testContext.SaveChanges();
        }

        Task<int> IRepository.SaveChangesAsync()
        {
            return _testContext.SaveChangesAsync();
        }

        Task IRepository.Update<T>(T entity)
        {
            _testContext.Set<T>().Update(entity);
            return _testContext.SaveChangesAsync();
        }
    }
}