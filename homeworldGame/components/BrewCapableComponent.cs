namespace homeworld
{
    public class BrewCapable : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        // need to store what kind of liquid is being held
        public BrewCapable()
        {
            ComponentID = IComponent.NextComponentID();
        }
    }
}