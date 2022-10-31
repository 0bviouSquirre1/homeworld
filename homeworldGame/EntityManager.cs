using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;

namespace homeworld
{
    public static class EntityManager
    {
        // TODO: implement archetypes
        private static int last_entity_id = 0;
        private static int NextEntityID()
        {
            last_entity_id++;
            return last_entity_id;
        }
        public static int CreateEntity(Archetype.States archetype)
        {
            int entity = EntityManager.NextEntityID();

            List<IComponent> component_list = Lookup.ArchetypeComponents(archetype);
            foreach (IComponent component in component_list)
            {
                component.EntityID = entity;
            }
            Lookup.AllEntities.Add(entity, component_list);

            return entity;
        }
        public static void AddComponentToEntity(int entity_id, IComponent component)
        {
            List<IComponent> component_list = Lookup.AllComponentsOfEntity(entity_id);
            component_list.Add(component);
        }
        public static void RemoveComponentFromEntity(int entity_id, IComponent component)
        {
            List<IComponent> component_list = Lookup.AllComponentsOfEntity(entity_id);
            component_list.Remove(component);

        }
        public static void DestroyEntity(int entity_id)
        {
            // possibly decommission components first?
            Lookup.AllEntities.Remove(entity_id);
        }
    }
}