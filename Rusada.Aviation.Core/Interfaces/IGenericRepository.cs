namespace Rusada.Aviation.Core.Interface
{
    public interface IGenericRepository<T> : IDisposable
        where T : class
    {
        /// <summary>
        /// Save new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Insert(T entity);

        /// <summary>
        /// Update an exsisting entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(T entity);

        /// <summary>
        /// Return all entities as queryable
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<T>> GetAll();

        /// <summary>
        /// Save DB changes
        /// </summary>
        Task Save();
    }
}
