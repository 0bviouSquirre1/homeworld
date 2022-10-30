namespace homeworld
{
    public static class Display
    {
        public static List<XY> occupied_locations = Movement.entities_locations.Values.ToList();

        public static void AllEntities()
        {
            List<int> all_entities_display  = new List<int>();
            string name                     = "";
            XY location                     = new XY(99,99);

            Console.WriteLine($"All entities currently present:");

            foreach (KeyValuePair<int, Dictionary<int, IComponent>> entity in EntityManager.AllEntities)
            {
                int entity_id                               = entity.Key;
                Dictionary<int, IComponent> component_list  = entity.Value;

                all_entities_display.Add(entity_id);
                foreach (KeyValuePair<int, IComponent> component_node in component_list)
                {
                    IComponent component             = component_node.Value;
                    if (component is NameComponent)
                    {
                        NameComponent name_component    = (NameComponent)component;
                        name                            = name_component.Name;
                    }
                    if (component is Mobility)
                    {
                        Mobility mobility_component     = (Mobility)component;
                        location                        = mobility_component.Location;
                    }
                }
                string entity_id_display    = String.Format("{0,3}",   entity_id);
                string name_display         = String.Format("{0,-20}", name);
                string location_display     = String.Format("{0,-10}", location);

                Console.WriteLine($"{entity_id_display} : {name_display} : {location_display}");
            }

            Console.WriteLine($"{all_entities_display.Count} entities present");
        }
        public static void AllComponentsOfEntity(int entity_id)
        {
            // Validation
            EntityManager.AllEntities.TryGetValue(entity_id, out Dictionary<int, IComponent>? entity);
            if (entity is not null)
            {
                Dictionary<int, IComponent> component_list = EntityManager.GetComponentsOfEntity(entity_id);

                Console.WriteLine($"Entity {entity_id} Component List:");

                foreach (KeyValuePair<int, IComponent> component_node in component_list)
                {
                    int component_id        = component_node.Key;
                    string component_type   = component_node.Value.GetType().ToString();
                    IComponent component    = component_node.Value;

                    Console.WriteLine($"{component_id} - {component_type} - {component}");
                }
            }
            else
            {
                Console.WriteLine($"Entity {entity_id} does not exist.");
            }
        }
        public static void AllComponentsOfType<T>() where T : IComponent
        {
            List<T> component_list = EntityManager.GetAllComponentsOfType<T>();

            Console.WriteLine($"All components of type {typeof(T)}:");

            foreach (T component in component_list)
            {
                Console.Write($"{component.ComponentID}, ");
            }

            Console.WriteLine();
        }
        public static void AllEntitiesWithComponentType<T>() where T : IComponent
        {
            List<int> entity_list = EntityManager.GetAllEntitiesWithComponentType<T>();

            Console.WriteLine($"All entities with component of type {typeof(T)}:");

            foreach (int entity in entity_list)
            {
                Console.Write($"{entity}, ");
            }

            Console.WriteLine($"{entity_list.Count} entities");
        }
        public static void OverheadMap()
        {
            // Display the map
            for (int y = 5; y > -6; y--)
            {
                // Y-axis label and padding logic
                if (y >= 0) 
                    Console.Write($" {y}");
                else
                    Console.Write(y);

                // Logic for displaying each room
                for (int x = -5; x < 6; x++)
                {
                    XY this_location            = new XY(x,y);
                    XY player_location          = Movement.GetEntityLocation(1);

                    bool is_player_location     = (this_location.Equals(player_location));
                    bool contains_something     = occupied_locations.Contains(this_location);
                    bool room_has_been_explored = Map.ExploredMap[this_location];
                    
                    if (is_player_location)
                    {
                        Console.Write($"[ P ]");
                    }
                    else if (contains_something && room_has_been_explored)
                    {
                        // TODO: add a switch case here to display different symbols based on what is there
                        // TODO: how do we find out what Something is contained here?
                        Console.Write($"[ o ]");
                    } 
                    else if (contains_something && !room_has_been_explored)
                    {
                        Console.Write($"[ ? ]");
                    }
                    else
                    {
                        Console.Write($"[   ]");
                    }
                }

                // for bigger boxes, uncomment
                /*
                Console.WriteLine();
                Console.Write("  ");
                for (int y = -5; y < 6; y++)
                {
                    Console.Write($"[   ]");
                }
                */

                Console.WriteLine();
            }
            
            // X-axis label and padding
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