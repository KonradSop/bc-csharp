using System;
using System.Threading;

namespace Org.BouncyCastle.Utilities
{
    /// <summary>Utility methods for working with objects.</summary>
    public static class Objects
    {
        /// <summary>Get the hash code of an object, or zero if the object is null.</summary>
        /// <param name="obj">The object, or null.</param>
        /// <returns>The hash code.</returns>
        public static int GetHashCode(object obj)
        {
            return null == obj ? 0 : obj.GetHashCode();
        }

        internal static TValue EnsureSingletonInitialized<TValue, TArg>(ref TValue value, TArg arg,
            Func<TArg, TValue> initialize)
            where TValue : class
        {
            TValue currentValue = Volatile.Read(ref value);
            if (null != currentValue)
                return currentValue;

            TValue candidateValue = initialize(arg);

            return Interlocked.CompareExchange(ref value, candidateValue, null) ?? candidateValue;
        }
    }
}
