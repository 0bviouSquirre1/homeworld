using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;

namespace homeworld
{
    public static class EntityManager
    {
        #region AllEntities
        private static Dictionary<int, Entity> AllEntities = new Dictionary<int, Entity>();
        public static IReadOnlyDictionary<int, Entity> GetAllEntities()
        {
            return AllEntities;
        }
        public static void AddEntityToAllEntities(int entity_id, Entity entity)
        {
            AllEntities.Add(entity_id, entity);
        }
        public static void RemoveEntity(int entity_id)
        {
            AllEntities.Remove(entity_id);
        }
        #endregion

        #region EntityCreation
        private static int last_entity_id = 0;
        public static int NextEntityID()
        {
            last_entity_id++;
            return last_entity_id;
        }
        public static Entity CreateEntity(Archetype.States archetype, string name, XY location, Mobility.States mobility)
        {
            Entity entity = new Entity(archetype, name, location, mobility);
            EntityManager.UpdateComponentEntityIDs(entity);
            EntityManager.AddEntityToAllEntities(entity.EntityID, entity);
            // Movement.UpdateEntityLocation(entity.EntityID, location);
            return entity;
        }
        public static void KillEntity(int entity_id)
        {
            EntityManager.RemoveEntity(entity_id);
        }
        public static void KillAllEntities()
        {
            foreach (KeyValuePair<int, Entity> entity in EntityManager.GetAllEntities())
            {
                KillEntity(entity.Key);
            }
        }
        #endregion

        #region Components
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
        public static void UpdateComponentEntityIDs(Entity entity)
        {
            entity.ComponentList.ForEach(c => c.EntityID = entity.EntityID);
            foreach (IComponent component in entity.ComponentList)
            {
                component.EntityID = entity.EntityID;
            }
        }
        #endregion
    }
}