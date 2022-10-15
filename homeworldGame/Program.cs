// 1.6 when the assemblage creation is complete, check
// 1.6.1 Check for all components on a specific entity
// 1.6.2 compare that against the list of assemblage parts to confirm
// Cannot create a component without having an entity to attach it to
// 2. active world state checks
// 3.1.1 removed components should have an unload function to make sure they're not losing important information?
// start with the change that needs to be reflected in the database and work backwards

namespace homeworld
{
    public class Program
    {
        public static DatabaseController dbController = new DatabaseController();
        public static void Main()
        {
            Setup();
        }

        public static void Setup()
        {
            int player = dbController.CreateEntity(1, "jod");

            for (int i = 0; i < 10; i++)
            {
                dbController.CreateEntity(2, $"dummy_{i}");
            }
            for (int i = 0; i < 10; i++)
            {
                dbController.CreateEntity(3, $"plant_{i}");
            }
            
            List<int> entities_type1 = dbController.GetAllEntitiesWithComponentOfType(1);
            List<int> entities_type2 = dbController.GetAllEntitiesWithComponentOfType(2);
            List<int> entities_type3 = dbController.GetAllEntitiesWithComponentOfType(3);

            List<int> components_type1 = dbController.GetAllActiveComponentsOfType(1);
            List<int> components_type2 = dbController.GetAllActiveComponentsOfType(2);
            List<int> components_type3 = dbController.GetAllActiveComponentsOfType(3);

            foreach (int entity_id in entities_type1)
            {
                Console.WriteLine($"{entity_id} has component type location");
            }
            foreach (int entity_id in entities_type2)
            {
                Console.WriteLine($"{entity_id} has component type healthpoints");
            }
            foreach (int entity_id in entities_type3)
            {
                Console.WriteLine($"{entity_id} has component type growth");
            }

            foreach (int component_id in components_type1)
            {
                Console.WriteLine($"location component {component_id}");
            }
            foreach (int component_id in components_type2)
            {
                Console.WriteLine($"healthpoints component {component_id}");
            }
            foreach (int component_id in components_type3)
            {
                Console.WriteLine($"growth component {component_id}");
            }
        }
    }
}