namespace homeworld
{
    public class XY : IComponent
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

/*
        public static XY CheckDirection(string direction)
        {
            XY coords;
            switch (direction)
            {
                case "north":
                    coords = new XY(0,1);
                    return coords;
                case "east":
                    coords = new XY(1,0);
                    return coords;
                case "south":
                    coords = new XY(0,-1);
                    return coords;
                case "west":
                    coords = new XY(-1,0);
                    return coords;
                default:
                    return new XY();
            }
        }

        public static XY Compare(XY firstXY, XY secondXY)
        {
            int x = secondXY.X - firstXY.X;
            int y = secondXY.Y - firstXY.Y;
            XY result = new XY(x,y);
            return result;
        }

        public static XY Translate(XY myLocation, XY translationVector)
        {
            XY result = new XY((myLocation.X + translationVector.X), (myLocation.Y + translationVector.Y));
            return result;
        }
*/
    }
}