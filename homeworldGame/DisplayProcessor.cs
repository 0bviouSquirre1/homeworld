namespace homeworld
{
    public static class Display
    {
        public static void AllEntities()
        {
            Console.WriteLine($"All entities currently present:");
            int count = 0;
            foreach (KeyValuePair<int, Dictionary<int, IComponent>> entity in EntityManager.AllEntities)
            {
                int entity_id = entity.Key;
                Dictionary<int, IComponent> component_list = entity.Value;

                Console.Write(entity_id.ToString("000"));
                foreach (KeyValuePair<int, IComponent> component_node in component_list)
                {
                    IComponent component = component_node.Value;
                    Console.Write(" : ");
                    Console.Write(component.ToString());
                }
                count++;
                Console.WriteLine();
            }
            Console.WriteLine($"{count} entities present");
        }
        public static void AllComponentsOfEntity(int entity_id)
        {
            // Validation
            EntityManager.AllEntities.TryGetValue(entity_id, out Dictionary<int, IComponent>? entity);
            if (entity is not null)
            {
                Console.WriteLine($"Entity {entity_id} Component List:");
                var component_list = EntityManager.GetComponentsOfEntity(entity_id);
                foreach (KeyValuePair<int, IComponent> componentNode in component_list)
                {
                    Console.WriteLine($"{componentNode.Key} - {componentNode.Value.GetType().ToString()} - {componentNode.Value}");
                }
            }
            else
            {
                Console.WriteLine($"Entity {entity_id} does not exist.");
            }
        }
        public static void AllComponentsOfType<T>() where T : IComponent
        {
            Console.WriteLine($"All components of type {typeof(T)}:");
            var component_list = EntityManager.GetAllComponentsOfType<T>();
            foreach (KeyValuePair<int, IComponent> componentNode in component_list)
            {
                Console.Write($"{componentNode.Value}, ");
            }
            Console.WriteLine();
        }
        public static void AllEntitiesWithComponentType<T>() where T : IComponent
        {
            Console.WriteLine($"All entities with component of type {typeof(T)}:");
            var entity_list = EntityManager.GetAllEntitiesWithComponentType<T>();
            foreach (int entity in entity_list)
            {
                Console.Write($"{entity}, ");
            }
            Console.WriteLine();
        }
    }
}