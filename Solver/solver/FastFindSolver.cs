namespace Solver
{
    public class FastFindSolver : BaseSolver
    {
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
        public override void Union(int[] lookup, int fromNode, int toNode)
        {
            int fromSize, toSize;

            var fromRoot = Find(lookup, fromNode, out fromSize);
            var toRoot = Find(lookup, toNode, out toSize);

            var fullSize = fromSize + toSize;

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
                lookup[fromRoot] = fullSize;
                lookup[toRoot] = fromRoot;
                if (toSize < -1)
                {
                    UpdateAllPointers(lookup, fromRoot, toRoot, fullSize);
                }

                return;
            }

            lookup[toRoot] = fullSize;
            lookup[fromRoot] = toRoot;

            if (fromSize < -1)
            {
                UpdateAllPointers(lookup, toRoot, fromRoot, fullSize);
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