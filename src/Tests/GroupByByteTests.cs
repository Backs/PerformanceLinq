using FluentAssertions;
using NUnit.Framework;
using PerformanceLinq;
using System;
using System.Collections;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class GroupByByteTests
    {
        [Test]
        [TestCaseSource(nameof(collections))]
        public void SimpleCollectionTest(byte[] collection)
        {
            var expected = collection.GroupBy(o => o).ToArray();
            var result = collection.GroupByByte(o => o).ToArray();

            result.Should().BeEquivalentTo(expected);
        }

        private static readonly IEnumerable collections = new[]
        {
            new byte[0],
            new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0},
            new byte[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            new byte[] {1, 1, 1, 1, 1, 2, 2, 2, 2, 2}
        };

        [Test]
        public void ObjectCollectionTest()
        {
            var collection = Enumerable.Range(0, 100)
                .Select(o => new Tuple<byte, string>((byte)o, "test"))
                .ToArray();

            var expected = collection.GroupBy(o => o.Item1).ToArray();
            var result = collection.GroupByByte(o => o.Item1).ToArray();

            result.Should().BeEquivalentTo(expected);
        }
    }
}
