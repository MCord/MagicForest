namespace Solver.test
{
    using NUnit.Framework;

    class LookupTest<T>
        where T : BaseSolver, new()
    {
        private BaseSolver solver;
        private int[] lookup;

        public static LookupTest<T> GraphOfSize(int size)
        {
            var solver = new T();
            return new LookupTest<T>
            {
                solver = solver,
                lookup = solver.CreateLookupTable(size)
            };
        }

        public LookupTest<T> AddEdge(int from, int to)
        {
            solver.Union(lookup, from, to);
            return this;
        }

        public void Verify(params int[] expected)
        {
            CollectionAssert.AreEqual(expected, lookup);
        }
    }
}