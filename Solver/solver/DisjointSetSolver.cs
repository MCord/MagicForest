namespace Solver
{
    using System.Collections.Generic;

    class DisjointSetSolver
    {
        public static int Solve(IEnumerable<Edge> edges, int nodes)
        {
            /* I use a hashset to keep track of patitions that are known to contain a loop. 
             * There should be a better way to handle this by having a reserved partition for all 
             * components with a loop.
             */
            var loops = new HashSet<int>();

            /* All nodes are root node at the begining so they contain -1 (root element of size 1)*/
            var lookup = new int[nodes];
            for (int i = 0; i < nodes; i++)
            {
                lookup[i] = -1;
            }

            /* Process one edge at a time. Ordering edges can improve data locality and further improve performance.
               Here we don't do any sorting.
             */
            foreach (var e in edges)
            {
                var toRoot = lookup[e.To];
                var fromRoot = lookup[e.From];

                if (toRoot == -1)
                {
                    if (fromRoot == -1)
                    {
                        /* If we are in this case it means both ends of this edge are single node partitions that should be joined.*/
                        MakeSet(lookup, e);
                        continue;
                    }

                    /* To is a single element root. If from is a root merge them both. if from is not a root merge whatever root it points to.*/
                    Add(lookup, e.To, fromRoot >= 0 ? fromRoot : e.From);

                    continue;
                }

                if (fromRoot == -1)
                {
                    /* From is a single element root. If to is a root merge them both. if to is not a root merge whatever root it points to.*/
                    Add(lookup, e.From, toRoot >= 0 ? toRoot : e.To);
                    continue;
                }

                if (fromRoot == toRoot)
                {
                    /* If both ends of the edge are in the same partition then we have a loop */
                    loops.Add(fromRoot);
                    continue;
                }

                /* Merge two multi edge partitions */
                MergeRoots(lookup, fromRoot > 0 ? fromRoot : e.From, toRoot > 0 ? toRoot : e.To);
            }

            /* Count the number of distinct partitions by counting roots.*/
            var count = 0;
            for (int i = 0; i < lookup.Length; i++)
            {
                if (lookup[i] < 0)
                {
                    count++;
                }
            }

            /* Since componenets with a loop are not valid remove their count. */
            return count - loops.Count;
        }

        /// <summary>
        /// Merge two partitions by their root identifiers
        /// </summary>
        /// <param name="lookup"></param>
        /// <param name="fromRoot"></param>
        /// <param name="toRoot"></param>
        private static void MergeRoots(int[] lookup, int fromRoot, int toRoot)
        {
            var fromSize = lookup[fromRoot];
            var toSize = lookup[toRoot];

            /* Make sure smaller partition is coppied into the larger on. */
            if (fromSize > toSize)
            {
                MergeInto(lookup, fromRoot, toRoot, fromSize);
            }
            else
            {
                MergeInto(lookup, toRoot, fromRoot, toSize);
            }
        }

        /// <summary>
        /// Assigns a new root to all elements of the partition to be merged.
        /// </summary>
        /// <param name="lookup">The array that contains the set data</param>
        /// <param name="sourceRoot">the partition root to be modified</param>
        /// <param name="destinationRoot">partitions's root to move all elements too</param>
        /// <param name="sourceSize">Size of the partition to be merged</param>
        private static void MergeInto(int[] lookup, int sourceRoot, int destinationRoot, int sourceSize)
        {
            var togo = sourceSize;
            /* Point all elements in partition to the new partition's root*/
            for (int i = 0; i < lookup.Length; i++)
            {
                if (lookup[i] == sourceRoot)
                {
                    lookup[i] = destinationRoot;
                    togo--;
                    /* Don't continue if there is no more element to be updated.*/
                    if (togo == 0)
                    {
                        break;
                    }
                }
            }

            /* increase the destination size*/
            lookup[destinationRoot] += sourceSize;
            /* source root should point to destination root.*/
            lookup[sourceRoot] = destinationRoot;
        }

        /// <summary>
        /// Add element to a partition.
        /// </summary>
        /// <param name="lookup">data structure</param>
        /// <param name="toAdd"> element to add. </param>
        /// <param name="root">partition's root</param>
        private static void Add(int[] lookup, int toAdd, int root)
        {
            lookup[toAdd] = root;
            /* Note that the parition size is represented in negative numbers.*/
            lookup[root]--; 
        }
        /// <summary>
        /// Handle Special case for merging two single element partitions/
        /// </summary>
        /// <param name="lookup"></param>
        /// <param name="edge"></param>
        private static void MakeSet(int[] lookup, Edge edge)
        {
            lookup[edge.To] = edge.From;
            /* Because the size is always two*/
            lookup[edge.From] = -2;
        }
    }
}