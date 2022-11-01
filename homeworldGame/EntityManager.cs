using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;

namespace homeworld
{
    public static class EntityManager
    {
        private static int last_entity_id = 0;
        public static int NextEntityID()
        {
            last_entity_id++;
            return last_entity_id;
        }
        public static Entity CreateEntity(Archetype.States archetype, XY location)
        {
            // Create base entity
            Entity entity = new Entity(archetype);

            // Ensure each component knows its entity
            foreach (IComponent component in entity.ComponentList)
            {
                component.EntityID = entity.EntityID;
            }

            // Add to data stores
            Lookup.AllEntities.Add(entity.EntityID, entity);
            var entry           = new Dictionary<int, Entity>()
            {
                { entity.EntityID, entity }
            };
            if (Lookup.EntitiesByLocation.ContainsKey(location))
                Lookup.EntitiesByLocation[location].Add(entity.EntityID, entity);
            else
                Lookup.EntitiesByLocation.Add(location, entry);
            return entity;
        }

        /*public static void AddComponentToEntity(int entity_id, IComponent component)
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
            Lookup.EntitiesAndComponents.Remove(entity_id);
        }*/
    }
}