// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MongoUnitOfWork.cs" company="James Dibble">
// Copyright (c) James Dibble. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Dibble.Framework.Data.MongoDb
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Driver;

    /// <summary>
    /// A MongoDB implementation of <see cref="IUnitOfWork"/>.  MongoDB is not really designed for
    /// the unit of work pattern so <see cref="IUnitOfWork.Commit"/> does nothing.
    /// </summary>
    public class MongoUnitOfWork : IUnitOfWork
    {
        private readonly IMongoDatabase database;
        private readonly IDictionary<Type, IRepository> repositoryCache;

        public MongoUnitOfWork(IMongoDatabase database)
        {
            this.database = database;

            this.repositoryCache = new ConcurrentDictionary<Type, IRepository>();
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
        }

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <remarks></remarks>
        public Task CommitAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IPersistedObject
        {
            if (this.repositoryCache.ContainsKey(typeof(TEntity)))
            {
                return this.repositoryCache[typeof(TEntity)] as IRepository<TEntity>;
            }

            var collection = this.database.GetCollection<TEntity>(typeof(TEntity).Name);

            var repo = new MongoRepository<TEntity>(collection);

            this.repositoryCache.Add(typeof(TEntity), repo);

            return repo;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposedValue)
            {
                return;
            }

            if (disposing)
            {
                foreach (var kvp in this.repositoryCache)
                {
                    kvp.Value.Dispose();
                }
            }

            this.repositoryCache.Clear();

            this.disposedValue = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
           this.Dispose(true);
        }
        #endregion
    }
}