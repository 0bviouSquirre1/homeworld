namespace homeworld
{
    public class Mobility : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public States State     { get; set; }
        public enum States
        {
            Immovable   = 0,
            Portable    = 1,
            Movable     = 2
        }

        public Mobility(States state, int entity_id = 0)
        {
            EntityID    = entity_id;
            ComponentID = IComponent.NextComponentID();

            // Component-specific setup
            State       = state;
        }

        // METHODS

        public override string ToString()
        {
            return $"Mobility: {State}";
        }
    }
}