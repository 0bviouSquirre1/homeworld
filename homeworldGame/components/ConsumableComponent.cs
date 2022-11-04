namespace homeworld
{
    public class Consumable : IComponent
    {
        public int PlantID     { get; set; }
        public int ComponentID  { get; set; }
        public States State     { get; set; }
        public enum States
        {
            Deadly      = 0,
            Edible      = 1,
            Potable     = 2,
            Applicable  = 3
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