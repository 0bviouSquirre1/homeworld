namespace homeworld
{
    public class Template : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public Template()
        {
            ComponentID = IComponent.NextComponentID();
        }
    }
}