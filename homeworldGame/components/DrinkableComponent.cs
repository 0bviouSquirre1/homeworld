namespace homeworld
{
    public class Drinkable : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public Drinkable()
        {
            ComponentID = IComponent.NextComponentID();
        }
    }
}