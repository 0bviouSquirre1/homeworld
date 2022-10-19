namespace homeworld
{
    public static class DisplayProcessor {
        public static void DisplayInventory(Player player) // TODO: collapse groups of like items
        {
            player.Inventory.Sort((x,y) => x.Name.CompareTo(y.Name));
            Console.WriteLine();
            Console.WriteLine($"Inventory of player {player.Name} contains:");
            foreach (Item item in player.Inventory)
            {
                Console.WriteLine($"{item.Name}");
            }
        }
        public static void DisplayLocation(XYComponent location)
        {
            DisplayAllItemsInLocation(location);
        }

        public static void DisplayAllItemsInWorld()
        {
            int count = 0;
            Console.WriteLine();
            Console.WriteLine($"Currently in the world there are:");
            foreach (KeyValuePair<XYComponent, List<Item>> itemNode in Item.AllItemsInWorld)
            {
                foreach (Item item in itemNode.Value)
                {
                    Console.WriteLine($"{item.Name}     {itemNode.Key}");
                    count++;
                }
            }
            Console.WriteLine($"{count} items in the world.");
        }

        public static void DisplayAllItemsInLocation(XYComponent location)
        {
            Dictionary<XYComponent, List<Item>> currentItemsInLocation = Item.AllItemsInWorld;
            int count = 0;
            Console.WriteLine();
            Console.WriteLine($"Currently in this location there are:");
            foreach (KeyValuePair<XYComponent, List<Item>> itemNode in currentItemsInLocation)
            {
                if (itemNode.Key.Equals(location))
                {
                    foreach (Item item in itemNode.Value)
                    {
                        Console.WriteLine($"{item.Name}     {itemNode.Key}");
                    }
                }
            }
            Console.WriteLine($"{count} items in this location.");
        }
    }
}