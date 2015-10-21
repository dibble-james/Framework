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
        private readonly IDbSet<TEntity> _entities;

        /// <summary>
        /// Initialises a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context to work from.</param>
        public Repository(IEntityUnitOfWork context)
        {
            this._context = context.CurrentContext;
            this._entities = this._context.Set<TEntity>();
        }

        public int Count()
        {
            return this._entities.Count();
        }
        
        public async Task<int> CountAsync()
        {
            return await this._entities.CountAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> @where)
        {
            return this._entities.Count(@where);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            return await this._entities.CountAsync(where);
        }

        /// <summary>
        /// Place a <typeparamref name="TEntity"/> into the <see cref="IRepository{TEntity}"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to add.</param>
        public void Add(TEntity entity)
        {
            this._entities.Add(entity);
        }

        /// <summary>
        /// Change a <typeparamref name="TEntity"/>. Only call this if you are updating an entity 
        /// that has not previously come from a <see cref="DbContext"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to change.</param>
        public void Update(TEntity entity)
        {
            this._entities.Attach(entity);
            this._context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Remove a <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to remove.</param>
        public void Delete(TEntity entity)
        {
            this._entities.Remove(entity);
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
                this.Delete(entity);
            }
        }

        /// <summary>
        /// Get a <typeparamref name="TEntity"/> using a given criterion where it would be expected
        /// that only one should be found.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        /// <returns>The <typeparamref name="TEntity"/> or null if none could be found.</returns>
        public TEntity Single(Expression<Func<TEntity, bool>> @where)
        {
            var entity = this._entities.SingleOrDefault(where);

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
            var entity = await this._entities.SingleOrDefaultAsync(where);

            return entity;
        }

        /// <summary>
        /// Get the first <typeparamref name="TEntity"/> from a collection derived from a given
        /// criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        /// <returns>The first <typeparamref name="TEntity"/> or null if none could be found.</returns>
        public TEntity First(Expression<Func<TEntity, bool>> @where)
        {
            var entity = this._entities.FirstOrDefault(where);

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
            var entity = await this._entities.FirstOrDefaultAsync(where);

            return entity;
        }

        /// <summary>
        /// Retrieve all the known <typeparamref name="TEntity"/>s.
        /// </summary>
        /// <returns>All the known <typeparamref name="TEntity"/>s.</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return this._entities;
        }

        /// <summary>
        /// Retrieve all the known <typeparamref name="TEntity"/>s.
        /// </summary>
        /// <returns>All the known <typeparamref name="TEntity"/>s.</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this._entities.ToListAsync();
        }

        /// <summary>
        /// Retrieve a collection of <typeparamref name="TEntity"/>s defined by a criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>s.</param>
        /// <returns>A collection of <typeparamref name="TEntity"/>s.</returns>
        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> @where)
        {
            var entities = this._entities.Where(where);

            return entities;
        }

        /// <summary>
        /// Retrieve a collection of <typeparamref name="TEntity"/>s defined by a criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>s.</param>
        /// <returns>A collection of <typeparamref name="TEntity"/>s.</returns>
        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            var entities = await this._entities.Where(where).ToListAsync();

            return entities;
        }
    }
}