namespace Dibble.Framework.Data.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    /// <summary>
    /// A class to store a database context to be injected into services so they all shared
    /// the same data resource.
    /// </summary>
    public class UnitOfWork : IEntityUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IDictionary<Type, IRepository> _repositoryCache;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The current database context.</param>
        public UnitOfWork(DbContext context)
        {
            this._disposed = false;
            this._context = context;
            this._repositoryCache = new Dictionary<Type, IRepository>();
        }

        /// <summary>
        /// Gets the active <see cref="DbContext"/> for this <see cref="IUnitOfWork"/>.
        /// </summary>
        public DbContext CurrentContext
        {
            get
            {
                return this._context;
            }
        }

        /// <summary>
        /// Save call changes that have been made to the context.
        /// </summary>
        public void Commit()
        {
            this._context.ChangeTracker.DetectChanges();
            this._context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            this._context.ChangeTracker.DetectChanges();
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieve an instance of <see cref="IRepository"/> to access the persistence layer.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IRepository"/> to create.</typeparam>
        /// <returns>An instance of <see cref="IRepository"/> to access the persistence layer.</returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IPersistedObject
        {
            if (this._repositoryCache.ContainsKey(typeof(TEntity)))
            {
                return this._repositoryCache[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repo = new Repository<TEntity>(this);

            this._repositoryCache.Add(typeof(TEntity), repo);

            return repo;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            
            this._disposed = true;
        }
    }
}