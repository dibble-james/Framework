namespace Dibble.Framework.Data.UnitOfWork
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        DbContext CurrentContext { get; }

        void Commit();

        Task CommitAsync();

        IRepository<T> GetRepository<T>() where T : class, IPersistedObject;
    }
}