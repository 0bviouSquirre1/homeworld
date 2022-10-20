namespace homeworld
{
    public class Plant
    {
        public string Name;
        public XYComponent Location;
        public Dictionary<Item, int> Inventory;
        public static Dictionary<XYComponent, List<Plant>> AllPlantsInWorld = new Dictionary<XYComponent, List<Plant>>();

        public Plant(string name, XYComponent location, Item produce)
        {
            Name = name;
            Location = location;
            Inventory = new Dictionary<Item, int>();
            AddToWorld(location, this);
            Inventory.Add(produce, 3);
        }

        // METHODS

        public static void AddToWorld(XYComponent location, Plant plant)
        {
            AllPlantsInWorld.TryGetValue(location, out List<Plant>? currentPlants);
            if (currentPlants is null)
            {
                AllPlantsInWorld.TryAdd(location, new List<Plant>() { plant });
            }
            else
            {
                currentPlants.Add(plant);
            }
        }

        public static void RandomPlants(string name, Item produce)
        {
            for (int i = 0; i <= 4; i++)
            {
                XYComponent location = XYComponent.RandomLocation();
                new Plant(name, location, produce);
            }
        }
    }
}