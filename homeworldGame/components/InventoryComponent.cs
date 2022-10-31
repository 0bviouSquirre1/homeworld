namespace homeworld
{
    public class Inventory : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public List<int> InventoryList = new List<int>();

        public Inventory()
        {
            ComponentID = IComponent.NextComponentID();
        }

        // METHODS

        public override string ToString()
        {
            return "Inventory";
        }
    }
}