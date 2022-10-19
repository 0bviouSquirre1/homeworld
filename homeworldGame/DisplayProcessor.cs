namespace homeworld
{
    public static class DisplayProcessor {
        public static void DisplayInventory(Player player)
        {
            Console.WriteLine();
            Console.WriteLine($"Inventory of player {player.Name} contains:");
            foreach (KeyValuePair<Item, int> itemNode in player.Inventory)
            {
                Console.WriteLine($"({itemNode.Value}) {itemNode.Key.Name}");
            }
        }
        public static void DisplayInventory(Plant plant)
        {
            Console.WriteLine();
            Console.WriteLine($"Inventory of {plant.Name} contains:");
            foreach (KeyValuePair<Item, int> itemNode in plant.Inventory)
            {
                Console.WriteLine($"({itemNode.Value}) {itemNode.Key.Name}");
            }
        }
        public static void DisplayLocation(XYComponent location)
        {
            DisplayAllPlantsInLocation(location);
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
        public static void DisplayAllPlantsInWorld()
        {
            int count = 0;
            Console.WriteLine();
            Console.WriteLine($"Currently in the world there are:");
            foreach (KeyValuePair<XYComponent, List<Plant>> plantNode in Plant.AllPlantsInWorld)
            {
                foreach (Plant plant in plantNode.Value)
                {
                    Console.WriteLine($"{plant.Name}     {plantNode.Key}");
                    count++;
                }
            }
            Console.WriteLine($"{count} plants in the world.");
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
                        count++;
                    }
                }
            }
            Console.WriteLine($"{count} items in this location.");
        }
        public static void DisplayAllPlantsInLocation(XYComponent location)
        {
            Dictionary<XYComponent, List<Plant>> currentPlantsInLocation = Plant.AllPlantsInWorld;
            int count = 0;
            Console.WriteLine();
            Console.WriteLine($"Currently in this location there are:");
            foreach (KeyValuePair<XYComponent, List<Plant>> plantNode in currentPlantsInLocation)
            {
                if (plantNode.Key.Equals(location))
                {
                    foreach (Plant plant in plantNode.Value)
                    {
                        Console.WriteLine($"{plant.Name}     {plantNode.Key}");
                        count++;
                    }
                }
            }
            Console.WriteLine($"{count} plants in this location.");
        }
    }
}