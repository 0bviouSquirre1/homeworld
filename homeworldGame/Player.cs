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

        public void Light(Container container)
        {
            if (!container.isLit)
            {
                container.isLit = true;
                Console.WriteLine();
                Console.WriteLine($"{Name} lights {container.Name}");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"{container.Name} is already lit!");
            }
        }
        public void Extinguish(Container container)
        {
            if (container.isLit)
            {
                container.isLit = false;
                Console.WriteLine($"{Name} extinguishes {container.Name}.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"{container.Name} is already out!");
            }
        }
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
        public void Fill(Container container, Container source)
        {
            if (container.isFull)
            {
                Console.WriteLine();
                Console.WriteLine($"The {container.Name} is already full!");
            }
            else
            {
                container.isFull = true;
                Console.WriteLine();
                Console.WriteLine($"{Name} filled {container.Name} from {source.Name}.");
            }
        }
        public void Empty(Container container, Container? receptacle = null)
        {
            if (receptacle is null)
            {
                if (container.isFull)
                {
                    container.isFull = false;
                    Console.WriteLine();
                    Console.WriteLine($"{Name} empties a {container.Name} here.");
                }
            }
            else
            {
                if (container.isFull && !receptacle.isFull)
                {
                    container.isFull = false;
                    receptacle.isFull = true;
                    Console.WriteLine();
                    Console.WriteLine($"{Name} fills a {receptacle.Name} from a {container.Name}.");
                }
            }
        }
        public Item PrepareTea(Container container, Item produce)
        {
            if (!container.isFull || !container.isLit)
            {
                Console.WriteLine();
                Console.WriteLine($"{container.Name} is not ready to make tea.");
                return null;
            }
            else
            {
                if (Inventory.ContainsKey(produce))
                {
                    Inventory[produce]--;
                    if (Inventory[produce] == 0)
                    {
                        Inventory.Remove(produce);
                    }
                    Console.WriteLine();
                    Console.WriteLine($"{Name} begins to make tea in {container.Name} out of {produce.Name}.");

                    Item tea = new Item($"{produce.Name} tea");
                    if (Inventory.ContainsKey(tea))
                    {
                        Inventory[tea]++;
                    }
                    else
                    {
                        Inventory.Add(tea, 1);
                    }

                    Console.WriteLine();
                    Console.WriteLine($"{Name} has made a nice cup of {tea.Name}.");
                    return tea;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"{Name} does not have any {produce.Name} on hand!");
                    return null;
                }
            }
        }
        public void Drink(Item tea)
        {
            if (Inventory.ContainsKey(tea))
            {
                Inventory[tea]--;
                if (Inventory[tea] == 0)
                {
                    Inventory.Remove(tea);
                }
                Console.WriteLine();
                Console.WriteLine($"{Name} sips a nice cup of {tea.Name}.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"{Name} does not have any {tea.Name} on hand!");
            }
        }
    }
}