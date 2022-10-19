namespace homeworld {
    public class Player
    {
        public string Name;
        public XYComponent Location;
        public Dictionary<Item, int> Inventory;
        public Player(string name, XYComponent location)
        {
            Name = name;
            Location = location;
            Inventory = new Dictionary<Item, int>();
        }

        // METHODS

        public void Gather(Plant plant, Item produce)
        {
            if (plant.Inventory.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine($"There is nothing to harvest on this {plant.Name}!");
            }
            else
            {
                plant.Inventory[produce]--;
                if (plant.Inventory[produce] == 0)
                {
                    plant.Inventory.Remove(produce);
                }

                if (Inventory.ContainsKey(produce))
                {
                    Inventory[produce]++;
                }
                else
                {
                    Inventory.Add(produce, 1);
                }
                Console.WriteLine();
                Console.WriteLine($"{Name} harvests {produce.Name} from {plant.Name}.");
            }
        }
        public void Get(Item item)
        {
            if(Item.AllItemsInWorld.TryGetValue(Location, out List<Item>? AllItemsInLocation) && AllItemsInLocation.Contains(item))
            {
                AllItemsInLocation.Remove(item);
                if (Inventory.ContainsKey(item))
                {
                    Inventory[item]++;
                }
                else
                {
                    Inventory.Add(item, 1);
                }
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
            if (Inventory.ContainsKey(item))
            {
                Inventory.Remove(item);
                Item.AddToWorld(Location, item);
                Console.WriteLine();
                Console.WriteLine($"{Name} drops a {item.Name} here.");
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