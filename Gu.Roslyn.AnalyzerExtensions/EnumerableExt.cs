namespace Gu.Roslyn.AnalyzerExtensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods that avoids allocations.
    /// </summary>
    public static partial class EnumerableExt
    {
        /// <summary>
        /// Try getting the element at <paramref name="index"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="index">The index.</param>
        /// <param name="result">The element at index if found, can be null.</param>
        /// <returns>True if an element was found.</returns>
        public static bool TryElementAt<T>(this IEnumerable<T> source, int index, out T result)
        {
            result = default(T);
            if (source == null)
            {
                return false;
            }

            var current = 0;
            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (current == index)
                    {
                        result = e.Current;
                        return true;
                    }

                    current++;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        public static bool TrySingle<T>(this IEnumerable<T> source, out T result)
        {
            result = default(T);
            if (source == null)
            {
                return false;
            }

            using (var e = source.GetEnumerator())
            {
                if (e.MoveNext())
                {
                    result = e.Current;
                    if (!e.MoveNext())
                    {
                        return true;
                    }

                    return false;
                }

                result = default(T);
                return false;
            }
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/> matching <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="result">The single element matching the predicate, can be null.</param>
        /// <returns>True if an element was found.</returns>
        public static bool TrySingle<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T result)
        {
            result = default(T);
            if (source == null)
            {
                return false;
            }

            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    result = e.Current;
                    if (predicate(result))
                    {
                        while (e.MoveNext())
                        {
                            if (predicate(e.Current))
                            {
                                result = default(T);
                                return false;
                            }
                        }

                        return true;
                    }
                }
            }

            result = default(T);
            return false;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="result">The first element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        public static bool TryFirst<T>(this IEnumerable<T> source, out T result)
        {
            result = default(T);
            if (source == null)
            {
                return false;
            }

            using (var e = source.GetEnumerator())
            {
                if (e.MoveNext())
                {
                    result = e.Current;
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/> matching <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="result">The first element matching the predicate, can be null.</param>
        /// <returns>True if an element was found.</returns>
        public static bool TryFirst<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T result)
        {
            if (source == null)
            {
                result = default(T);
                return false;
            }

            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    result = e.Current;
                    if (predicate(result))
                    {
                       return true;
                    }
                }
            }

            result = default(T);
            return false;
        }
    }
}
