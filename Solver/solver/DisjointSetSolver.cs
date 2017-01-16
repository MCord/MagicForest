namespace Solver
{
    using System.Collections.Generic;

    class DisjointSetSolver
    {
        public static int Solve(IEnumerable<Edge> edges, int nodes)
        {
            var loops = new HashSet<int>();

            var lookup = new int[nodes];
            for (int i = 0; i < nodes; i++)
            {
                lookup[i] = -1;
            }

            foreach (var e in edges)
            {
                var toRoot = lookup[e.To];
                var fromRoot = lookup[e.From];

                if (toRoot == -1)
                {
                    if (fromRoot == -1)
                    {
                        MakeSet(lookup, e);
                        continue;
                    }

                    Add(lookup, e.To, fromRoot >= 0 ? fromRoot : e.From);

                    continue;
                }

                if (fromRoot == -1)
                {
                    Add(lookup, e.From, toRoot >= 0 ? toRoot : e.To);
                    continue;
                }

                if (fromRoot == toRoot)
                {
                    loops.Add(fromRoot);
                    continue;
                }

                MergeRoots(lookup, fromRoot, toRoot);
            }

            var count = 0;
            for (int i = 0; i < lookup.Length; i++)
            {
                if (lookup[i] < 0)
                {
                    count++;
                }
            }


            return count - loops.Count;
        }

        private static void MergeRoots(int[] lookup, int fromRoot, int toRoot)
        {
            var fromSize = lookup[fromRoot];
            var toSize = lookup[toRoot];

            if (fromSize > toSize)
            {
                MergeInto(lookup, fromRoot, toRoot, fromSize);
            }
            else
            {
                MergeInto(lookup, toRoot, fromRoot, toSize);
            }
        }

        private static void MergeInto(int[] lookup, int sourceRoot, int destinationRoot, int sourceSize)
        {
            var togo = sourceSize;
            for (int i = 0; i < lookup.Length; i++)
            {
                if (lookup[i] == sourceRoot)
                {
                    lookup[i] = destinationRoot;
                    togo--;
                    if (togo == 0)
                    {
                        return;
                    }
                }
            }
            lookup[destinationRoot] += sourceSize;
            lookup[sourceRoot] = destinationRoot;
        }

        private static void Add(int[] lookup, int toAdd, int root)
        {
            lookup[toAdd] = root;
            lookup[root]--;
        }
        private static void MakeSet(int[] lookup, Edge edge)
        {
            lookup[edge.To] = edge.From;
            lookup[edge.From] = -2;
        }
    }
}