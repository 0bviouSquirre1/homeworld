using System.Data.SQLite;

namespace homeworld
{
    public class DatabaseController {
        string connectionString;
        SQLiteConnection connection;

        public DatabaseController()
        {
            connectionString = @"URI=file:/home/shannon/Documents/programming/homeworld/homeworld.db";
            connection = new SQLiteConnection(connectionString);
        }

        // METHODS

        // Database-level Methods (contain queries, no logic)
        public int GetComponentDataID(int entity_id, int component_type_id)
        {
            connection.Open();

            string get_component_data_id_statement = 
            @"
                SELECT component_data_id
                FROM entity_components
                WHERE entity_id = (@entity_id) AND component_type_id = (@component_type_id)
            ";
            using var get_component_data_id_command = new SQLiteCommand(get_component_data_id_statement, connection);
            get_component_data_id_command.Parameters.AddWithValue("@entity_id", entity_id);
            get_component_data_id_command.Parameters.AddWithValue("@component_type_id", component_type_id);
            using SQLiteDataReader entity_component_reader = get_component_data_id_command.ExecuteReader();

            int component_data_id = 0;
            while (entity_component_reader.Read())
            {
                component_data_id = entity_component_reader.GetInt32(0);
            }

            connection.Close();
            return component_data_id;
        }

        public List<int> GetComponentsOfAnEntity(int entity_id)
        {
            connection.Open();
            string get_component_data_ids_statement = 
            @"
                SELECT component_data_id
                FROM entity_components
                WHERE entity_id = (@entity_id)
            ";
            using var get_component_data_ids_command = new SQLiteCommand(get_component_data_ids_statement, connection);
            get_component_data_ids_command.Parameters.AddWithValue("@entity_id", entity_id);
            using SQLiteDataReader entity_component_reader = get_component_data_ids_command.ExecuteReader();
            
            List<int> list_of_component_data_ids = new List<int>();
            while (entity_component_reader.Read())
            {
                list_of_component_data_ids.Add(entity_component_reader.GetInt32(0));
            }
            connection.Close();
            return list_of_component_data_ids;
        }

        public List<int> GetAllActiveComponentsOfType(int component_type_id)
        {
            connection.Open();
            string get_component_data_ids_statement = 
            @"
                SELECT component_data_id
                FROM entity_components
                WHERE component_type_id = (@component_type_id)
            ";
            using var get_component_data_ids_command = new SQLiteCommand(get_component_data_ids_statement, connection);
            get_component_data_ids_command.Parameters.AddWithValue("@component_type_id", component_type_id);
            using SQLiteDataReader entity_component_reader = get_component_data_ids_command.ExecuteReader();
            
            List<int> list_of_component_data_ids = new List<int>();
            while (entity_component_reader.Read())
            {
                list_of_component_data_ids.Add(entity_component_reader.GetInt32(0));
            }
            connection.Close();
            return list_of_component_data_ids;
        }

        public List<int> GetAllComponentTypeIDs()
        {
            connection.Open();
            string get_all_component_type_ids_statement =
            @"
                SELECT component_type_id
                FROM all_components
            ";
            using var get_all_component_type_ids_command = new SQLiteCommand(get_all_component_type_ids_statement, connection);
            using SQLiteDataReader all_components_reader = get_all_component_type_ids_command.ExecuteReader();
            connection.Close();

            List<int> list_of_component_type_ids = new List<int>();
            while (all_components_reader.Read())
            {
                list_of_component_type_ids.Add(all_components_reader.GetInt32(0));
            }
            return list_of_component_type_ids;
        }

        public List<int> GetComponentsForAssemblage(int assemblage_id)
        {
            connection.Open();

            string assemblage_statement =
            @"
                SELECT component_type_id
                FROM assemblage_components
                WHERE assemblage_id = (@assemblage_id)
            ";
            using var assemblage_command = new SQLiteCommand(assemblage_statement, connection);
            assemblage_command.Parameters.AddWithValue("@assemblage_id", assemblage_id);
            using SQLiteDataReader assemblage_reader = assemblage_command.ExecuteReader();

            List<int> list_of_component_type_ids = new List<int>();
            while (assemblage_reader.Read())
            {
                list_of_component_type_ids.Add(assemblage_reader.GetInt32(0));
            }

            connection.Close();
            return list_of_component_type_ids;
        }

        public List<int> GetAllEntitiesWithComponentOfType(int component_type_id)
        {
            connection.Open();
            string get_entity_ids_statement = 
            @"
                SELECT entity_id
                FROM entity_components
                WHERE component_type_id = (@component_type_id)
            ";
            using var get_entity_ids_command = new SQLiteCommand(get_entity_ids_statement, connection);
            get_entity_ids_command.Parameters.AddWithValue("@component_type_id", component_type_id);
            using SQLiteDataReader entity_component_reader = get_entity_ids_command.ExecuteReader();
            
            List<int> list_of_entity_ids = new List<int>();
            while (entity_component_reader.Read())
            {
                list_of_entity_ids.Add(entity_component_reader.GetInt32(0));
            }
            connection.Close();
            return list_of_entity_ids;
        }

        public int InsertEntity(string entity_name)
        {
            connection.Open();

            string insert_entity_statement =
            @"
                INSERT INTO all_entities (entity_name)
                VALUES (@entity_name)
                RETURNING entity_id
            ";
            using var create_entity_command = new SQLiteCommand(insert_entity_statement, connection);
            create_entity_command.Parameters.AddWithValue("@entity_name", entity_name);
            create_entity_command.ExecuteNonQuery();
            long entity_id = connection.LastInsertRowId;

            connection.Close();
            return (int)entity_id;
        }

        public int InsertComponent(int entity_id, int component_type_id)
        {
            connection.Open();

            string add_to_entity_components_statement = 
            @"
                INSERT INTO entity_components (entity_id, component_type_id)
                VALUES (@entity_id, @component_type_id)
                RETURNING component_data_id
            ";
            using var add_to_entity_components_command = new SQLiteCommand(add_to_entity_components_statement, connection);
            add_to_entity_components_command.Parameters.AddWithValue("@entity_id", entity_id);
            add_to_entity_components_command.Parameters.AddWithValue("@component_type_id", component_type_id);
            add_to_entity_components_command.ExecuteNonQuery();
            long component_data_id = connection.LastInsertRowId;

            connection.Close();
            return (int)component_data_id;
        }

        public int InsertComponentToXYTable(int component_data_id, int component_x, int component_y)
        {
            connection.Open();

            string add_to_xy_table_statement = 
            @"
                INSERT INTO xy_table (component_data_id, x, y)
                VALUES (@component_data_id, @component_x, @component_y)
            ";
            using var add_to_xy_table_command = new SQLiteCommand(add_to_xy_table_statement, connection);
            add_to_xy_table_command.Parameters.AddWithValue("@component_data_id", component_data_id);
            add_to_xy_table_command.Parameters.AddWithValue("@component_x", component_x);
            add_to_xy_table_command.Parameters.AddWithValue("@component_y", component_y);
            add_to_xy_table_command.ExecuteNonQuery();

            connection.Close();
            return component_data_id;
        }

        public void DeleteComponentFromEntity(int entity_id, int component_data_id)
        {
            connection.Open();
            string delete_from_entity_components_statement = 
            @"
                DELETE FROM entity_components
                WHERE entity_id = (@entity_id) AND component_data_id = (@component_data_id)
            ";
            using var delete_from_entity_components_command = new SQLiteCommand(delete_from_entity_components_statement, connection);
            delete_from_entity_components_command.Parameters.AddWithValue("@entity_id", entity_id);
            delete_from_entity_components_command.Parameters.AddWithValue("@component_data_id", component_data_id);
            delete_from_entity_components_command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteEntity(int entity_id)
        {
            connection.Open();
            string delete_entity_statement = 
            @"
                DELETE FROM all_entities
                WHERE entity_id = (@entity_id)
            ";
            using var delete_entity_command = new SQLiteCommand(delete_entity_statement, connection);
            delete_entity_command.Parameters.AddWithValue("@entity_id", entity_id);
            delete_entity_command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteAllEntities()
        {
            connection.Open();
            string delete_all_entities_statement = 
            @"
                DELETE FROM all_entities
            ";
            using var delete_all_entities_command = new SQLiteCommand(delete_all_entities_statement, connection);
            delete_all_entities_command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteAllComponentsFromEntity(int entity_id)
        {
            connection.Open();
            string delete_from_entity_components_statement = 
            @"
                DELETE FROM entity_components
                WHERE entity_id = (@entity_id)
            ";
            using var delete_from_entity_components_command = new SQLiteCommand(delete_from_entity_components_statement, connection);
            delete_from_entity_components_command.Parameters.AddWithValue("@entity_id", entity_id);
            delete_from_entity_components_command.ExecuteNonQuery();
            connection.Close();
        }

        // Game architecture layer? This is implementing business rules aboug how entities are created
        
        public int CreateEntity(int assemblage_id, string entity_name)
        {
            // look up assemblage_id on assemblage_components
            List<int> assemblage_component_type_ids = GetComponentsForAssemblage(assemblage_id);
            
            int entity_id = InsertEntity(entity_name);

            foreach (int component_type_id in assemblage_component_type_ids)
            {
                CreateComponent(entity_id, component_type_id);
            }
            return entity_id;
        }

        public int CreateComponent(int entity_id, int component_type_id)
        {
            // if the entity does NOT have this component type already
            if (!EntityHasComponentType(entity_id, component_type_id))
            {
                // create a new XY component/object
                XY location = new XY(1,2);
                int location_component_data_id = InsertComponent(entity_id, component_type_id);

                InsertComponentToXYTable(location_component_data_id, location.X, location.Y);
                return location_component_data_id;
            }
            else
            {
                // the entity DOES have this component type, return component_data_id
                int component_data_id = GetComponentDataID(entity_id, component_type_id);
                return component_data_id;
            }
        }

        public bool EntityHasComponentType(int entity_id, int component_type_id)
        {
            int component_data_id = GetComponentDataID(entity_id, component_type_id);

            if (component_data_id != 0) // figure out what actually gets returned when no records
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveComponent(int entity_id, int component_data_id)
        {
            DeleteComponentFromEntity(entity_id, component_data_id);
        }

        public void RemoveAllComponents(int entity_id)
        {
            DeleteAllComponentsFromEntity(entity_id);
        }

        public void KillEntity(int entity_id)
        {
            DeleteEntity(entity_id);
        }

        public void KillAllEntities()
        {
            DeleteAllEntities();
        }
    }
}