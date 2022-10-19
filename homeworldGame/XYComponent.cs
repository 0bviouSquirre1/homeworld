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

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}