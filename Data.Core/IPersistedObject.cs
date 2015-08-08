// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPersistedObject.cs" company="James Dibble">
//    Copyright 2012 James Dibble
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Dibble.Framework.Data
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Implementing classes represent an object to be managed by an <see cref="IPersistenceManager"/>.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", 
        Justification = @"This is a marker interface.  
        Because we are trying to keep domain objects clean an attribute is inappropriate.")]
    public interface IPersistedObject
    {
    }
}