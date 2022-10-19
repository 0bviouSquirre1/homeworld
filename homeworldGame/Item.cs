namespace homeworld
{
    public class Item
    {
        public string Name;
        public int ItemID;
        public static Dictionary<XYComponent, List<Item>> AllItemsInWorld = new Dictionary<XYComponent, List<Item>>();
        public static int LastItemID = 0;

        public Item(string name)
        {
            Name = name;
            ItemID = GetNewItemID();
            
        }
        public Item(string name, XYComponent location = new XYComponent())
        {
            Name = name;
            ItemID = GetNewItemID();
            AddToWorld(location, this);
        }

        // METHODS

        public static void AddToWorld(XYComponent location, Item item)
        {
            AllItemsInWorld.TryGetValue(location, out List<Item>? currentItems);
            if (currentItems is null)
            {
                AllItemsInWorld.TryAdd(location, new List<Item>() { item });
            }
            else
            {
                currentItems.Add(item);
            }
        }

        public static int GetNewItemID()
        {
            LastItemID++;
            return LastItemID;
        }
    }
}