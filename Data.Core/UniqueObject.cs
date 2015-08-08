// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UniqueObject.cs" company="James Dibble">
//    Copyright 2012 James Dibble
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Dibble.Framework.Data
{
    /// <summary>
    /// A base class for <see cref="IPersistedObject"/>s that can be uniquely identified.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the unique identifier.
    /// </typeparam>
    public abstract class UniqueObject<T> : IPersistedObject
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="UniqueObject{T}"/> class.
        /// </summary>
        protected UniqueObject()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="UniqueObject{T}"/> class.
        /// </summary>
        /// <param name="id">
        /// The identifier of this <see cref="UniqueObject{T}"/>.
        /// </param>
        protected UniqueObject(T id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of this <see cref="object"/>.
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// Set the unique identifier of this <see cref="IUniqueObject{T}"/>.
        /// </summary>
        /// <param name="newId">
        /// The new identifier.
        /// </param>
        public void SetNewId(T newId)
        {
            this.Id = newId;
        }
    }
}