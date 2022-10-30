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
        public int EntityID { get; set; }
        public XY Location { get; set; }

        public Mobility(int entity_id, States state, XY location)
        {
            ComponentID = IComponent.NextComponentID();
            State = state;
            EntityID = entity_id;
            Location = location;
            Movement.entities_locations.Add(entity_id, Location);
            if (State == States.Movable)
            {
                Movement.movable_entities.Add(entity_id, this);
            }
        }

        // METHODS

        public override string ToString()
        {
            return $"Mobility: {State}";
        }
    }
}