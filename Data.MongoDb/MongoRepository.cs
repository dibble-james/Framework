// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MongoRepository.cs" company="James Dibble">
// Copyright (c) James Dibble. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Dibble.Framework.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Driver;

    /// <summary>
    /// A class to wrap and normalize access to <see cref="IMongoCollection{TDocument}"/>s.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity this repository manages.</typeparam>
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : IPersistedObject
    {
        private readonly IMongoCollection<TEntity> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public MongoRepository(IMongoCollection<TEntity> collection)
        {
            this._collection = collection;
        }

        /// <summary>
        /// Place a <typeparamref name="TEntity" /> into the <see cref="IRepository{TEntity}" />.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity" /> to add.</param>
        public TEntity Add(TEntity entity)
        {
            return this.AddAsync(entity).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Place a <typeparamref name="TEntity" /> into the <see cref="IRepository{TEntity}" />.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity" /> to add.</param>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await this._collection.InsertOneAsync(entity);

            return entity;
        }

        /// <summary>
        /// Get the number of <typeparamref name="TEntity" />s in he persistence store.
        /// </summary>
        /// <returns>
        /// The number of <typeparamref name="TEntity" />s in he persistence store.
        /// </returns>
        public int Count()
        {
            return this.CountAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get the number of <typeparamref name="TEntity" />s in he persistence store.
        /// </summary>
        /// <param name="where"></param>
        /// <returns>
        /// The number of <typeparamref name="TEntity" />s in he persistence store.
        /// </returns>
        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return this.CountAsync(where).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get the number of <typeparamref name="TEntity" />s in he persistence store.
        /// </summary>
        /// <returns>
        /// The number of <typeparamref name="TEntity" />s in he persistence store.
        /// </returns>
        public async Task<int> CountAsync()
        {
            return (int)(await this._collection.CountAsync(FilterDefinition<TEntity>.Empty));
        }

        /// <summary>
        /// Get the number of <typeparamref name="TEntity" />s in he persistence store.
        /// </summary>
        /// <param name="where"></param>
        /// <returns>
        /// The number of <typeparamref name="TEntity" />s in he persistence store.
        /// </returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            return (int)(await this._collection.CountAsync(where));
        }

        /// <summary>
        /// Delete a group of <typeparamref name="TEntity" />s using a given criterion.
        /// </summary>
        /// <param name="where">The criteria by which to delete <typeparamref name="TEntity" />s.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            this.DeleteAsync(where).Wait();
        }

        /// <summary>
        /// Remove a <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Task DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            return this._collection.DeleteManyAsync(where);
        }

        /// <summary>
        /// Get the first <typeparamref name="TEntity" /> from a collection derived from a given
        /// criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity" />.</param>
        /// <returns>
        /// The first <typeparamref name="TEntity" /> or null if none could be found.
        /// </returns>
        public TEntity First(Expression<Func<TEntity, bool>> where)
        {
            return this.FirstAsync(where).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get the first <typeparamref name="TEntity" /> from a collection derived from a given
        /// criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity" />.</param>
        /// <returns>
        /// The first <typeparamref name="TEntity" /> or null if none could be found.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> where)
        {
            return this._collection.Find(where).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieve all the known <typeparamref name="TEntity" />s.
        /// </summary>
        /// <returns>
        /// All the known <typeparamref name="TEntity" />s.
        /// </returns>
        public IEnumerable<TEntity> GetAll()
        {
            return this.GetAllAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Retrieve all the known <typeparamref name="TEntity" />s.
        /// </summary>
        /// <returns>
        /// All the known <typeparamref name="TEntity" />s.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this._collection.Find(FilterDefinition<TEntity>.Empty).ToListAsync();
        }

        /// <summary>
        /// Retrieve a collection of <typeparamref name="TEntity" />s defined by a criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity" />s.</param>
        /// <returns>
        /// A collection of <typeparamref name="TEntity" />s.
        /// </returns>
        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return this.GetManyAsync(where).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Retrieve a collection of <typeparamref name="TEntity" />s defined by a criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity" />s.</param>
        /// <returns>
        /// A collection of <typeparamref name="TEntity" />s.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await this._collection.Find(where).ToListAsync();
        }

        /// <summary>
        /// Get a <typeparamref name="TEntity" /> using a given criterion where it would be expected
        /// that only one should be found.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity" />.</param>
        /// <returns>
        /// The <typeparamref name="TEntity" /> or null if none could be found.
        /// </returns>
        public TEntity Single(Expression<Func<TEntity, bool>> where)
        {
            return this.SingleAsync(where).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get a <typeparamref name="TEntity" /> using a given criterion where it would be expected
        /// that only one should be found.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity" />.</param>
        /// <returns>
        /// The <typeparamref name="TEntity" /> or null if none could be found.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> where)
        {
            return this._collection.Find(where).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Change a <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity" /> to change.</param>
        public TEntity Update(TEntity entity, Expression<Func<TEntity, bool>> where)
        {
            return this.UpdateAsync(entity, where).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Change a <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity" /> to change.</param>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity" />.</param>
        public async Task<TEntity> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> where)
        {
            await this._collection.ReplaceOneAsync(where, entity);

            return entity;
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
                }

                this.disposedValue = true;
            }
        }
        #endregion
    }
}