using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataServices.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fushan.EntityFrameworkCore
{
    public interface IRepository
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IEnumerable<T> Get<T>() where T : class;
        IEnumerable<T> Get<T>(Expression<Func<T, bool>> predicate);
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void RemoveRange<ICollection>(ICollection entities);
        EntityEntry<T> Entry<T>(T entity) where T : class;

        IQueryable<T> GetQueryable<T>() where T : class;

        DbSet<User> Members { get; set; }
        DbSet<Game> Games { get; set; }
    }
    public class Repository : IRepository
    {
        private readonly FushanContext _testContext;
        public Repository(FushanContext testContext)
        {
            _testContext = testContext;
        }
        public DbSet<User> Members { get => _testContext.Members; set => Members = value; }
        public DbSet<Game> Games { get => _testContext.Games; set => Games = value; }

        void IRepository.Add<T>(T entity)
        {
            _testContext.Set<T>().Add(entity);
        }

        void IRepository.Delete<T>(T entity)
        {
            _testContext.Set<T>().Remove(entity);
        }

        EntityEntry<T> IRepository.Entry<T>(T entity)
        {
            return _testContext.Entry(entity);
        }

        IEnumerable<T> IRepository.Get<T>()
        {
            throw new NotImplementedException();
        }

        IEnumerable<T> IRepository.Get<T>(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        IQueryable<T> IRepository.GetQueryable<T>()
        {
            return _testContext.Set<T>().AsQueryable();
        }

        void IRepository.RemoveRange<ICollection>(ICollection entities)
        {
            _testContext.RemoveRange(entities);
        }

        int IRepository.SaveChanges()
        {
            return _testContext.SaveChanges();
        }

        Task<int> IRepository.SaveChangesAsync()
        {
            return _testContext.SaveChangesAsync();
        }

        void IRepository.Update<T>(T entity)
        {
            _testContext.Set<T>().Update(entity);
        }
    }
}
