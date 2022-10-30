namespace homeworld
{
    public static class Movement
    {
        public static Dictionary<int, Mobility> movable_entities = new Dictionary<int, Mobility>();
        public static Dictionary<int, XY> entity_locations = new Dictionary<int, XY>();
        public static XY GetEntityLocation(int entity_id)
        {
            var component_list = EntityManager.GetComponentsOfEntity(entity_id);
            foreach (KeyValuePair<int, IComponent> component_node in component_list)
            {
                IComponent component = component_node.Value;
                if (component.GetType() == typeof(Mobility))
                {
                    var location = (Mobility)component;
                    return location.Location;
                }
            }
            Console.WriteLine("GetEntityLocation() failed");
            return new XY(99,99);
        }
        public static void UpdateEntityLocation(int entity_id, XY next_location)
        {
            movable_entities[entity_id].Location = next_location;
        }
        public static void MovePlayer(XY nextLocation)
        {
            UpdateEntityLocation(1, nextLocation);
            Map.ExploreRoom(nextLocation);
        }
    }
}