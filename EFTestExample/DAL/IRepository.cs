using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFTestExample.DAL
{

    public interface IRepository
    {
        IQueryable<T> Entity<T>() where T : class, IEntityWithKey;

        void Add<T>(T entity) where T : class, IEntityWithKey;

        void Remove<T>(T entity) where T : class, IEntityWithKey;

        void SaveChanges();
    }

    public class BasicRepository<TContext> : IRepository, IDisposable
        where TContext : DbContext
        
    {
        private TContext _context;

        public BasicRepository(TContext context)
        {
            _context = context;
        }

        public IQueryable<T> Entity<T>() where T : class, IEntityWithKey
        {
            return _context.Set<T>();
        }

        public void Add<T>(T entity) where T : class, IEntityWithKey
        {
            _context.Set<T>().Add(entity);
        }

        public void Remove<T>(T entity) where T : class, IEntityWithKey
        {
            _context.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IEntityWithKey
    {
        int Id { get; }
    }

    public static class RepositoryExtentions
    {
        public static T Get<T>(this IQueryable<T> query, int id)
            where T : class, IEntityWithKey
        {
            return query.Where(e => e.Id == id).FirstOrDefault();
        }

        public static T Get<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression)
            where T : class, IEntityWithKey
        {
            return query.Where(expression).SingleOrDefault();
        }

        public static IReadOnlyList<T> GetAll<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression)
            where T : class, IEntityWithKey
        {
            return query.Where(expression).ToArray();
        }
    }
}
