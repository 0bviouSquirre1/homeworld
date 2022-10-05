using System.Collections.Concurrent;

namespace homeworld
{
    public class Item : IExists
    {
        [Flags]
        public enum Status
        {
            None = 0,
            Equipped = 1
        }
        public string Name { get; set; }
        public XY Location { get; set; }
        public string Description { get; set; }
        public List<Item> Inventory { get; }
        public Status ItemStatus { get; set; } 

        public Item(string name = "a journal", string description = "a tattered old journal")
        {
            Name = name;
            Description = description;
            Inventory = new List<Item>();
        }

        // METHODS

        public static void Move() { }

        public override string ToString()
        {
            return Name;
        }
    }
}