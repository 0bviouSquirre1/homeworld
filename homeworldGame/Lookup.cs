using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;
using Optional;

namespace homeworld
{
    public static class Lookup
    {
        public static Dictionary<XY, bool> ExploredMap = new Dictionary<XY, bool>();
        /* I want to use this to clean up the archetype code at some point, but there's too much to change right now
        public static void Beetroot(List<IComponent> return_list, Archetype.States archetype, string name, XY location, Mobility.States mobility)
        {
            return_list.Add(new Archetype(archetype));
            return_list.Add(new NameComponent(name));
            return_list.Add(new Location(location));
            return_list.Add(new Mobility(mobility));
        }*/
        public static List<IComponent> ArchetypeComponents(Archetype.States archetype)
        {
            List<IComponent> return_list = new List<IComponent>();
            switch (archetype)
            {
                case Archetype.States.None:
                    return_list.Add(new Archetype(Archetype.States.None));
                    return_list.Add(new NameComponent());
                    break;
                case Player:
                    return_list.Add(new Archetype(Player));
                    return_list.Add(new NameComponent("player"));
                    return_list.Add(new Inventory());
                    return_list.Add(new Location());
                    return_list.Add(new Mobility(Movable));
                    break;
                case Produce:
                    return_list.Add(new Archetype(Produce));
                    return_list.Add(new NameComponent());
                    return_list.Add(new Brewable());
                    return_list.Add(new Consumable(Edible));
                    return_list.Add(new Growable());
                    return_list.Add(new Mobility(Portable));
                    break;
                case Cup:
                    return_list.Add(new Archetype(Cup));
                    return_list.Add(new NameComponent());
                    return_list.Add(new Location());
                    return_list.Add(new CapacityComponent(5));
                    return_list.Add(new Mobility(Portable));
                    break;
                case Kettle:
                    return_list.Add(new Archetype(Kettle));
                    return_list.Add(new NameComponent("an iron kettle"));
                    return_list.Add(new BrewCapable());
                    return_list.Add(new Location());
                    return_list.Add(new CapacityComponent(20));
                    return_list.Add(new Mobility(Portable));
                    break;
                case Bucket:
                    return_list.Add(new Archetype(Bucket));
                    return_list.Add(new NameComponent("a wooden bucket"));
                    return_list.Add(new Location());
                    return_list.Add(new CapacityComponent(100));
                    return_list.Add(new Mobility(Portable));
                    break;
                case Well:
                    return_list.Add(new Archetype(Well));
                    return_list.Add(new NameComponent("a stone well"));
                    return_list.Add(new Location());
                    return_list.Add(new CapacityComponent(1000));
                    return_list.Add(new Mobility(Immovable));
                    break;
                case Tea:
                    return_list.Add(new Archetype(Tea));
                    return_list.Add(new NameComponent());
                    return_list.Add(new Consumable(Potable));
                    return_list.Add(new Location());
                    return_list.Add(new Mobility(Portable)); // TODO: liquid system? need to put tea in cup
                    break;
                case Plant:
                    return_list.Add(new Archetype(Plant));
                    return_list.Add(new NameComponent());
                    return_list.Add(new Inventory());
                    return_list.Add(new Growable());
                    return_list.Add(new Location());
                    return_list.Add(new Mobility(Immovable));
                    break;
                case Item:
                    return_list.Add(new Archetype(Item));
                    return_list.Add(new NameComponent());
                    return_list.Add(new Location());
                    return_list.Add(new Mobility(Portable));
                    break;
                default:
                    break;
            }
            return return_list;
        }
        public static XY EntityLocation(Entity entity)
        {
            XY return_location = new XY();
            var location = ComponentOfEntityByType<Location>(entity);
            location
                .Map(l => l.Coordinates)
                .MatchSome(c => return_location = c);
            return return_location;
        }
        public static List<Entity> EntitiesAtLocation(XY target_location)
        {
            List<Entity> return_list = 
                AllComponentsOfType<Location>()
                .FindAll(c => c.Coordinates.Equals(target_location))
                .Select(c => Lookup.EntityById(c.EntityID)).ToList();
            return return_list;
        }
        public static List<XY> NearbyRooms(XY room)
        {
            List<XY> nearby_rooms = new List<XY>
            {
                new XY(room.xValue + 1, room.yValue),
                new XY(room.xValue, room.yValue + 1),
                new XY(room.xValue - 1, room.yValue),
                new XY(room.xValue, room.yValue - 1)
            };
            foreach (XY nearby_room in nearby_rooms)
            {
                if (nearby_room.xValue > 5 || nearby_room.xValue < -5 || nearby_room.yValue > 5 || nearby_room.yValue < -5)
                {
                    nearby_rooms.Remove(room);
                }
            }
            return nearby_rooms;
        }
        public static string EntityName(Entity entity)
        {
            string display_name = "(no)";
            var name = Lookup.ComponentOfEntityByType<NameComponent>(entity);
                name
                    .Map(c => c.Name)
                    .MatchSome(n => display_name = n);
            return display_name;
        }
        public static List<IComponent> AllComponentsOfEntity(Entity entity)
        {
            return entity.ComponentList;
        }
        public static Option<T> ComponentOfEntityByType<T>(Entity entity) where T : IComponent
        {
            var component_list = AllComponentsOfEntity(entity);
            var return_component = Option.None<T>();

            foreach (IComponent component in component_list)
            {
                if (component is T typed_component)
                {
                    return_component = Option.Some(typed_component);
                }
            }
            return return_component;
        }
        public static Entity EntityById(int entity_id)
        {
            // This will fail if the Entity doesn't exist. This is good, we shouldn't be calling entities that don't exist

            return EntityManager.GetAllEntities()[entity_id];
        }
        public static List<Entity> EntityInventory(Entity entity)
        {
            var inventory = Lookup.ComponentOfEntityByType<Inventory>(entity);
            List<Entity> return_list = new List<Entity>();
            inventory
                .Map(inv => inv.InventoryList)
	            .MatchSome(list => return_list = list);
            return return_list;
        }
        public static List<T> AllComponentsOfType<T>() where T : IComponent
        {
            List<T> component_list = new List<T>();
            foreach (KeyValuePair<int, Entity> entity in EntityManager.GetAllEntities())
            {
                foreach (IComponent component in entity.Value.ComponentList)
                {
                    if (component is T)
                    component_list.Add((T)component);
                }
            }
            return component_list;
        }
    }
}


// TODO NOTES
// components have no methods        | no behavior
// systems have no fields/properties | no state
// systems can't call other systems
// systems iterate in an order on Tick()
//  therefore all systems require a Tick() function?
// consider the singleton component? for system state
// one state entity that carries all singleton components
// shared code lives in utils?
// complex side effects should be deferred?

// overwatch:
// entityadmin(entitymanager) has
// - array of systems 
// - hashmap of entityID to entity
// - an array of components (for storing the singletons?)
// each entity has 
// - an array of components
// - an ID
// all systems have Update() and NotifyComponent(component)
// update runs on timeframes, entityadmin iterates over a list of systems and calls their update functions
