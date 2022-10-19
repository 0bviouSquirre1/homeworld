namespace homeworld
{        
    public static class EntityProcessor
    {
        static List<int> all_entities = DatabaseController.GetAllEntities();

        // Game architecture layer
        // This is implementing business rules aboug how entities are created

        public static int CreateEntity(int assemblage_id, string entity_name)
        {
            // look up assemblage_id on assemblage_components
            List<int> assemblage_component_type_ids = DatabaseController.GetComponentsForAssemblage(assemblage_id);
            
            int entity_id = DatabaseController.InsertEntity(entity_name);
            all_entities.Add(entity_id);

            foreach (int component_type_id in assemblage_component_type_ids)
            {
                CreateComponent(entity_id, component_type_id);
            }
            return entity_id;
        }

        public static int CreateComponent(int entity_id, int component_type_id)
        {
            // if the entity does NOT have this component type already
            if (!EntityHasComponentType(entity_id, component_type_id))
            {
                // create a new XY component/object
                XYComponent location = new XYComponent(1,2);
                int location_component_data_id = DatabaseController.InsertComponent(entity_id, component_type_id);

                DatabaseController.InsertComponentToXYTable(location_component_data_id, location.X, location.Y);
                return location_component_data_id;
            }
            else
            {
                // the entity DOES have this component type, return component_data_id
                int component_data_id = DatabaseController.GetComponentDataID(entity_id, component_type_id);
                return component_data_id;
            }
        }

        public static bool EntityHasComponentType(int entity_id, int component_type_id)
        {
            int component_data_id = DatabaseController.GetComponentDataID(entity_id, component_type_id);

            if (component_data_id != 0) // figure out what actually gets returned when no records
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void RemoveComponent(int entity_id, int component_data_id)
        {
            DatabaseController.DeleteComponentFromEntity(entity_id, component_data_id);
        }

        public static void RemoveAllComponents(int entity_id)
        {
            DatabaseController.DeleteAllComponentsFromEntity(entity_id);
        }

        public static void KillEntity(int entity_id)
        {
            DatabaseController.DeleteEntity(entity_id);
        }

        public static void KillAllEntities()
        {
            DatabaseController.DeleteAllEntities();
        }
    }
}