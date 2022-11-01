using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;

namespace homeworld
{
    public static class Lookup
    {
        public static Dictionary<int, Entity> AllEntities = new Dictionary<int, Entity>();
        public static Dictionary<XY, Dictionary<int, Entity>> EntitiesByLocation = new Dictionary<XY, Dictionary<int, Entity>>();
        public static Dictionary<XY, bool>               ExploredMap = new Dictionary<XY, bool>();              // Location and Visibility

        public static List<IComponent> ArchetypeComponents(Archetype.States archetype)
        {
            List<IComponent> return_list = new List<IComponent>();
            switch (archetype)
            {
                case None:
                    return_list.Add(new Archetype(None));
                    return_list.Add(new NameComponent());
                    break;
                case Player:
                    return_list.Add(new Archetype(Player));
                    return_list.Add(new NameComponent("player"));
                    return_list.Add(new Inventory());
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
                    return_list.Add(new Drinkable());
                    return_list.Add(new Fillable());
                    return_list.Add(new Emptyable());
                    return_list.Add(new Mobility(Portable));
                    break;
                case Kettle:
                    return_list.Add(new Archetype(Kettle));
                    return_list.Add(new NameComponent("an iron kettle"));
                    return_list.Add(new Drinkable());
                    return_list.Add(new Fillable());
                    return_list.Add(new Emptyable());
                    return_list.Add(new BrewCapable());
                    return_list.Add(new Mobility(Portable));
                    break;
                case Bucket:
                    return_list.Add(new Archetype(Bucket));
                    return_list.Add(new NameComponent("a wooden bucket"));
                    return_list.Add(new Drinkable());
                    return_list.Add(new Fillable());
                    return_list.Add(new Emptyable());
                    return_list.Add(new Mobility(Portable));
                    break;
                case Well:
                    return_list.Add(new Archetype(Well));
                    return_list.Add(new NameComponent("a stone well"));
                    return_list.Add(new Drinkable());
                    return_list.Add(new Fillable());
                    return_list.Add(new Mobility(Immovable));
                    break;
                case Tea:
                    // TODO: make tea
                    break;
                case Plant:
                    return_list.Add(new Archetype(Plant));
                    return_list.Add(new NameComponent());
                    return_list.Add(new Inventory());
                    return_list.Add(new Growable());
                    return_list.Add(new Mobility(Immovable));
                    break;
                case Item:
                    return_list.Add(new Archetype(Item));
                    return_list.Add(new NameComponent());
                    return_list.Add(new Mobility(Portable));
                    break;
                default:
                    break;
            }
            return return_list;
        }
        public static XY EntityLocation(int entity_id)
        {
            XY return_location = new XY();
            foreach (KeyValuePair<XY, Dictionary<int, Entity>> location_node in EntitiesByLocation)
            {
                foreach (KeyValuePair<int, Entity> entity_node in location_node.Value)
                {
                    if (entity_node.Key == entity_id)
                    {
                        return_location = location_node.Key;
                    }
                }
            }
            return return_location;
        }
        public static bool LocationIsOccupied(XY location)
        {
            if (EntitiesByLocation.ContainsKey(location) && EntitiesByLocation[location].Count != 0)
                return true;
            return false;
        }
        public static List<IComponent> AllComponentsOfEntity(int entity_id)
        {
            return Lookup.AllEntities[entity_id].ComponentList;
        }
        public static T? ComponentOfEntityByType<T>(int entity_id) where T : IComponent
        {
            var component_list = AllComponentsOfEntity(entity_id);
            foreach (IComponent component in component_list)
            {
                if (component is T)
                {
                    T return_component = (T)component;
                    return return_component;
                }
            }
            return default(T);
        }
        public static Entity EntityById(int entity_id)
        {
            return AllEntities[entity_id];
        }

        /*
        public static List<int> AllEntitiesWithComponentType<T>()
        {
            List<int> entity_list = new List<int>();

            foreach (KeyValuePair<int, List<IComponent>> entity in EntitiesAndComponents)
            {
                foreach (IComponent component in entity.Value)
                {
                    if (component is T)
                    {
                        entity_list.Add(component.EntityID);
                    }
                }
            }
            return entity_list;
        }
        public static List<T> AllComponentsOfType<T>() where T : IComponent
        {
            List<T> component_list = new List<T>();

            foreach (KeyValuePair<int, List<IComponent>> entity in EntitiesAndComponents)
            {
                foreach (IComponent component in entity.Value)
                {
                    if (component is T)
                    {
                        component_list.Add((T)component);
                    }
                }
            }
            return component_list;
        }
        */
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
