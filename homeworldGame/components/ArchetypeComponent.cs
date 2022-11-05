namespace homeworld
{
    public class Archetype : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public States State { get; set; }
        public enum States
        {
            None    = 0,
            Player  = 1,
            Produce = 2,
            Cup     = 3,
            Kettle  = 4,
            Bucket  = 5,
            Well    = 6,
            Tea     = 7,
            Plant   = 8,
            Item    = 9
        }
        public Archetype(States archetype)
        {
            ComponentID = IComponent.NextComponentID();

            // Component-specific setup
            State       = archetype;
        }
    }
}