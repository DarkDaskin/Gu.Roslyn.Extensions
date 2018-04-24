namespace Gu.Roslyn.AnalyzerExtensions
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    [DebuggerTypeProxy(typeof(PooledSetDebugView<>))]
    [DebuggerDisplay("Count = {this.Count}, refCount = {this.refCount}")]
    public sealed class PooledSet<T> : IDisposable, IReadOnlyCollection<T>
    {
        private static readonly ConcurrentQueue<PooledSet<T>> Cache = new ConcurrentQueue<PooledSet<T>>();
        private readonly HashSet<T> inner = new HashSet<T>();

        private int refCount;

        private PooledSet()
        {
        }

        /// <inheritdoc />
        public int Count => this.inner.Count;

        /// <summary>
        /// The result from this call is meant to be used in a using.
        /// </summary>
        /// <returns>A <see cref="PooledSet{T}"/></returns>
        public static PooledSet<T> Borrow()
        {
            if (Cache.TryDequeue(out var set))
            {
                Debug.Assert(set.refCount == 0, $"{nameof(Borrow)} set.refCount == {set.refCount}");
                set.refCount = 1;
                return set;
            }

            return new PooledSet<T> { refCount = 1 };
        }

        /// <summary>
        /// The result from this call is meant to be used in a using.
        /// </summary>
        /// <param name="set">A previously borrowed set or null.</param>
        /// <returns>A newly borrowed set or the same instance with incremented ref count.</returns>
        public static PooledSet<T> BorrowOrIncrementUsage(PooledSet<T> set)
        {
            if (set == null)
            {
                return Borrow();
            }

            var current = Interlocked.Increment(ref set.refCount);
            Debug.Assert(current >= 1, $"{nameof(BorrowOrIncrementUsage)} set.refCount == {current}");
            return set;
        }

        /// <summary>
        /// Add an item to the set.
        /// </summary>
        /// <param name="item">The item</param>
        /// <returns>True if the item was added.</returns>
        public bool Add(T item)
        {
            this.ThrowIfDisposed();
            return this.inner.Add(item);
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => this.inner.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <inheritdoc />
        void IDisposable.Dispose()
        {
            if (Interlocked.Decrement(ref this.refCount) == 0)
            {
                Debug.Assert(!Cache.Contains(this), "!Cache.Contains(this)");
                this.inner.Clear();
                Cache.Enqueue(this);
            }
        }

        [Conditional("DEBUG")]
        private void ThrowIfDisposed()
        {
            if (this.refCount <= 0)
            {
                Debug.Assert(this.refCount == 0, $"{nameof(this.ThrowIfDisposed)} set.refCount == {this.refCount}");
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }
    }
}
