using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceLinq
{
    internal sealed class ByteLookup<TElement> : ILookup<byte, TElement>
    {
        private readonly Grouping[] groupings = new Grouping[byte.MaxValue + 1];

        public static ByteLookup<TElement> Create<TSource>(IEnumerable<TSource> source, Func<TSource, byte> keySelector, Func<TSource, TElement> elementSelector)
        {
            var result = new ByteLookup<TElement>();

            foreach (var source1 in source)
            {
                result.GetGrouping(keySelector(source1)).Add(elementSelector(source1));
                result.Count++;
            }

            return result;
        }

        private Grouping GetGrouping(byte key)
        {
            return groupings[key] ?? (groupings[key] = new Grouping(key));
        }

        public IEnumerator<IGrouping<byte, TElement>> GetEnumerator()
        {
            foreach (var g in groupings)
            {
                if (g != null && g.Count != 0)
                    yield return g;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(byte key)
        {
            return groupings[key].Any();
        }

        public int Count { get; private set; }

        public IEnumerable<TElement> this[byte key] => groupings[key];

        private class Grouping : IGrouping<byte, TElement>, IList<TElement>
        {
            private readonly List<TElement> list = new List<TElement>(4);

            public Grouping(byte key)
            {
                Key = key;
            }

            public IEnumerator<TElement> GetEnumerator()
            {
                return list.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public byte Key { get; }

            internal void Add(TElement item)
            {
                list.Add(item);
            }

            void ICollection<TElement>.Add(TElement item)
            {
                throw new NotSupportedException();
            }

            void ICollection<TElement>.Clear()
            {
                throw new NotSupportedException();
            }

            bool ICollection<TElement>.Contains(TElement item)
            {
                return list.Contains(item);
            }

            void ICollection<TElement>.CopyTo(TElement[] array, int arrayIndex)
            {
                list.CopyTo(array, arrayIndex);
            }

            bool ICollection<TElement>.Remove(TElement item)
            {
                throw new NotSupportedException();
            }

            public int Count => list.Count;

            bool ICollection<TElement>.IsReadOnly => true;

            int IList<TElement>.IndexOf(TElement item)
            {
                return list.IndexOf(item);
            }

            void IList<TElement>.Insert(int index, TElement item)
            {
                throw new NotSupportedException();
            }

            void IList<TElement>.RemoveAt(int index)
            {
                throw new NotSupportedException();
            }

            TElement IList<TElement>.this[int index]
            {
                get => list[index];
                set => throw new NotSupportedException();
            }
        }
    }
}
