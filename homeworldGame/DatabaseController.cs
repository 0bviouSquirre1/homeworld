using System.Data.SQLite;

namespace homeworld
{
    public class DatabaseController
    {
        string connectionString;
        SQLiteConnection connection;

        public DatabaseController()
        {
            connectionString = @"URI=file:/home/shannon/Documents/programming/homeworld/game.db";
            connection = new SQLiteConnection(connectionString);
        }

        // will add/remove entities to/from all_entities
        // access assemblage tables to build new entities
        // will add/remove components to/from entities
        // add new components to their respective tables and
        // the entity_components table
        // read: assemblages, assemblage_components, all_components, 
        // write: entity_components, all_entities, component-specfic tables

        // ENTITY METHODS

        public int CreateEntity()
        {
            int result = 0;
            connection.Open();

            string addStatement = 
            @"
                INSERT INTO all_entities(human_label) 
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

        public List<string> GetAllComponentTypes()
        {
            List<string> result = new List<string>();
            connection.Open();

            string statement = "SELECT * FROM all_components";
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