using System.Collections.Concurrent;

namespace homeworld {
    interface IExists {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Inventory { get; }
        public XY Location { get; set; }

        public static void Move() { }
    }
}