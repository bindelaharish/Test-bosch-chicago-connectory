#pragma warning disable 1591
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RB.JobAssistant.Repo
{
    public interface IRepository : IDisposable
    {
        /// <summary>
        ///     Gets all objects from database
        /// </summary>
        /// <returns></returns>
        IQueryable<T> All<T>() where T : class;

        /// <summary>
        ///     Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        /// <returns></returns>
        IQueryable<T> Filter<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        ///     Gets objects from database with filting and paging.
        /// </summary>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        /// <returns></returns>
        IQueryable<T> Filter<T>(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50)
            where T : class;

        /// <summary>
        ///     Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified the filter expression</param>
        /// <returns></returns>
        bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        ///     Find object by specified expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        ///     Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        /// <returns></returns>
        Task<T> Create<T>(T t) where T : class;

        /// <summary>
        ///     Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param>
        Task<int> Delete<T>(T t) where T : class;

        /// <summary>
        ///     Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        /// <returns></returns>
        Task<int> Update<T>(T t) where T : class;

        /// <summary>
        ///     Select Single Item by specified expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        T Single<T>(Expression<Func<T, bool>> expression) where T : class;

        /// <summary>
        ///     Asynchronously save changes.
        /// </summary>
        Task<int> SaveChanges();

        /// <summary>
        ///     Save changes (blocking).
        /// </summary>
        void SaveChangesSync();


    }
}