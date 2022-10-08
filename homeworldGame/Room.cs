using System.Collections.Concurrent;

namespace homeworld
{
    public class Room : IExists
    {
        private static ConcurrentDictionary<XY, Room> _map = new ConcurrentDictionary<XY, Room>();

        public string Description { get; set; }
        public List<Item> Inventory { get; }
        public XY Location { get; set; }
        public static ConcurrentDictionary<XY, Room> Map
        { 
            get => _map;
            set => Map = _map;
        }
        
        public string Name { get; set; }
        /*public Room(XY location = new XY())
        {
            Location = location;
            Name = "(blank)";
            Description = "";
            Inventory = new List<Item>();
            Map.GetOrAdd(Location, this);
        }*/

        // METHODS

        public void AddToInventory(Item item)
        {
            Inventory.Add(item);
        }

        public void AddToInventory(List<Item> itemList)
        {
            foreach (Item item in itemList) {
                Inventory.Add(item);
            }
        }

        public void Display()
        {
            Console.WriteLine($":: {Name} @ {Location}");
            Console.WriteLine($"{Description}");
            Console.WriteLine($"--------------------");
            foreach (Item item in Inventory)
            {
                Console.WriteLine($"{item}");
            }
        }

        public static void DisplayMap()
        {
            foreach (KeyValuePair<XY, Room> entry in Map)
            {
                Console.WriteLine($"{entry.Key} : {entry.Value.Name}");
            }
        }
        
        public static bool Exists(XY coords)
        {
            if (Room.Map.ContainsKey(coords))
            {
                return true;
            }
            return false;
        }

        /*public static Room GetRoom(XY location)
        {
            Room thisRoom = Room.Map.GetOrAdd(location, new Room(location));
            return thisRoom;
        }*/
    }
}