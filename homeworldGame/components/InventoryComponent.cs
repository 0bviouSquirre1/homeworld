namespace homeworld
{
    public class Inventory : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public List<Entity> InventoryList = new List<Entity>();
        // TODO: set optional max inventory size

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