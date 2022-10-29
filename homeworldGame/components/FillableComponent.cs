namespace homeworld
{
    public class Fillable : IComponent
    {
        public int ComponentID { get; set; }
        public Fillable()
        {
            ComponentID = IComponent.NextComponentID();
        }
    }
}