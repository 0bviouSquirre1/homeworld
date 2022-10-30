namespace homeworld
{
    public static class Display
    {
        public static void AllEntities()
        {
            Console.WriteLine($"All entities currently present:");
            List<int> all_entities = new List<int>();
            string name = "";
            XY location = new XY(99,99);
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
                        var name_component = (NameComponent)component;
                        name = name_component.Name;
                    }
                    if (component is Mobility)
                    {
                        var mobility_component = (Mobility)component;
                        location = mobility_component.Location;
                    }
                }
                Console.WriteLine($"{entity_id} : {name} : {location}");
            }
            Console.WriteLine($"{all_entities.Count} entities present");
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
            var mobs = Movement.entity_locations;
            List<XY> all_locations = new List<XY>();
            foreach (KeyValuePair<int, XY> mobile in mobs)
            {
                all_locations.Add(mobile.Value);
            }

            all_locations = all_locations.OrderBy(z => z.xValue).ThenBy(z => z.yValue).ToList();

            // Display the map
            for (int y = 5; y > -6; y--)
            {
                if (y >= 0) 
                    Console.Write($" {y}");
                else
                    Console.Write(y);

                for (int x = -5; x < 6; x++)
                {
                    XY this_location = new XY(x,y);
                    XY player_location = Movement.GetEntityLocation(1);

                    bool isPlayerLocation = (this_location.Equals(player_location));
                    bool containsSomething = all_locations.Contains(this_location);
                    bool roomExplored = Map.ExploredMap[this_location];
                    
                    if (isPlayerLocation)
                    {
                        Console.Write($"[ P ]");
                    }
                    else if (containsSomething && roomExplored)
                    {
                        Console.Write($"[ o ]");
                    } 
                    else if (containsSomething && !roomExplored)
                    {
                        Console.Write($"[ ? ]");
                    }
                    else
                    {
                        Console.Write($"[   ]");
                    }
                }

                /* for bigger boxes, uncomment
                Console.WriteLine();
                Console.Write("  ");
                for (int y = -5; y < 6; y++)
                {
                    Console.Write($"[   ]");
                }*/

                Console.WriteLine();
            }
            
            // Displays the X-axis label
            Console.Write(" ");
            for (int x = -5; x < 6; x++)
            {
                if (x < 0)
                    Console.Write($"  {x} ");
                else
                    Console.Write($"   {x} ");
            }
            Console.WriteLine();
        }
    }
}