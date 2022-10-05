namespace homeworld {
    public class Plant : Entity {
        [Flags]
        public enum Status {
            None = 0,
            Harvestable = 1
        }

        public Status PlantStatus { get; set; }
        public Item HarvestItem = new Item("a mandrake root", "a gnarled root that vaguely resembles a human shape");

        public Plant(string name = "a mandrake plant", string description = "a menacing rosette of tough, dark leaves spreads out from a center of showy purple flowers") {
            Name = name;
            Description = description;
            // Grow();
        }

        // METHODS

        public void Grow() {
            Inventory.Add(HarvestItem);
            PlantStatus = Status.Harvestable;
        }

        public Item Harvest() {
            if (Inventory.Count > 0) {
                if (Inventory.Remove(HarvestItem)) {
                    PlantStatus ^= Status.Harvestable;
                    return HarvestItem;
                } else {
                    Console.WriteLine("Shouldn't have come here!");
                    return null;
                }
            } else {
                Console.WriteLine("There is nothing there to harvest.");
                return null;
            }
        }
    }
}