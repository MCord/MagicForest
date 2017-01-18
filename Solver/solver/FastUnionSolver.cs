namespace Solver
{
    using System.Runtime.CompilerServices;

    public class FastUnionSolver : BaseSolver
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SlowFindRoot(int[] lookup, int node)
        {
            var pathCompaction = int.MaxValue;


            while (true)
            {
                var sizeAndPointer = lookup[node];
                if (sizeAndPointer < 0)
                {
                    if (pathCompaction != int.MaxValue)
                    {
                        lookup[pathCompaction] = node;
                    }

                    return node;
                }


                // pathCompaction = node;
                node = sizeAndPointer;
            }
        }

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