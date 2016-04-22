using System;
using log4net;

namespace Core.Utilities
{
    public static class Guard
    {
        /// <summary>
        /// Againsts the specified assertion.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter",
            Justification = "Required for creating instances of this exception type.")]
        public static void Against<TException>(bool assertion, string message) where TException : Exception
        {
            if (assertion == false)
            {
                return;
            }
            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        /// <summary>
        /// Againsts the specified assertion.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="logger">The logger.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter",
            Justification = "Required for creating instances of this exception type.")]
        public static void Against<TException>(bool assertion, string message, ILog logger) where TException : Exception
        {
            if (assertion)
            {
                logger.Error(message);
            }
            Against<TException>(assertion, message);
        }
    }
}
