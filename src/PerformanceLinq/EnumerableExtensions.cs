using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceLinq
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IGrouping<byte, TSource>> GroupByByte<TSource>(this IEnumerable<TSource> source, Func<TSource, byte> keySelector)
        {
            return new GroupedByteEnumerable<TSource, TSource>(source, keySelector, x => x);
        }
    }
}
