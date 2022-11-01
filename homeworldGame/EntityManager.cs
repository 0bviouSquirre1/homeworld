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
    }
}