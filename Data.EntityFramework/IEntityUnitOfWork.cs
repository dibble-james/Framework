using System.Data.Entity;

namespace Dibble.Framework.Data.EntityFramework
{
    public interface IEntityUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets the active <see cref="DbContext"/> for this <see cref="IUnitOfWork"/>.
        /// </summary>
        DbContext CurrentContext { get; }
    }
}