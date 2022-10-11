using System.Data.SQLite;

namespace homeworld
{
    public class DatabaseController
    {
        string connectionString;
        SQLiteConnection connection;

        public DatabaseController()
        {
            connectionString = @"URI=file:/home/shannon/Documents/programming/homeworld/homeworld.db";
            connection = new SQLiteConnection(connectionString);
        }

        // will add/remove entities to/from all_entities
        // access assemblage tables to build new entities
        // will add/remove components to/from entities
        // add new components to their respective tables and
        // the entity_components table
        // read: assemblages, assemblage_components, all_components, 
        // write: entity_components, component-specfic tables

        // ENTITY METHODS

        // public void GetAllEntitiesPossessingComponentType()
        // public void EntityHasComponent()

        public int CreateEntity()
        {
            int result = 0;
            connection.Open();

            string addStatement = 
            @"
                INSERT INTO all_entities(entity_name) 
                VALUES('jod')
            ";

            using var command = new SQLiteCommand(addStatement, connection);
            command.ExecuteNonQuery();
            connection.Close();

            result = GetLastEntityID();

            return result;
        }

        public bool EntityExists(int entity_id)
        {
            connection.Open();

            string statement = 
            @"
                SELECT entity_id 
                FROM all_entities
                WHERE entity_id = (@entity_id)
            ";
            using var command = new SQLiteCommand(statement, connection);
            command.Parameters.AddWithValue("@entity_id", entity_id);

            using SQLiteDataReader reader = command.ExecuteReader();
            int returned_id = 0;

            while (reader.Read())
            {
                returned_id = reader.GetInt32(0);
            }
            connection.Close();

            if (returned_id == entity_id)
            {
                return true;
            }

            return false;
        }

        public bool EntityHasComponent(int entity_id, IComponent component)
        {
            int component_type_id = GetComponentTypeID(component);
            connection.Open();

            string statement =
            @"
                SELECT entity_id, component_type_id
                FROM entity_components
                WHERE entity_id = (@entity_id) AND component_type_id = (@component_type_id)
            ";
            using var command = new SQLiteCommand(statement, connection);
            command.Parameters.AddWithValue("@entity_id", entity_id);
            command.Parameters.AddWithValue("@component_type_id", component_type_id);
            using SQLiteDataReader reader = command.ExecuteReader();

            int returned_id = 0;

            while (reader.Read())
            {
                returned_id = reader.GetInt32(0);
            }
            connection.Close();
            if (returned_id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<int> GetAllEntities()
        {
            List<int> result = new List<int>();
            connection.Open();

            string statement = 
            @"
                SELECT entity_id 
                FROM all_entities
            ";
            using var command = new SQLiteCommand(statement, connection);
            using SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int entityID = reader.GetInt32(0);
                result.Add(entityID);
            }

            connection.Close();

            return result;
        }

        public int GetLastEntityID()
        {
            int result = 0;
            connection.Open();

            string statement = 
            @"
                SELECT MAX(entity_id) 
                FROM all_entities 
            ";

            using var command = new SQLiteCommand(statement, connection);
            using SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result = reader.GetInt32(0);
            }
            connection.Close();

            return result;
        }

        public void KillAllEntities()
        {
            connection.Open();

            string statement = 
            @"
                DELETE FROM all_entities
            ";

            using var command = new SQLiteCommand(statement, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void KillEntity(int entity_id)
        {
            connection.Open();

            string statement = 
            @"
                DELETE FROM all_entities
                WHERE entity_id = (@entity_id)
            ";
            using var command = new SQLiteCommand(statement, connection);
            command.Parameters.AddWithValue("@entity_id", entity_id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        // COMPONENT METHODS

        public int AddComponent(int entity_id, IComponent component)
        {
            int component_type_id = GetComponentTypeID(component);
            Type component_type = component.GetType();
            // add entity_id and component_type_id to the table to get component_data_id
            // add component_data_id and values to the correct type table
            connection.Open();
            string entity_component_statement =
            @"
                INSERT INTO entity_components(entity_id, component_type_id)
                VALUES(@entity_id, @component_type_id)
            ";
            using var command = new SQLiteCommand(entity_component_statement, connection);
            command.Parameters.AddWithValue("@entity_id", entity_id);
            command.Parameters.AddWithValue("@component_type_id", component_type_id);
            command.ExecuteNonQuery();
            connection.Close();
            int component_data_id = GetLastComponentID();
            connection.Open();
            XY newComponent = (XY)component;
            string xytype_table_statement =
            @"
                INSERT INTO xy_table(component_data_id, X, Y)
                VALUES(@component_data_id, @X, @Y)
            ";
            var command2 = new SQLiteCommand(xytype_table_statement, connection);
            command2.Parameters.AddWithValue("@component_data_id", component_data_id);
            command2.Parameters.AddWithValue("@X", newComponent.X);
            command2.Parameters.AddWithValue("@Y", newComponent.Y);
            command2.ExecuteNonQuery();
            connection.Close();
            int result = GetLastComponentID();
            return result;
            
        }
        // public void GetAllComponentsOfType()
        // public IComponent GetComponent()

        public int GetComponentTypeID(IComponent component)
        {
            Type component_type = component.GetType();
            int component_type_id = 0;
            switch (component_type.ToString())
            {
                case "XY":
                    component_type_id = 1;
                    break;
                case "HealthPoints":
                    component_type_id = 2;
                    break;
                case "Grows":
                    component_type_id = 3;
                    break;
                default:
                    component_type_id = 4;
                    break;
            }
            return component_type_id;

        }

        public int GetLastComponentID()
        {
            int result = 0;
            connection.Open();

            string statement = 
            @"
                SELECT MAX(component_data_id) 
                FROM entity_components 
            ";

            using var command = new SQLiteCommand(statement, connection);
            using SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result = reader.GetInt32(0);
            }
            connection.Close();

            return result;
        }
        // public void RemoveComponent()

        public List<string> GetAllComponentTypes()
        {
            List<string> result = new List<string>();
            connection.Open();

            string statement = "SELECT * FROM all_component_types";
            using var command = new SQLiteCommand(statement, connection);
            using SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string className = reader.GetString(1);
                result.Add(className);
            }

            connection.Close();
            return result;
        }
    }
}