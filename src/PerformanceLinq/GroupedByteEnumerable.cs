using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceLinq
{
    internal sealed class GroupedByteEnumerable<TSource, TElement> : IEnumerable<IGrouping<byte, TElement>>, IEnumerable
    {
        private readonly IEnumerable<TSource> source;
        private readonly Func<TSource, byte> keySelector;
        private readonly Func<TSource, TElement> elementSelector;

        public GroupedByteEnumerable(IEnumerable<TSource> source, Func<TSource, byte> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.elementSelector = elementSelector;
        }

        public IEnumerator<IGrouping<byte, TElement>> GetEnumerator()
        {
            return ByteLookup<TElement>
                .Create(this.source, this.keySelector, this.elementSelector)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
