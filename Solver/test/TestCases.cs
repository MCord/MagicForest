//namespace Solver
//{
//    using System;
//    using System.Collections.Generic;
//    using NUnit.Framework;

//    public class TestCases
//    {
//        private EdgeGen randomTree;
//        private List<Edge> smallList;
//        private EdgeGen oneBigTree;
//        private EdgeGen oneDeepTree;
//        private EdgeGen grassForest;
//        private EdgeGen branchOf5;
//        private EdgeGen noEdgeGraph;
//        private List<Edge> singleEdge;
//        private EdgeGen forest;

//        [TestFixtureSetUp]
//        public void Setup()
//        {
//            singleEdge = new List<Edge>
//            {
//                new Edge(0, 1)
//            };

//            noEdgeGraph = new EdgeGen();
//            noEdgeGraph.GetNode();
//            noEdgeGraph.GetNode();
//            noEdgeGraph.GetNode();

//            randomTree = new EdgeGen();
//            for (int i = 0; i < 600 * 1000; i += 1)
//            {
//                var node = randomTree.GetNode();
//                randomTree.AddNodesFrom(node, 10);
//            }

//            smallList = new List<Edge>(100)
//            {
//                new Edge(0, 1),
//                new Edge(0, 2),
//                new Edge(0, 3),
//                new Edge(0, 10),

//                new Edge(8, 9),

//                new Edge(4, 5),
//                new Edge(4, 6),
//                new Edge(4, 7),
//            };

//            oneBigTree = new EdgeGen();
//            var root = oneBigTree.GetNode();

//            oneBigTree.Create(6 * 1000 * 1000, i => new Edge(root, oneBigTree.GetNode()));

//            oneDeepTree = new EdgeGen();
//            oneDeepTree.CreateBranch(6 * 1000 * 1000);

//            grassForest = new EdgeGen();
//            grassForest.Create(10 * 1000 * 1000, i => new Edge(grassForest.GetNode(), grassForest.GetNode()));

//            branchOf5 = new EdgeGen();
//            branchOf5.CreateBranch(5);

//            forest = new EdgeGen();
//            var random = new Random();

//            for (int i = 0; i < 1 * 1000 * 1000; i++)
//            {
//                var treeSize = random.Next(10);
//                var r = forest.GetNode();

//                forest.Create(treeSize, x => new Edge(r, forest.GetNode()));
//            }

//            forest.RandomizeEdges();

//        }

//        [TestCase(FastFindSolver.Mode.FastUnion)]
//        [TestCase(FastFindSolver.Mode.FastFind)]
//        [Repeat(5)]
//        public void Huge_Randomly_Ordered_Tree(FastFindSolver.Mode mode)
//        {
//            Assert.AreEqual(600 * 1000, Process(randomTree, randomTree.GetMaxNode(), mode));
//        }

//        [TestCase(FastFindSolver.Mode.FastUnion)]
//        [TestCase(FastFindSolver.Mode.FastFind)]
//        [Repeat(5)]
//        public void Experiment(FastFindSolver.Mode mode)
//        {
//            Assert.AreEqual(3, Process(smallList, 11, mode));
//        }

//        [TestCase(FastFindSolver.Mode.FastUnion)]
//        [TestCase(FastFindSolver.Mode.FastFind)]
//        [Repeat(5)]
//        public void OneHugeTree(FastFindSolver.Mode mode)
//        {
//            Assert.AreEqual(1, Process(oneBigTree, oneBigTree.GetMaxNode(),mode));
//        }

//        [TestCase(FastFindSolver.Mode.FastUnion)]
//        [TestCase(FastFindSolver.Mode.FastFind)]
//        [Repeat(5)]
//        public void Huge_Random_Edge_List_Deep_Tree(FastFindSolver.Mode mode)
//        {
//            Assert.AreEqual(1, Process(oneDeepTree, oneDeepTree.GetMaxNode(), mode));
//        }

//        [TestCase(FastFindSolver.Mode.FastUnion)]
//        [TestCase(FastFindSolver.Mode.FastFind)]
//        [Repeat(5)]
//        public void GrassForest(FastFindSolver.Mode mode)
//        {
//            Assert.AreEqual(10 * 1000 * 1000, Process(grassForest, grassForest.GetMaxNode(), mode));
//        }


//        private static int Process(IEnumerable<Edge> edges, int nodes, FastFindSolver.Mode mode)
//        {
//            return new FastFindSolver(mode).Solve(edges, nodes);
//        }

//        [TestCase(FastFindSolver.Mode.FastUnion)]
//        [TestCase(FastFindSolver.Mode.FastFind)]
//        [Repeat(5)]
//        public void OneBranch(FastFindSolver.Mode mode)
//        {
//            Assert.AreEqual(1, Process(branchOf5, branchOf5.GetMaxNode(), mode));
//        }

//        [TestCase(FastFindSolver.Mode.FastUnion)]
//        [TestCase(FastFindSolver.Mode.FastFind)]
//        [Repeat(5)]
//        public void GraphWithNoEdge(FastFindSolver.Mode mode)
//        {
//            Assert.AreEqual(3, Process(noEdgeGraph, noEdgeGraph.GetMaxNode(),mode));
//        }

//        [TestCase(FastFindSolver.Mode.FastUnion)]
//        [TestCase(FastFindSolver.Mode.FastFind)]
//        [Repeat(5)]
//        public void Single_Edge(FastFindSolver.Mode mode)
//        {
//            Assert.AreEqual(1, Process(singleEdge, 2, mode));
//        }

//        [TestCase(FastFindSolver.Mode.FastUnion)]
//        [TestCase(FastFindSolver.Mode.FastFind)]
//        [Repeat(5)]
//        public void OneNormalForestWithDifferentSizeTrees(FastFindSolver.Mode mode)
//        {
//            Assert.AreEqual(1 * 1000 * 1000, Process(forest, forest.GetMaxNode(), mode));
//        }
//    }
//}