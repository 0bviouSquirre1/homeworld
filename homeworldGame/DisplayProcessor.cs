namespace homeworld
{
    public static class Display
    {
        public static void AllEntities()
        {
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
                    count++;
                }
                Console.WriteLine();
            }
            Console.WriteLine($"{count} entities present");
        }
        public static void AllComponentsOfEntity(int entity_id)
        {
            // Validation
            if (EntityManager.AllEntities[entity_id] is not null)
            {
                Console.WriteLine($"Entity {entity_id} Component List:");
                var component_list = EntityManager.GetComponentsOfEntity(entity_id);
                foreach (KeyValuePair<int, IComponent> componentNode in component_list)
                {
                    Console.WriteLine($"{componentNode.Key} - {componentNode.Value.GetType().ToString()} - {componentNode.Value}");
                }
            }
        }
        public static void AllComponentsOfType(Type type)
        {
            var component_list = EntityManager.GetAllComponentsOfType(type);
            foreach (KeyValuePair<int, IComponent> componentNode in component_list)
            {
                Console.WriteLine(componentNode.Value);
            }
        }
    }
}