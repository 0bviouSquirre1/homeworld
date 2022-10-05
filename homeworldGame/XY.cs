using System.Collections.Concurrent;

namespace homeworld {
    public struct XY {
        public int X { get; }
        public int Y { get; }
        public XY(int x = 0, int y = 0) {
            X = x;
            Y = y;
        }

        // METHODS

        public static XY CheckDirection(string direction) {
            Console.WriteLine($"XY.CheckDirection received {direction}");
            XY coords;
            switch (direction) {
                case "north":
                        coords = new XY(0,1);
                        Console.WriteLine($"checkdirection method returns {coords} for {direction}");
                        return coords;
                    case "east":
                        coords = new XY(1,0);
                        Console.WriteLine($"checkdirection method returns {coords} for {direction}");
                        return coords;
                    case "south":
                        coords = new XY(0,-1);
                        Console.WriteLine($"checkdirection method returns {coords} for {direction}");
                        return coords;
                    case "west":
                        coords = new XY(-1,0);
                        Console.WriteLine($"checkdirection method returns {coords} for {direction}");
                        return coords;
                    default:
                        Console.WriteLine($"did you get here?");
                        return new XY();
            }
        }

        public static XY Compare(XY firstXY, XY secondXY) {
            int x = secondXY.X - firstXY.X;
            int y = secondXY.Y - firstXY.Y;
            XY result = new XY(x,y);
            return result;
        }

        public override string ToString() {
            return $"({X},{Y})";
        }

        public static XY Translate(XY myLocation, XY translationVector) {
            XY result = new XY((myLocation.X + translationVector.X), (myLocation.Y + translationVector.Y));
            Console.WriteLine($"my location: {myLocation.ToString()}, translation vector: {translationVector.ToString()}, new location: {result}");
            return result;
        }
    }
}