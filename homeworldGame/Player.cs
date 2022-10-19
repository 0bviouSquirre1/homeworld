namespace homeworld {
    public class Player
    {
        public string Name;
        public XYComponent Location;
        public List<Item> Inventory;

        public Player(string name, XYComponent location)
        {
            Name = name;
            Location = location;
            Inventory = new List<Item>();
        }

        // METHODS

        public void Get(Item item, XYComponent location)
        {
            if(Item.AllItemsInWorld.TryGetValue(location, out List<Item>? AllItemsInLocation) && AllItemsInLocation.Contains(item))
            {
                AllItemsInLocation.Remove(item);
                Inventory.Add(item);
                Console.WriteLine();
                Console.WriteLine($"{Name} took the {item.Name}.");
            }
            else
            {
                Console.WriteLine($"There is no {item.Name} here!");
            }
        }

        public void Drop(Item item)
        {
            if (Inventory.Contains(item))
            {
                Inventory.Remove(item);
                Item.AddToWorld(Location, item);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"You are not carrying any {item.Name}!");
            }
        }

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
            Console.WriteLine();
            Console.WriteLine($"{Name} moves {direction} from {oldLocation} to {neoLocation}.");
            DisplayProcessor.DisplayLocation(neoLocation);
        }
    }
}