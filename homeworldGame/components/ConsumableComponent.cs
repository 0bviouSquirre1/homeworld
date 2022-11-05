namespace homeworld
{
    public class Consumable : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public States State     { get; set; }
        public enum States
        {
            None,
            Deadly,
            Edible,
            Potable,
            Applicable
        }
        public Consumable()
        {
            ComponentID = IComponent.NextComponentID();
        }
        public Consumable(States state)
        {
            ComponentID = IComponent.NextComponentID();

            // Component-specific setup
            State       = state;
        }

        // METHODS

        public override string ToString()
        {
            return $"Consumability: {State}";
        }
    }
}