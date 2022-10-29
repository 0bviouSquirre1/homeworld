namespace homeworld
{
    public static class EntityManager
    {
        // outer dictionary: entity_id, component list
        // inner dictionary: component_id, component
        public static Dictionary<int, Dictionary<int, IComponent>> AllEntities = new Dictionary<int, Dictionary<int, IComponent>>();

        // METHODS

        public static int CreateEntity(string name, XYComponent location)
        {
            Entity entity = new Entity();
            AllEntities.Add(entity.EntityID, entity.EntityComponents);

            AddComponentToEntity(entity.EntityID, new NameComponent(name));
            AddComponentToEntity(entity.EntityID, location);

            return entity.EntityID;
        }
        public static IComponent CreateComponent<T>(T type) where T : IComponent
        {
            object? returnComponent;
            switch (type)
            {
                case NameComponent:
                    string? name = Console.ReadLine();
                    returnComponent = new NameComponent(name!);
                    break;
                case XYComponent:
                    returnComponent = new XYComponent();
                    break;
                default:
                    returnComponent = null;
                    break;
            }
            return (T)returnComponent!;
        }
        public static void AddComponentToEntity(int entity_id, IComponent component)
        {
            var entity_components = GetComponentsOfEntity(entity_id);
            entity_components.Add(component.ComponentID, component);
        }
        public static Dictionary<int, IComponent> GetComponentsOfEntity(int entity_id)
        {
            Dictionary<int, IComponent> entity_components = AllEntities[entity_id];
            return entity_components;
        }
        public static Dictionary<int, IComponent> GetAllComponentsOfType<T>() where T : IComponent
        {
            Dictionary<int, IComponent> list_of_components = new Dictionary<int, IComponent>();

            foreach (KeyValuePair<int, Dictionary<int, IComponent>> entity in AllEntities)
            {
                foreach (KeyValuePair<int, IComponent> componentNode in entity.Value)
                {
                    if (componentNode.Value is T)
                    {
                        list_of_components.Add(componentNode.Key, componentNode.Value);
                    }
                }
            }
            return list_of_components;
        }
        public static List<int> GetAllEntitiesWithComponentType<T>() where T : IComponent
        {
            List<int> entity_list = new List<int>();
            foreach (KeyValuePair<int, Dictionary<int, IComponent>> entity in AllEntities)
            {
                foreach (KeyValuePair<int, IComponent> componentNode in entity.Value)
                {
                    if (componentNode.Value is T)
                    {
                        entity_list.Add(entity.Key);
                    }
                }
            }
            return entity_list;
        }
        public static void RemoveComponentFromEntity(int entity_id, int component_id)
        {
            var entity_components = GetComponentsOfEntity(entity_id);
            entity_components.Remove(component_id);

        }
        public static void DestroyEntity(int entity_id)
        {
            // possibly decommission components first?
            AllEntities.Remove(entity_id);
        }
    }
}