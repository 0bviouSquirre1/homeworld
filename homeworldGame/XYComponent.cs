using System.Diagnostics.Tracing;
namespace homeworld
{
    public struct XYComponent //: IComponent
    {
        public int X { get; }
        public int Y { get; }
        public XYComponent(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        // METHODS

        public static XYComponent RandomLocation()
        {
            var random = new Random();
            int x = random.Next(-5,6);
            int y = random.Next(-5,6);
            if (x == 1 && y == 1)
            {
                XYComponent reroll = RandomLocation();
                return reroll;
            }
            return new XYComponent(x,y);
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}