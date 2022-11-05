namespace homeworld
{
    public class CapacityComponent : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public int Capacity     { get; set; }
        public int Contents     { get; set; }
        public CapacityComponent(int capacity)
        {
            ComponentID = IComponent.NextComponentID();
            Capacity = capacity;
            Contents = 0;
        }
    }
}