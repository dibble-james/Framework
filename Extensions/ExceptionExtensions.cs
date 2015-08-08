// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionExtensions.cs" company="James Dibble">
//    Copyright 2012 James Dibble
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace System
{
    /// <summary>
    /// Extension methods for the <see cref="System.Exception"/> class.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Find the inner most exception.
        /// </summary>
        /// <param name="exception">The exception to dig into.</param>
        /// <returns>The inner most exception.</returns>
        public static Exception InnerMostException(this Exception exception)
        {
            if(exception.InnerException != null)
            {
                return exception.InnerException.InnerMostException();
            }

            return exception;
        }

        /// <summary>
        /// Get the inner most exception or the exception at a particular depth.
        /// </summary>
        /// <param name="exception">The exception to dig into.</param>
        /// <param name="maxDepth">How many inner exceptions to dig too.</param>
        /// <returns>The inner most exception or the exception at <paramref name="maxDepth"/>.</returns>
        public static Exception InnerMostException(this Exception exception, int maxDepth)
        {
            if (exception.InnerException != null || maxDepth == 0)
            {
                return exception.InnerException.InnerMostException(maxDepth--);
            }

            return exception;
        }

        /// <summary>
        /// Get the inner most exception or the first exception of type <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException">The type of exception to find.</typeparam>
        /// <param name="exception">The exception to dig into.</param>
        /// <returns>The inner most exception or the first exception of type <typeparamref name="TException"/>.</returns>
        public static Exception InnerMostException<TException>(this Exception exception) 
            where TException : Exception
        {
            if (exception.InnerException != null && !(exception.InnerException is TException))
            {
                return exception.InnerException.InnerMostException<TException>();
            }

            if(exception.InnerException != null && exception.InnerException is TException)
            {
                return exception.InnerException;
            }

            return exception;
        }    
    }
}
