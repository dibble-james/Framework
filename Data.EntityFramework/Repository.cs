namespace Dibble.Framework.Data.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// A class to wrap and normalize access to database contexts.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity this repository manages.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IPersistedObject
    {
        private readonly DbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context to work from.</param>
        public Repository(IEntityUnitOfWork context)
        {
            this._context = context.CurrentContext;
        }

        private IDbSet<TEntity> Entities
        {
            get
            {
                return this._context.Set<TEntity>();
            }
        }

        /// <summary>
        /// Place a <typeparamref name="TEntity"/> into the <see cref="IRepository{TEntity}"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to add.</param>
        public TEntity Add(TEntity entity)
        {
            this.Entities.Add(entity);

            return entity;
        }

        /// <summary>
        /// Place a <typeparamref name="TEntity"/> into the <see cref="IRepository{TEntity}"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to add.</param>
        public Task<TEntity> AddAsync(TEntity entity)
        {
            return Task.FromResult(this.Add(entity));
        }

        /// <summary>
        /// Get the number of <typeparamref name="TEntity"/>s in he persistence store.
        /// </summary>
        /// <returns>
        /// The number of <typeparamref name="TEntity"/>s in he persistence store.
        /// </returns>
        public int Count()
        {
            return this.Entities.Count();
        }

        /// <summary>
        /// Get the number of <typeparamref name="TEntity"/>s in he persistence store that
        /// match a criteria.
        /// </summary>
        /// <returns>
        /// The number of <typeparamref name="TEntity"/>s in he persistence store.
        /// </returns>
        public int Count(Expression<Func<TEntity, bool>> @where)
        {
            return this.Entities.Count(@where);
        }

        /// <summary>
        /// Get the number of <typeparamref name="TEntity"/>s in he persistence store.
        /// </summary>
        /// <returns>
        /// The number of <typeparamref name="TEntity"/>s in he persistence store.
        /// </returns>
        public async Task<int> CountAsync()
        {
            return await this.Entities.CountAsync();
        }

        /// <summary>
        /// Get the number of <typeparamref name="TEntity"/>s in he persistence store that
        /// match a criteria.
        /// </summary>
        /// <returns>
        /// The number of <typeparamref name="TEntity"/>s in he persistence store.
        /// </returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            return await this.Entities.CountAsync(where);
        }

        /// <summary>
        /// Delete a group of <typeparamref name="TEntity"/>s using a given criterion.
        /// </summary>
        /// <param name="where">The criteria by which to delete <typeparamref name="TEntity"/>s.</param>
        public void Delete(Expression<Func<TEntity, bool>> @where)
        {
            var objectsToDelete = this.GetMany(@where);

            foreach (var entity in objectsToDelete)
            {
                this.Entities.Remove(entity);
            }
        }

        /// <summary>
        /// Remove a <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to remove.</param>
        public Task DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            this.Delete(where);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Get the first <typeparamref name="TEntity"/> from a collection derived from a given
        /// criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        /// <returns>The first <typeparamref name="TEntity"/> or null if none could be found.</returns>
        public TEntity First(Expression<Func<TEntity, bool>> @where)
        {
            var entity = this.Entities.FirstOrDefault(where);

            return entity;
        }

        /// <summary>
        /// Get the first <typeparamref name="TEntity"/> from a collection derived from a given
        /// criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        /// <returns>The first <typeparamref name="TEntity"/> or null if none could be found.</returns>
        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> where)
        {
            var entity = await this.Entities.FirstOrDefaultAsync(where);

            return entity;
        }

        /// <summary>
        /// Retrieve all the known <typeparamref name="TEntity"/>s.
        /// </summary>
        /// <returns>All the known <typeparamref name="TEntity"/>s.</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return this.Entities;
        }

        /// <summary>
        /// Retrieve all the known <typeparamref name="TEntity"/>s.
        /// </summary>
        /// <returns>All the known <typeparamref name="TEntity"/>s.</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.Entities.ToListAsync();
        }

        /// <summary>
        /// Retrieve a collection of <typeparamref name="TEntity"/>s defined by a criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>s.</param>
        /// <returns>A collection of <typeparamref name="TEntity"/>s.</returns>
        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> @where)
        {
            var entities = this.Entities.Where(where);

            return entities;
        }

        /// <summary>
        /// Retrieve a collection of <typeparamref name="TEntity"/>s defined by a criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>s.</param>
        /// <returns>A collection of <typeparamref name="TEntity"/>s.</returns>
        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            var entities = await this.Entities.Where(where).ToListAsync();

            return entities;
        }

        /// <summary>
        /// Get a <typeparamref name="TEntity"/> using a given criterion where it would be expected
        /// that only one should be found.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        /// <returns>The <typeparamref name="TEntity"/> or null if none could be found.</returns>
        public TEntity Single(Expression<Func<TEntity, bool>> @where)
        {
            var entity = this.Entities.SingleOrDefault(where);

            return entity;
        }

        /// <summary>
        /// Get a <typeparamref name="TEntity"/> using a given criterion where it would be expected
        /// that only one should be found.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        /// <returns>The <typeparamref name="TEntity"/> or null if none could be found.</returns>
        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> where)
        {
            var entity = await this.Entities.SingleOrDefaultAsync(where);

            return entity;
        }

        /// <summary>
        /// Change a <typeparamref name="TEntity"/>.
        /// </summary>
        /// <remarks>Entity Framework tracks changes.</remarks>
        /// <param name="entity">The <typeparamref name="TEntity"/> to change.</param>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        public TEntity Update(TEntity entity, Expression<Func<TEntity, bool>> @where)
        {
            return entity;
        }
        /// <summary>
        /// Change a <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to change.</param>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        public Task<TEntity> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> @where)
        {
            return Task.FromResult(this.Update(entity, @where));
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }

                this.disposedValue = true;
            }
        }
        #endregion
    }
}