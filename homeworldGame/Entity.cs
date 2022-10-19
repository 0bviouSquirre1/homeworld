using System.Xml.Serialization;
namespace homeworld {
    public class Entity
    {
        string Name;
        XYComponent Location;

        public Entity(string name, XYComponent location = new XYComponent())
        {
            Name = name;
            Location = location;
        }

        // METHODS

        public void Move(string direction)
        {
            XYComponent oldLocation = Location;
            XYComponent neoLocation = new XYComponent();

            switch (direction)
            {
                case "north":
                    neoLocation = new XYComponent(Location.X, Location.Y + 1);
                    break;
                case "south":
                    neoLocation = new XYComponent(Location.X, Location.Y - 1);
                    break;
                case "east":
                    neoLocation = new XYComponent(Location.X + 1, Location.Y);
                    break;
                case "west":
                    neoLocation = new XYComponent(Location.X - 1, Location.Y);
                    break;
                default:
                    Console.WriteLine($"Move() switch case default activated.");
                    return;
            }
            Location = neoLocation;

            // Display code
            Console.WriteLine($"{Name} moves {direction} from {oldLocation} to {neoLocation}.");
        }
    }
}