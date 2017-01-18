namespace Solver.test
{
    using System.Linq;
    using NUnit.Framework;

    internal class FastFindTests
    {
        private readonly FastFindSolver subject = new FastFindSolver();

        private static LookupTest<FastFindSolver> GraphOfSize(int size)
        {
            return LookupTest<FastFindSolver>.GraphOfSize(size);
        }

        [Test]
        public void NoEdgeGraphShouldHaveNPartitions()
        {
            Assert.AreEqual(5, subject.Solve(Enumerable.Empty<Edge>(), 5));
        }

        [Test]
        public void VerifyLookupTableAfterSingleUnion()
        {
            GraphOfSize(2)
                .AddEdge(0, 1)
                .Verify(-2, 0, -1);
        }

        [Test]
        public void UnionShouldPointToRoot()
        {
            GraphOfSize(5)
                .AddEdge(0, 1).AddEdge(1, 2).AddEdge(2, 3).AddEdge(3, 4)
                .Verify(-5, 0, 0, 0, 0, -1);
        }

        [Test]
        public void UnionShouldDetectLoop()
        {
            GraphOfSize(5)
                .AddEdge(0, 1).AddEdge(1, 2).AddEdge(2, 3).AddEdge(3, 4)
                .AddEdge(4, 2)
                .Verify(5, 5, 5, 5, 5, -6);
        }

        [Test]
        public void ExampleCase()
        {
            subject.Solve(new[]
            {
                new Edge(0, 1),
                new Edge(0, 2),
                new Edge(3, 4),
                new Edge(3, 5),
                new Edge(4, 5)
            }, 7);
        }

    }
}