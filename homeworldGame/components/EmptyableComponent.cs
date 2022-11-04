namespace homeworld
{
    public class Emptyable : IComponent
    {
        public int PlantID     { get; set; }
        public int ComponentID  { get; set; }
        public Emptyable()
        {
            ComponentID = IComponent.NextComponentID();
        }
    }
}