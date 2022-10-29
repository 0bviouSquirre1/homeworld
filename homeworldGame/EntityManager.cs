namespace homeworld
{
    public static class EntityManager
    {
        // outer dictionary: entity_id, component list
        // inner dictionary: component_id, component
        public static Dictionary<int, Dictionary<int, IComponent>> AllEntities = new Dictionary<int, Dictionary<int, IComponent>>();

        // METHODS

        public static int CreateEntity(List<IComponent> components)
        {
            Entity entity = new Entity();
            AllEntities.Add(entity.EntityID, entity.EntityComponents);

            foreach (IComponent component in components)
            {
                entity.EntityComponents.Add(component.ComponentID, component);
            }

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
        public static List<T> GetAllComponentsOfType<T>() where T : IComponent
        {
            List<T> list_of_components = new List<T>();

            foreach (KeyValuePair<int, Dictionary<int, IComponent>> entity in AllEntities)
            {
                foreach (KeyValuePair<int, IComponent> componentNode in entity.Value)
                {
                    if (componentNode.Value is T)
                    {
                        list_of_components.Add((T)componentNode.Value);
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

        // Component-Specific Methods (split out into Systems?)
        public static XYComponent GetEntityLocation(int entity_id)
        {
            var component_list = GetComponentsOfEntity(entity_id);
            foreach (var component in component_list)
            {
                if (component.Value.GetType() == typeof(XYComponent))
                {
                    return (XYComponent)component.Value;
                }
            }
            return new XYComponent(99,99);
        }
    }
}