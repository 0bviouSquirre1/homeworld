namespace homeworld
{
    public class Mobility : IComponent
    {
        public enum States : int
        {
            Immovable = 0,
            Portable = 1,
            Movable = 2
        }
        public States State { get; set; }
        public int ComponentID { get; set; }

        public Mobility(States state)
        {
            ComponentID = IComponent.NextComponentID();
            State = state;
        }

        // METHODS

        public override string ToString()
        {
            return $"Mobility: {State}";
        }
    }
}