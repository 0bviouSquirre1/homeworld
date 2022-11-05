using static homeworld.Consumable.States;

namespace homeworld
{
    public static class Display
    {
        public static void AllEntities()
        {
            List<int> all_entities          = new List<int>();
            string name                     = "";
            XY location                     = new XY();

            Console.WriteLine();
            Console.WriteLine($"All entities currently present:");

            foreach (KeyValuePair<int, Entity> entity_node in EntityManager.GetAllEntities())
            {
                int entity_id                    = entity_node.Key;

                all_entities.Add(entity_id);
                name = Lookup.EntityName(entity_node.Value);
                location = Lookup.EntityLocation(entity_node.Value);

                string entity_id_display    = String.Format("{0,3}",   entity_id);
                string name_display         = String.Format("{0,-20}", name);
                // string location_display     = String.Format("{0,3}",   location);

                Console.WriteLine($"{entity_id_display} : {name_display} : {location}");
            }

            Console.WriteLine($"{all_entities.Count} entities present");
        }
        public static void EntitiesAtLocation(XY location)
        {
            Console.WriteLine();
            List<Entity> list = Lookup.EntitiesAtLocation(location);
            Console.WriteLine();
            Console.WriteLine($"Entities present at {location}:");
            foreach (Entity item in list)
            {
                Console.WriteLine(Lookup.EntityName(item));
            }
        }
        public static void OverheadMap(Entity player)
        {
            Console.WriteLine();
            // Display the map
            for (int y = 5; y >= -5; y--)
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
                    XY player_location          = Lookup.EntityLocation(player);

                    bool is_player_location     = (this_location.Equals(player_location));
                    bool contains_something     = (Lookup.EntitiesAtLocation(this_location).Count > 0);
                    bool room_has_been_explored = Lookup.ExploredMap[this_location];
                    
                    if (is_player_location)
                    {
                        Console.Write($"[ P ]");
                    }
                    else if (contains_something && room_has_been_explored)
                    {
                        List<Entity> present_entities = Lookup.EntitiesAtLocation(this_location);
                        char entity_display = FindChar(present_entities[0]);
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
                for (int x = -5; x < 6; x++)
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
        public static void EntityInventory(Entity entity)
        {
            var display_list = Lookup.EntityInventory(entity);
            Console.WriteLine();
            Console.WriteLine($"Inventory for entity {entity}");
            foreach (Entity item in display_list)
            {
                Console.WriteLine($"- {item}");
            }
            Console.WriteLine($"{display_list.Count} items");
        }
        public static void PlayerMoved(XY previous_location, XY next_location)
        {
            Console.WriteLine();
            Console.WriteLine($"The player has moved from {previous_location} to {next_location}");
        }
        public static void DropItem(Entity item)
        {
            Console.WriteLine();
            Console.WriteLine($"The player has dropped {item}.");
        }
        public static void GetItem(Entity item)
        {
            Console.WriteLine();
            Console.WriteLine($"The player has retrieved {item}.");
        }
        public static char FindChar(Entity entity) // possible candidate for textutil class
        {
            char return_char = ' ';
            List<IComponent> component_list = Lookup.AllComponentsOfEntity(entity);
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
        public static void AllComponentsOfEntity(Entity entity)
        {
            var list = Lookup.AllComponentsOfEntity(entity);
            Console.WriteLine();
            foreach (IComponent component in list)
            {
                Console.WriteLine($"{component.ToString()}");
            }
        }
        public static void ConsumedItem(Entity entity, Entity consumed_item, Consumable.States consumption_style)
        {
            string verb = "";
            switch(consumption_style)
            {
                case Deadly:
                    verb = "You shouldn't have eaten that!";
                    break;
                case Edible:
                    verb = "eats";
                    break;
                case Potable:
                    verb = "drinks";
                    break;
                case Applicable:
                    verb = "applies";
                    break;
                default:
                    break;
            }
            Console.WriteLine();
            if (consumption_style == Deadly)
                Console.WriteLine(verb);
            else
                Console.WriteLine($"{entity.Name()} {verb} {consumed_item}.");
        }
        public static void ContainerContents(Entity container)
        {
            Console.WriteLine();
            Console.WriteLine($"{container} contains {LiquidSystem.GetContents(container)} sips of liquid.");
        }
    }
}