// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Argument.cs" company="James Dibble">
//    Copyright 2012 James Dibble
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace System
{
    /// <summary>
    /// Static methods for verifying public method parameters.
    /// </summary>
    public static class Argument
    {
        /// <summary>
        /// Check an argument is not null.
        /// </summary>
        /// <param name="argument">The object to verify.</param>
        /// <param name="argumentName">The name of the argument being checked for use in exception.</param>
        /// <typeparam name="TArg">The type of the argument</typeparam>
        /// <exception cref="ArgumentNullException">The <paramref name="argument"/> was null.</exception>
        public static void CannotBeNull<TArg>(TArg argument, string argumentName) where TArg : class
        {
            Argument.CannotBeNull(argument, argumentName, string.Empty);
        }

        /// <summary>
        /// Check an argument is not null and throw an argument with a message.
        /// </summary>
        /// <param name="argument">The argument to verify.</param>
        /// <param name="argumentName">The name of the argument being checked for use in exception.</param>
        /// <param name="message">The message to return to the calling method.</param>
        /// <typeparam name="TArg">The type of the argument</typeparam>
        /// <exception cref="ArgumentNullException">The <paramref name="argument"/> was null.</exception>
        public static void CannotBeNull<TArg>(TArg argument, string argumentName, string message) where TArg : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName, message);
            }
        }

        /// <summary>
        /// Verify an argument using an expression.
        /// </summary>
        /// <param name="argument">The argument to verify.</param>
        /// <param name="argumentName">The name of the argument being checked for use in exception.</param>
        /// <param name="validationExpression">The expression that validates the argument.</param>
        /// <typeparam name="TArg">The type of the argument</typeparam>
        /// <exception cref="ArgumentNullException">The <paramref name="argument"/> was null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The argument does not conform to the validation expression.</exception>
        public static void CannotBe<TArg>(TArg argument, string argumentName, Func<TArg, bool> validationExpression) where TArg : class
        {
            Argument.CannotBe(argument, argumentName, validationExpression, string.Empty);
        }

        /// <summary>
        /// Verify an argument using an expression.
        /// </summary>
        /// <param name="argument">The argument to verify.</param>
        /// <param name="argumentName">The name of the argument being checked for use in exception.</param>
        /// <param name="validationExpression">The expression that validates the argument.</param>
        /// <param name="message">The message to return to the calling method.</param>
        /// <typeparam name="TArg">The type of the argument</typeparam>
        /// <exception cref="ArgumentNullException">The <paramref name="argument"/> was null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The argument does not conform to the validation expression.</exception>
        public static void CannotBe<TArg>(TArg argument, string argumentName, Func<TArg, bool> validationExpression, string message) where TArg : class
        {
            Argument.CannotBeNull(argument, argumentName);

            if (!validationExpression(argument))
            {
                throw new ArgumentOutOfRangeException(argumentName, argument, message);
            }
        }
    }
}