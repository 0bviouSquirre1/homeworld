namespace homeworld
{
    public struct XY : IComponent
    {
        public int X { get; }
        public int Y { get; }
        public XY(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        // METHODS

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}