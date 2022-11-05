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
        public static void AddEntity(int entity_id, Entity entity)
        {
            AllEntities.Add(entity_id, entity);
        }
        public static void RemoveEntity(int entity_id)
        {
            AllEntities.Remove(entity_id);
        }
        #endregion

        private static int last_entity_id = 0;
        public static int NextEntityID()
        {
            last_entity_id++;
            return last_entity_id;
        }
        public static Entity CreateEntity(Archetype.States archetype, XY location)
        {
            Entity entity = new Entity(archetype);
            EntityManager.UpdateComponentEntityIDs(entity);
            EntityManager.AddEntity(entity.EntityID, entity);
            Movement.UpdateEntityLocation(entity, location);
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

        #region Components
        public static T AddComponent<T>(Entity entity) where T : IComponent, new()
        {
            var return_component = new T();
            Lookup.AllComponentsOfEntity(entity).Add(return_component);
            return_component.PlantID = entity.EntityID;
            return return_component;
        }
        public static void RemoveComponent<T>(Entity entity) where T : IComponent
        {
            var component = Lookup.ComponentOfEntityByType<T>(entity);
            component
                .MatchSome(c => Lookup.AllComponentsOfEntity(entity).Remove(c));
        }
        public static void UpdateComponentEntityIDs(Entity entity)
        {
            entity.ComponentList.ForEach(c => c.PlantID = entity.EntityID);
            foreach (IComponent component in entity.ComponentList)
            {
                component.PlantID = entity.EntityID;
            }
        }
        #endregion
    }
}