namespace homeworld
{
    public class BrewCapable : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public BrewCapable()
        {
            ComponentID = IComponent.NextComponentID();
        }
    }
}