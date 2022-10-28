namespace homeworld
{
    public static class EntityManager
    {
        // outer dictionary: entity_id, component dictionary
        // inner dictionary: component_type_id, component
        public static Dictionary<int, Dictionary<int, IComponent>> AllEntities = new Dictionary<int, Dictionary<int, IComponent>>();

        // METHODS

        public static Entity CreateEntity(string name, XYComponent location)
        {
            Entity entity = new Entity();
            AllEntities.Add(entity.EntityID, entity.EntityComponents);

            AddComponentToEntity(entity.EntityID, new NameComponent(name));
            AddComponentToEntity(entity.EntityID, location);

            return entity;
        }
        public static IComponent CreateComponent(int component_type_id)
        {
            IComponent returnComponent;
            switch (component_type_id)
            {
                case 0:
                    returnComponent = new XYComponent();
                    break;
                default:
                    returnComponent = new XYComponent(99,99);
                    break;
            }
            return returnComponent;
        }
        public static void AddComponentToEntity(int entity_id, IComponent component)
        {
            Dictionary<int, IComponent> entity_components = AllEntities[entity_id];
            entity_components.Add(component.ComponentID, component);
        }
        public static void RemoveComponentFromEntity(int entity_id, int component_type_id)
        {
            Dictionary<int, IComponent> entity_components = AllEntities[entity_id];
            entity_components.Remove(component_type_id);

        }
        public static void DestroyEntity(int entity_id)
        {
            // possibly decommission components first?
            AllEntities.Remove(entity_id);
        }
    }
}