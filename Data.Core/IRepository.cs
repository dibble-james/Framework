namespace Dibble.Framework.Data
{
    using System;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository : IDisposable
    {
    }

    public interface IRepository<TEntity> : IRepository where TEntity : IPersistedObject
    {
        /// <summary>
        /// Place a <typeparamref name="TEntity"/> into the <see cref="IRepository{TEntity}"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to add.</param>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Place a <typeparamref name="TEntity"/> into the <see cref="IRepository{TEntity}"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to add.</param>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Get the number of <typeparamref name="TEntity"/>s in he persistence store.
        /// </summary>
        /// <returns>
        /// The number of <typeparamref name="TEntity"/>s in he persistence store.
        /// </returns>
        int Count();

        /// <summary>
        /// Get the number of <typeparamref name="TEntity"/>s in he persistence store that
        /// match a criteria.
        /// </summary>
        /// <returns>
        /// The number of <typeparamref name="TEntity"/>s in he persistence store.
        /// </returns>
        int Count(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Get the number of <typeparamref name="TEntity"/>s in he persistence store.
        /// </summary>
        /// <returns>
        /// The number of <typeparamref name="TEntity"/>s in he persistence store.
        /// </returns>
        Task<int> CountAsync();

        /// <summary>
        /// Get the number of <typeparamref name="TEntity"/>s in he persistence store that
        /// match a criteria.
        /// </summary>
        /// <returns>
        /// The number of <typeparamref name="TEntity"/>s in he persistence store.
        /// </returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Delete a group of <typeparamref name="TEntity"/>s using a given criterion.
        /// </summary>
        /// <param name="where">The criteria by which to delete <typeparamref name="TEntity"/>s.</param>
        void Delete(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Delete a group of <typeparamref name="TEntity"/>s using a given criterion.
        /// </summary>
        /// <param name="where">The criteria by which to delete <typeparamref name="TEntity"/>s.</param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Get the first <typeparamref name="TEntity"/> from a collection derived from a given
        /// criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        /// <returns>The first <typeparamref name="TEntity"/> or null if none could be found.</returns>
        TEntity First(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Get the first <typeparamref name="TEntity"/> from a collection derived from a given
        /// criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        /// <returns>The first <typeparamref name="TEntity"/> or null if none could be found.</returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Retrieve all the known <typeparamref name="TEntity"/>s.
        /// </summary>
        /// <returns>All the known <typeparamref name="TEntity"/>s.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Retrieve all the known <typeparamref name="TEntity"/>s.
        /// </summary>
        /// <returns>All the known <typeparamref name="TEntity"/>s.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Retrieve a collection of <typeparamref name="TEntity"/>s defined by a criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>s.</param>
        /// <returns>A collection of <typeparamref name="TEntity"/>s.</returns>
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Retrieve a collection of <typeparamref name="TEntity"/>s defined by a criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>s.</param>
        /// <returns>A collection of <typeparamref name="TEntity"/>s.</returns>
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Get a <typeparamref name="TEntity"/> using a given criterion where it would be expected
        /// that only one should be found.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        /// <returns>The <typeparamref name="TEntity"/> or null if none could be found.</returns>
        TEntity Single(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Get a <typeparamref name="TEntity"/> using a given criterion where it would be expected
        /// that only one should be found.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        /// <returns>The <typeparamref name="TEntity"/> or null if none could be found.</returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Change a <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to change.</param>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/> to update.</param>
        TEntity Update(TEntity entity, Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Change a <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to change.</param>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/> to update.</param>
        Task<TEntity> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> where);
    }
}