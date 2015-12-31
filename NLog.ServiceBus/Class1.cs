using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLog.ServiceBus
{
    public class ConcurrentHashSet<T> : ISet<T>, IDisposable
    {
        private readonly ISet<T> _hashSet = new HashSet<T>();
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        
        public IEnumerator<T> GetEnumerator()
        {
            return _hashSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _hashSet).GetEnumerator();
        }

        public void Add(T item)
        {
            ((ISet<T>) this).Add(item);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            _hashSet.UnionWith(other);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            _hashSet.IntersectWith(other);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            _hashSet.ExceptWith(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            _hashSet.SymmetricExceptWith(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return _hashSet.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return _hashSet.IsSupersetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return _hashSet.IsProperSupersetOf(other);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return _hashSet.IsProperSubsetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return _hashSet.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return _hashSet.SetEquals(other);
        }

        bool ISet<T>.Add(T item)
        {
            Trace.TraceInformation("ConcurrentHastSet.Add");
            try
            {
                _lock.EnterWriteLock();
                return _hashSet.Add(item);
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            try
            {
                _lock.EnterWriteLock();
                _hashSet.Clear();
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }

        public bool Contains(T item)
        {
            try
            {
                _lock.EnterReadLock();
                return _hashSet.Contains(item);
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _hashSet.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            Trace.TraceInformation("ConcurrentHastSet.Remove");
            try
            {
                _lock.EnterWriteLock();
                return _hashSet.Remove(item);
            }
            finally
            {
                if (_lock.IsWriteLockHeld) _lock.ExitWriteLock();
            }
        }

        public int Count
        {
            get
            {
                Trace.TraceInformation("ConcurrentHastSet.Count");
                try
                {
                    _lock.EnterReadLock();
                    return _hashSet.Count;
                }
                finally
                {
                    if (_lock.IsReadLockHeld) _lock.ExitReadLock();
                }
            }
        }

        public bool IsReadOnly
        {
            get { return _hashSet.IsReadOnly; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                if (_lock != null)
                    _lock.Dispose();
        }
        ~ConcurrentHashSet()
        {
            Dispose(false);
        }

        public void CopyTo(out T[] items)
        {
            Trace.TraceInformation("ConcurrentHastSet.CopyTo");
            try
            {
                _lock.EnterReadLock();
                items = new T[Count];
                CopyTo(items, 0);
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
        }
    }
}
