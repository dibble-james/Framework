namespace Dibble.Framework.Data
{
    using System;

    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        Task CommitAsync();

        IRepository<T> GetRepository<T>() where T : class, IPersistedObject;
    }
}