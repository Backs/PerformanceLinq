using BenchmarkDotNet.Attributes;
using PerformanceLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Configs;

namespace Benchmark
{
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class GroupByByteBenchmark
    {
        [Params(10, 100, 1000, 10000)]
        public int count;

        private byte[] randomData;
        private byte[] zeroData;

        [GlobalSetup]
        public void Setup()
        {
            randomData = new byte[count];
            zeroData = new byte[count];
            new Random(42).NextBytes(randomData);
        }

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Random")]
        public IEnumerator<IGrouping<byte, byte>> GroupBy()
        {
            return randomData.GroupBy(o => o).GetEnumerator();
        }


        [Benchmark]
        [BenchmarkCategory("Random")]
        public IEnumerator<IGrouping<byte, byte>> GroupByByte()
        {
            return randomData.GroupByByte(o => o).GetEnumerator();
        }

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Zero")]
        public IEnumerator<IGrouping<byte, byte>> ZeroGroupBy()
        {
            return zeroData.GroupBy(o => o).GetEnumerator();
        }

        [Benchmark]
        [BenchmarkCategory("Zero")]
        public IEnumerator<IGrouping<byte, byte>> ZeroGroupByByte()
        {
            return zeroData.GroupByByte(o => o).GetEnumerator();
        }
    }
}
