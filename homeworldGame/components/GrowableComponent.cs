namespace homeworld
{
    public class Growable : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public int ProduceID    { get; set; }
        public Growable(int produce_id)
        {
            ComponentID = IComponent.NextComponentID();

            // Component-specific setup
            ProduceID   = produce_id;
        }

        // TODO This will be moved to the GrowSystem
        public void Grow()
        {
            
        }
    }
}