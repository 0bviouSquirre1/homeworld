namespace homeworld
{
    public class Entity
    {
        private int entity_id;
        private static int last_entity_id = 0;
        public int EntityID 
        {
            get => entity_id;
            set => entity_id = value;
        }

        public Dictionary<int, IComponent> EntityComponents = new Dictionary<int, IComponent>();

        public Entity()
        {
            EntityID = NextEntityID();
        }

        // METHODS

        private static int NextEntityID()
        {
            last_entity_id++;
            return last_entity_id;
        }

        // OVERRIDES

        public override string ToString()
        {
            return $"{EntityID}";
        }
    }
}