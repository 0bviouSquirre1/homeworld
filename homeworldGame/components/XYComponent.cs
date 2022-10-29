using System.Diagnostics.CodeAnalysis;

namespace homeworld
{
    public struct XYComponent : IComponent
    {
        public int xValue { get; }
        public int yValue { get; }
        public int ComponentID { get; set; }

        public XYComponent(int x = 0, int y = 0)
        {
            xValue = x;
            yValue = y;
            ComponentID = IComponent.NextComponentID();
        }

        // METHODS

        public override string ToString()
        {
            return $"({xValue},{yValue})";
        }
        public override bool Equals([NotNullWhen(true)] object? xycomponent)
        {
            XYComponent component = (XYComponent)xycomponent!;
            if (xValue == component.xValue && yValue == component.yValue)
                return true;
            return false;
        }
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
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}