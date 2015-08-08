// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="James Dibble">
//    Copyright 2012 James Dibble
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace System
{
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The system extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Get a <typeparamref name="TAttribute"/> from an object.
        /// </summary>
        /// <param name="instance">The object instance to extract the attribute from.</param>
        /// <typeparam name="TAttribute">The type of attribute to look for.</typeparam>
        /// <returns>The first instance of the custom attribute or null if none were available.</returns>
        public static TAttribute Attribute<TAttribute>(this object instance) where TAttribute : Attribute
        {
            return instance.GetType().Attribute<TAttribute>();
        }

        /// <summary>
        /// Get a <typeparamref name="TAttribute"/> from a type.
        /// </summary>
        /// <param name="type">The object instance to extract the attribute from.</param>
        /// <typeparam name="TAttribute">The type of attribute to look for.</typeparam>
        /// <returns>The first instance of the custom attribute or null if none were available.</returns>
        public static TAttribute Attribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            var attribute = type.GetTypeInfo().GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault();

            return attribute;
        }
    }
}