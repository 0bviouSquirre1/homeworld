namespace homeworld
{
    public class Consumable : IComponent
    {
        public enum States : int
        {
            Deadly = 0,
            Edible = 1,
            Potable = 2,
            Applicable = 3
        }
        public States State { get; set; }
        public int ComponentID { get; set; }
        public Consumable(States state)
        {
            State = state;
            ComponentID = IComponent.NextComponentID();
        }

        // METHODS

        public override string ToString()
        {
            return $"Consumability: {State}";
        }
    }
}