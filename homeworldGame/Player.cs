using System.Collections.Concurrent;

namespace homeworld
{
    public class Player : Entity
    {

        public Player (string name = "jod", XY location = new XY())
        {
            Name = name;
            Location = location;
            Description = "a strapping young adventurer bound for glory or a hole";
        }

        // METHODS

        public void Display()
        {
            Console.WriteLine($"{Name} is at {Location}.");
        }

        new public void Move(XY targetLocation)
        {
            if (Room.Exists(targetLocation))
            {
                // First do math to ensure that the location is ok
                XY result = new XY((targetLocation.X - Location.X), (targetLocation.Y - Location.Y));

                if (targetLocation.Equals(Location))
                {
                    Console.WriteLine($"You're already there!");
                    return;
                } else 
                {
                    // Check that the X and Y are between -1 and 1
                    Location = targetLocation;
                }
            }
        }
        public void Move(string direction)
        {
            XY coords = XY.CheckDirection(direction);
            XY newLocation = XY.Translate(Location, coords);
            if(Room.Exists(newLocation))
            {
                Move(newLocation);
            } else
            {
                Console.WriteLine($"You cannot move in that direction.");
            }
        }
    }
}