namespace homeworld
{
    public class Fillable : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public Fillable()
        {
            ComponentID = IComponent.NextComponentID();
        }
    }
}