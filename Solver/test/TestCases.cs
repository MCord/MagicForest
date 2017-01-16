namespace Solver
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    public class TestCases
    {
        [Test]
        public void Huge_Randomly_Ordered_Tree()
        {
            var td = new TestData();

            for (int i = 0; i < 600 * 1000; i += 1)
            {
                var node = td.GetNode();
                td.AddNodesFrom(node, 10);
            }

            var process = Process(td, td.GetMaxNode());

            Assert.AreEqual(600 * 1000, process);
        }

        [Test]
        public void Experiment()
        {
            var list = new List<Edge>(100)
            {
                new Edge(0, 1),
                new Edge(0, 2),
                new Edge(0, 3),
                new Edge(0, 10),

                new Edge(8, 9),

                new Edge(4, 5),
                new Edge(4, 6),
                new Edge(4, 7),
            };


            Assert.AreEqual(3, Process(list, 11));
        }

        [Test]
        public void OneHugeTree()
        {
            var td = new TestData();
            var root = td.GetNode();

            td.Create(6 * 1000 * 1000, i => new Edge(root, td.GetNode()));

            Assert.AreEqual(1, Process(td, td.GetMaxNode()));
        }

        [Test]
        public void Huge_Random_Edge_List_Deep_Tree()
        {
            var td = new TestData();
            td.CreateBranch(6 * 1000);

            Assert.AreEqual(1, Process(td, td.GetMaxNode()));
        }

        [Test]
        public void GrassForest()
        {
            var td = new TestData();
            td.Create(60 * 1000 * 1000, i => new Edge(td.GetNode(), td.GetNode()));
            Assert.AreEqual(60 * 1000 * 1000, Process(td, td.GetMaxNode()));
        }


        private static int Process(IEnumerable<Edge> edges, int nodes)
        {
            return DisjointSetSolver.Solve(edges, nodes);
        }

        [Test]
        public void OneBranch()
        {
            var td = new TestData();
            td.CreateBranch(5);

            Assert.AreEqual(1, Process(td, td.GetMaxNode()));
        }

        [Test]
        public void GraphWithNoEdge()
        {
            var td = new TestData();
            td.GetNode();
            td.GetNode();
            td.GetNode();

            Assert.AreEqual(3, Process(td, td.GetMaxNode()));
        }

        [Test]
        public void Single_Tree_Multiple_Edge()
        {
            var td = new TestData();
            var root = td.GetNode();
            td.Create(5, i => new Edge(root, td.GetNode()));

            Assert.AreEqual(1, Process(td, td.GetMaxNode()));
        }

        [Test]
        public void Single_Edge()
        {
            Assert.AreEqual(1, Process(new List<Edge>
            {
                new Edge(0, 1)
            }, 2));
        }

        [Test]
        public void OneNormalForestWithDifferentSizeTrees()
        {
            var td = new TestData();
            var random = new Random();

            for (int i = 0; i < 1 * 1000 * 1000; i++)
            {
                var treeSize = random.Next(100);
                var root = td.GetNode();

                td.Create(treeSize, x => new Edge(root, td.GetNode()));
            }

            td.RandomizeEdges();

            Assert.AreEqual(1 * 1000 * 1000, Process(td, td.GetMaxNode()));
        }
    }
}