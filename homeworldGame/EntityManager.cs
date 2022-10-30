using static homeworld.Mobility.States;
using static homeworld.Consumable.States;

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
        public static void AddComponentToEntity(int entity_id, IComponent component)
        {
            Dictionary<int, IComponent> entity_components = GetComponentsOfEntity(entity_id);
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
                foreach (KeyValuePair<int, IComponent> component_node in entity.Value)
                {
                    if (component_node.Value is T)
                    {
                        entity_list.Add(entity.Key);
                    }
                }
            }
            return entity_list;
        }
        public static void RemoveComponentFromEntity(int entity_id, int component_id)
        {
            Dictionary<int, IComponent> entity_components = GetComponentsOfEntity(entity_id);
            entity_components.Remove(component_id);

        }
        public static void DestroyEntity(int entity_id)
        {
            // possibly decommission components first?
            AllEntities.Remove(entity_id);
        }
    }
}