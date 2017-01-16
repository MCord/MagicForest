namespace Solver
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class TestData : IEnumerable<Edge>
    {
        private int counter = -1;
        private readonly List<Edge> edges = new List<Edge>();
        public int GetNode()
        {
            counter++;
            return counter;
        }

        public void AddNodesFrom(int node, int count)
        {
            for (int i = 0; i < count; i++)
            {
                edges.Add(new Edge(node, GetNode()));
            }
        }

        public int GetMaxNode()
        {
            return counter + 1;
        }

        public IEnumerator<Edge> GetEnumerator()
        {
            return edges.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Create(int times, Func<object, Edge> func)
        {
            for (int i = 0; i < times; i++)
            {
                edges.Add(func(i));
            }
        }

        public void CreateBranch(int length)
        {
            var last = GetNode();

            for (int i = 0; i < length; i++)
            {
                edges.Add(new Edge(last, (last = GetNode())));
            }
        }

        public void RandomizeEdges()
        {
            edges.Shuffle();
        }
    }
}