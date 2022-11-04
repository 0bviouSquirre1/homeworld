using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;

namespace homeworld
{
    public static class EntityManager
    {
        public static Dictionary<int, Entity> AllEntities = new Dictionary<int, Entity>();
        public static Dictionary<int, Entity> GetAllEntities()
        {
            return new Dictionary<int, Entity>();
        }
        
        private static int last_entity_id = 0;
        public static int NextEntityID()
        {
            last_entity_id++;
            return last_entity_id;
        }
        public static Entity CreateEntity(Archetype.States archetype, XY location)
        {
            // Create base entity
            Entity entity = new Entity(archetype, location);

            // Ensure each component knows its entity
            entity.ComponentList.ForEach(c => c.EntityID = entity.EntityID);
            foreach (IComponent component in entity.ComponentList)
            {
                component.EntityID = entity.EntityID;
            }

            // Add to data stores
            EntityManager.AllEntities.Add(entity.EntityID, entity);

            Movement.UpdateEntityLocation(entity.EntityID, location);
            return entity;
        }
        public static void KillEntity(int entity_id)
        {
            EntityManager.AllEntities.Remove(entity_id);
        }
        public static void KillAllEntities()
        {
            foreach (KeyValuePair<int, Entity> entity in EntityManager.AllEntities)
            {
                KillEntity(entity.Key);
            }
        }
        public static T AddComponent<T>(int entity_id) where T : IComponent, new()
        {
            var return_component = new T();
            Lookup.AllComponentsOfEntity(entity_id).Add(return_component);
            return_component.EntityID = entity_id;
            return return_component;
        }
        public static void RemoveComponent<T>(int entity_id) where T : IComponent
        {
            var component = Lookup.ComponentOfEntityByType<T>(entity_id);
            component
                .MatchSome(c => Lookup.AllComponentsOfEntity(entity_id).Remove(c));
        }
    }
}