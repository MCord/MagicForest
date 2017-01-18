namespace Solver
{
    public class FastFindSolver : BaseSolver
    {
        /// <summary>
        /// The find operation in disjoint-set finds what partition an element belongs to
        /// </summary>
        /// <param name="lookup">The array that contains the data structure.</param>
        /// <param name="node">The element to lookup</param>
        /// <param name="size">The size of the partition element belongs to</param>
        /// <returns>The identity of the partition the element belongs to.</returns>
        /// <remarks>Since this is a fast find implementation there are only two cases.
        /// either the element that we are searching for is a root itself in which case 
        /// it has a negative value as the size of partition or it points to another index 
        /// in the array in which case the parent is a root.
        /// </remarks>
        protected int Find(int[] lookup, int node, out int size)
        {
            var value = lookup[node];

            if (value < 0)
            {
                size = value;
                return node;
            }

            size = lookup[value];
            return value;
        }

        /// <summary>
        /// This method unions two partitions in a disjoint sets. Since it's a fast find implementation
        /// we find the root of two partitions and update every single element in the smaller partition
        /// to point to the root of the bigger partition.
        /// </summary>
        /// <param name="lookup">The data structure containing the disjoint sets.</param>
        /// <param name="fromNode">The node from first partition to join</param>
        /// <param name="toNode">The node from second partition to join</param>
        /// <remarks>
        /// from and to nods can not be the same node. If both from and to point to the same partition
        /// we have a loop in the partition and we add it to a special partition reserved for all partitions 
        /// with loops.
        /// </remarks>
        public override void Union(int[] lookup, int fromNode, int toNode)
        {
            int fromSize, toSize;

            var fromRoot = Find(lookup, fromNode, out fromSize);
            var toRoot = Find(lookup, toNode, out toSize);

            var newSize = fromSize + toSize;

            if (fromRoot == toRoot)
            {
                var loopPartition = lookup.Length - 1;
                UpdateAllPointers(lookup, loopPartition, fromRoot, fromSize);
                lookup[fromRoot] = loopPartition;
                lookup[loopPartition] += fromSize;
                return;
            }

            if (fromSize <= toSize)
            {
                lookup[fromRoot] = newSize;
                lookup[toRoot] = fromRoot;
                if (toSize < -1)
                {
                    UpdateAllPointers(lookup, fromRoot, toRoot, newSize);
                }

                return;
            }

            lookup[toRoot] = newSize;
            lookup[fromRoot] = toRoot;

            if (fromSize < -1)
            {
                UpdateAllPointers(lookup, toRoot, fromRoot, newSize);
            }
        }

        private static void UpdateAllPointers(int[] lookup, int newTarget, int oldTarget, int sizeInNegative)
        {
            for (var i = 0; sizeInNegative < 0 && i < lookup.Length; i++)
            {
                if (lookup[i] == oldTarget)
                {
                    sizeInNegative++;
                    lookup[i] = newTarget;
                }
            }
        }
    }
}