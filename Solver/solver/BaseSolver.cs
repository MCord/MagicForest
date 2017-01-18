namespace Solver
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BaseSolver
    {
        public int[] CreateLookupTable(int nodes)
        {
            /* All nodes are root node at the begining so they contain -1 (root element of size 1)
               The last index inside array is used as a special partition for all subtrees with loops in them.
             */

            var lookup = new int[nodes + 1];
            for (var i = 0; i < nodes + 1; i++)
            {
                lookup[i] = -1;
            }
            return lookup;
        }
        public int Solve(IEnumerable<Edge> edges, int nodes)
        {
            var lookup = CreateLookupTable(nodes);

            /* Process one edge at a time. Ordering edges can improve data locality and further improve performance.
               Here we don't do any sorting.
             */

            foreach (var e in edges)
            {
                Union(lookup, e.From, e.To);
            }

            /* Count the number of distinct partitions by counting roots.
               minus 1 is to account for the special  partition.*/
            return lookup.Count(t => t < 0) - 1;
        }
        public abstract void Union(int[] lookup, int @from, int to);
    }
}