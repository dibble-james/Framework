// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionExtensions.cs" company="James Dibble">
//    Copyright 2012 James Dibble
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace System
{
    using System.Linq.Expressions;

    /// <summary>
    /// Extensions for the <see cref="Expression"/> class.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Using a delegate, retrieve the name of the method called in the lambda.
        /// </summary>
        /// <param name="expression">The lambda expressing the method.</param>
        /// <returns>The name of the method.</returns>
        /// <exception cref="ArgumentException">The expression does not contain a method call at its root.</exception>
        /// <exception cref="ArgumentNullException">The expression argument is null.</exception>
        public static string MethodName(Expression<Action> expression)
        {
            Argument.CannotBeNull(expression, "Expression cannot be null");

            var method = expression.Body as MethodCallExpression;

            if (method == null)
            {
                throw new ArgumentException("Expression must only contain a method call.");
            }

            return method.Method.Name;
        }
    }
}