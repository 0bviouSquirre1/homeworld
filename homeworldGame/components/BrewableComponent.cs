namespace homeworld
{
    public class Brewable : IComponent
    {
        public int PlantID     { get; set; }
        public int ComponentID  { get; set; }
        public Brewable()
        {
            ComponentID = IComponent.NextComponentID();
        }
    }
}