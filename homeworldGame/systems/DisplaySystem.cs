namespace homeworld
{
    public static class Display
    {
        public static void AllEntities()
        {
            List<int> all_entities          = new List<int>();
            string display_name             = "";
            XY display_location             = new XY(99,99);

            Console.WriteLine($"All entities currently present:");

            foreach (KeyValuePair<int, Entity> entity_node in Lookup.AllEntities)
            {
                int entity_id                    = entity_node.Key;
                List<IComponent> component_list  = entity_node.Value.ComponentList;

                all_entities.Add(entity_id);
                foreach (IComponent component in component_list)
                {
                    if (component is NameComponent)
                    {
                        NameComponent name_component    = (NameComponent)component;
                        display_name                    = name_component.Name;
                    }
                }
                display_location = Lookup.EntityLocation(entity_id);

                string entity_id_display    = String.Format("{0,3}",   entity_id);
                string name_display         = String.Format("{0,-20}", display_name);
                string location_display     = String.Format("{0,-10}", display_location);

                Console.WriteLine($"{entity_id_display} : {name_display} : {location_display}");
            }

            Console.WriteLine($"{all_entities.Count} entities present");
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
                    XY player_location          = Lookup.EntityLocation(1);

                    bool is_player_location     = (this_location.Equals(player_location));
                    bool contains_something     = Lookup.LocationIsOccupied(this_location);
                    bool room_has_been_explored = Lookup.ExploredMap[this_location];
                    
                    if (is_player_location)
                    {
                        Console.Write($"[ P ]");
                    }
                    else if (contains_something && room_has_been_explored)
                    {
                        List<Entity> present_entities = Lookup.EntitiesByLocation[this_location].Values.ToList();
                        char entity_display = FindChar(present_entities[0].EntityID);
                        Console.Write($"[ {entity_display} ]");
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
        public static void EntityInventory(int entity_id)
        {
            // what to do about nulllllllllssssssss
            var list = Lookup.ComponentOfEntityByType<Inventory>(entity_id).InventoryList;
            if (list is not null)
            {
                foreach (Entity item in list)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine($"{list.Count} items");
            }
        }        
        public static char FindChar(int entity_id) // possible candidate for textutil class
        {
            char return_char = ' ';
            List<IComponent> component_list = Lookup.AllComponentsOfEntity(entity_id);
            foreach (IComponent component in component_list)
            {
                if (component is Archetype)
                {
                    Archetype arch_component = (Archetype)component;
                    switch (arch_component.State)
                    {
                        case Archetype.States.Well:
                            return_char = 'O';
                            break;
                        case Archetype.States.Bucket:
                            return_char = 'U';
                            break;
                        case Archetype.States.Kettle:
                            return_char = 'H';
                            break;
                        case Archetype.States.Plant:
                            return_char = '#';
                            break;
                        default:
                            return_char = 'o';
                            break;
                    }
                }
            }
            return return_char;
        }
    }
}