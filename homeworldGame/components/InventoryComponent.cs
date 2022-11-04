namespace homeworld
{
    public class Inventory : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public List<Entity> InventoryList;
        // TODO: set optional max inventory size

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