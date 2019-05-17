using System;
using System.Collections;
using System.Collections.Generic;

namespace IEnumerableImpl
{
    public class CustomEnumerable : IEnumerable<int>
    {
        private readonly int _start;
        private readonly int _end;

        public CustomEnumerable(int start, int end)
        {
            this._start = start;
            this._end = end;
        }

        public IEnumerator GetEnumerator()
        {
            return (this as IEnumerable<int>).GetEnumerator();
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return new CustomEnumerator(_start, _end);
        }

        private class CustomEnumerator : IEnumerator, IEnumerator<int>
        {
            private readonly int _start;
            private readonly int _end;
            private bool _disposed;
            private int _currentItem = 0;

            public CustomEnumerator(int start, int end)
            {
                this._start = start;
                this._end = end;
                Reset();
            }

            public int Current => _currentItem;

            object IEnumerator.Current => _currentItem;

            public void Dispose()
            {
                _disposed = true;
            }

            public bool MoveNext()
            {
                if (_disposed)
                    throw new ObjectDisposedException(nameof(CustomEnumerator));

                if (_currentItem >= _end)
                    return false;

                _currentItem++;
                return true;
            }

            public void Reset() => this._currentItem = this._start;
        }
    }
}