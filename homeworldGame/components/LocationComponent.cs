namespace homeworld
{
    public class Location : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public XY Coordinates   { get; set; }
        public Location()
        {
            ComponentID = IComponent.NextComponentID();
        }
        public Location(XY coords)
        {
            ComponentID = IComponent.NextComponentID();

            // Component-specific setup
            Coordinates = coords;
        }
    }
}