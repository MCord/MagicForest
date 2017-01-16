namespace Solver
{
    public struct Edge
    {
        public Edge(int from, int to)
        {
            From = from;
            To = to;
        }

        public int From;

        public int To;

        public override string ToString()
        {
            return $"{From} => {To}";
        }
    }
}