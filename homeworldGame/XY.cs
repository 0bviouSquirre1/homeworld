using System.Diagnostics.CodeAnalysis;

namespace homeworld
{
    public struct XY
    {
        public int xValue { get; }
        public int yValue { get; }

        public XY(int x = 99, int y = 99)
        {
            xValue = x;
            yValue = y;
        }

        // METHODS

        // Used for distributing entities randomly during setup
        // Rerolls on (1,1) so that nothing is assigned to the starting position
        public static XY RandomLocation()
        {
            var random  = new Random();
            int x       = random.Next(-5,6);
            int y       = random.Next(-5,6);
            
            if (x == 1 && y == 1)
            {
                XY reroll = RandomLocation();
                return reroll;
            }
            return new XY(x,y);
        }

#region OVERRIDES
        // OVERRIDES

        public override string ToString()
        {
            return $"({xValue},{yValue})";
        }
        public override bool Equals([NotNullWhen(true)] object? xycomponent)
        {
            XY component = (XY)xycomponent!;
            if (xValue == component.xValue && yValue == component.yValue)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
#endregion
    }
}