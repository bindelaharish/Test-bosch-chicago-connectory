#pragma warning disable 1591
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RB.JobAssistant.Util;

namespace RB.JobAssistant.Repo
{
    /**
        DZone inspired Generic Repository implementation. Modified for Entity Framework Core and now a work in-progress.
        See https://dzone.com/articles/implementing-repository for background details.
    */
    public class Repository : IRepository
    {
        private readonly ILogger<Repository> _logger;
        private readonly DbContext _repoDbContext;

        public Repository(DbContext context)
        {
            _logger = ApplicationLogging.CreateTypeLogger<Repository>();
            _repoDbContext = context;
        }

        public T Single<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return All<T>().FirstOrDefault(expression);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return _repoDbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> Filter<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _repoDbContext.Set<T>().Where(predicate).AsQueryable();
        }

        public async Task<IQueryable<T>> FilterAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await new Task<IQueryable<T>>(() => _repoDbContext.Set<T>().Where(predicate).AsQueryable());
        }

        public virtual IQueryable<T> Filter<T>(Expression<Func<T, bool>> filter, out int total, int index = 0,
            int size = 50) where T : class
        {
            var skipCount = index * size;
            var resetSet = filter != null
                ? _repoDbContext.Set<T>().Where(filter).AsQueryable()
                : _repoDbContext.Set<T>().AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        public virtual async Task<T> Create<T>(T TObject) where T : class
        {
            using (_logger.BeginScope(typeof(Repository)))
            {
                var newEntry = _repoDbContext.Set<T>().Add(TObject);
                await _repoDbContext.SaveChangesAsync();
                return newEntry.Entity;
            }
        }

        public virtual async Task<int> Delete<T>(T TObject) where T : class
        {
            using (_logger.BeginScope(typeof(Repository)))
            {
                _repoDbContext.Set<T>().Remove(TObject);
                return await _repoDbContext.SaveChangesAsync();
            }
        }

        public virtual async Task<int> Update<T>(T TObject) where T : class
        {
            using (_logger.BeginScope(typeof(Repository)))
            {
                var entry = _repoDbContext.Entry(TObject);
                _repoDbContext.Set<T>().Attach(TObject);
                entry.State = EntityState.Modified;
                return await _repoDbContext.SaveChangesAsync();
            }
        }

        public bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _repoDbContext.Set<T>().Count(predicate) > 0;
        }

        public virtual async Task<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _repoDbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<int> SaveChanges()
        {
            return await _repoDbContext.SaveChangesAsync();
        }

        public void SaveChangesSync()
        {
            _repoDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _repoDbContext?.Dispose();
        }

        public void CommitChanges()
        {
            _repoDbContext.SaveChanges();
        }

        public virtual async Task<int> Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
                _repoDbContext.Set<T>().Remove(obj);
            return await _repoDbContext.SaveChangesAsync();
        }
    }
}