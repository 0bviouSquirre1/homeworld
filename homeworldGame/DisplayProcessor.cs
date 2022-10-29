namespace homeworld
{
    public static class Display
    {
        public static void AllEntities()
        {
            Console.WriteLine($"All entities currently present:");
            int count = 0;
            var all_entities = new List<int>();
            foreach (KeyValuePair<int, Dictionary<int, IComponent>> entity in EntityManager.AllEntities)
            {
                int entity_id = entity.Key;
                Dictionary<int, IComponent> component_list = entity.Value;

                all_entities.Add(entity_id);
                foreach (KeyValuePair<int, IComponent> component_node in component_list)
                {
                    var component = component_node.Value;
                    if (component is NameComponent)
                    {
                        Console.Write(entity_id);
                        Console.Write(" : ");
                        Console.Write(component.ToString());
                        Console.WriteLine();
                    }
                }
                count++;
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
            foreach (var component in component_list)
            {
                Console.Write($"{component.ComponentID}, ");
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
        public static void OverheadMap()
        {
            List<XYComponent> all_locations = EntityManager.GetAllComponentsOfType<XYComponent>();
            all_locations = all_locations.OrderBy(z => z.xValue).ThenBy(z => z.yValue).ToList();

            // Display the map
            for (int x = -5; x < 6; x++)
            {
                if (x >= 0) 
                    Console.Write($" {x}");
                else
                    Console.Write(x);

                Console.Write("[   ]");
                for (int y = -5; y < 6; y++)
                {
                    bool locationContainsSomething = all_locations.Any(location => location.xValue == x && location.yValue == y);
                    bool roomIsExplored = Map.ExploredMap[new XYComponent(x,y)];
                    bool playerLocation = (new XYComponent(x,y).Equals(EntityManager.GetEntityLocation(1)));
                    if (playerLocation)
                    {
                        Console.Write("[ P ]");
                    }
                    else if (locationContainsSomething && !roomIsExplored)
                    {
                        Console.Write("[ X ]");
                    }
                    else if (locationContainsSomething && roomIsExplored)
                    {
                        Console.Write("[ o ]");
                    }
                    else
                    {
                        Console.Write("[   ]");

                    }
                }
                Console.WriteLine();
            }
            
            Console.Write(" ");
            for (int y = -5; y < 6; y++)
            {
                if (y < 0)
                    Console.Write($"  {y} ");
                else
                    Console.Write($"   {y} ");
            }
            Console.WriteLine();
        }
    }
}