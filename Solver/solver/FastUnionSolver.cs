namespace Solver
{
    public class FastUnionSolver : BaseSolver
    {
        /// <summary>
        /// Finds the root of a partition give an element in that partition.
        /// </summary>
        /// <param name="lookup">The array containing the disjoint sets.</param>
        /// <param name="node">The element to lookup.</param>
        /// <returns>The identity of the partition the node belongs to</returns>
        /// <remarks>
        /// Since this is fast union implementation, the find operation is slow since it has to 
        /// go all the way up to find the root. The path compaction technic is used to 
        /// optimize lookup time for repeated lookups.
        /// </remarks>
        public static int SlowFindRoot(int[] lookup, int node)
        {
            var pathCompaction = int.MaxValue;

            while (true)
            {
                var sizeAndPointer = lookup[node];
                if (sizeAndPointer < 0) /*It's a root*/
                {
                    if (pathCompaction != int.MaxValue)
                    {
                        lookup[pathCompaction] = node;
                    }

                    return node;
                }


                pathCompaction = node;
                node = sizeAndPointer;
            }
        }
        /// <summary>
        /// Merges two partitions into one
        /// </summary>
        /// <param name="lookup">The array containing the data structure.</param>
        /// <param name="fromNode">A node in the first partition.</param>
        /// <param name="toNode">A node in the second partition.</param>
        /// <remarks>
        /// Since this is a fast union implementation, The merge operation just points the curent node to
        /// the other node.To make sure the two nodes are not already in the same partition (and thus form a loop) 
        /// we have to find their roots
        /// </remarks>
        public override void Union(int[] lookup, int fromNode, int toNode)
        {
            var fromRoot = SlowFindRoot(lookup, fromNode);
            var toRoot = SlowFindRoot(lookup, toNode);

            if (fromRoot == toRoot)
            {
                lookup[fromRoot] = lookup.Length - 1;
                return;
            }

            lookup[toNode] = fromNode;
        }
    }
}