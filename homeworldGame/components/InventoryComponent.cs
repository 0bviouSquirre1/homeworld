namespace homeworld
{
    public class Inventory : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public List<Entity> InventoryList;
        // TODO: actually use the MaxInventorySize, later
        public int MaxInventorySize = 20;

        public Inventory()
        {
            ComponentID = IComponent.NextComponentID();
            InventoryList = new List<Entity>();
        }

        // METHODS

        public override string ToString()
        {
            return "Inventory";
        }
    }
}