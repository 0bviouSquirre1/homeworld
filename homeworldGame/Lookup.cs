using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;

namespace homeworld
{
    public static class Lookup
    {
        public static Dictionary<int, List<IComponent>>  AllEntities = new Dictionary<int, List<IComponent>>(); // Entity # and Component List
        public static Dictionary<int, Mobility>      MovableEntities = new Dictionary<int, Mobility>();         // Entity # and Mobility Component
        public static Dictionary<XY, bool>               ExploredMap = new Dictionary<XY, bool>();              // Location and Visibility
        public static Dictionary<XY, List<int>>   EntitiesByLocation = new Dictionary<XY, List<int>>();         // Location and Entity #s
        public static List<XY>                     OccupiedLocations = EntitiesByLocation.Keys.ToList();        // Extracted list of Locations


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
                    return_list.Add(new Mobility(Movable, new XY(1,1)));
                    break;
                case Produce:
                    return_list.Add(new Archetype(Produce));
                    return_list.Add(new NameComponent());
                    return_list.Add(new Brewable());
                    return_list.Add(new Consumable(Edible));
                    return_list.Add(new Mobility(Portable, new XY(99,99)));
                    break;
                case Cup:
                    return_list.Add(new Archetype(Cup));
                    return_list.Add(new NameComponent());
                    return_list.Add(new Drinkable());
                    return_list.Add(new Fillable());
                    return_list.Add(new Emptyable());
                    return_list.Add(new Mobility(Portable, new XY(99,99)));
                    break;
                case Kettle:
                    return_list.Add(new Archetype(Kettle));
                    return_list.Add(new NameComponent("an iron kettle"));
                    return_list.Add(new Drinkable());
                    return_list.Add(new Fillable());
                    return_list.Add(new Emptyable());
                    return_list.Add(new BrewCapable());
                    return_list.Add(new Mobility(Portable, new XY(99,99)));
                    break;
                case Bucket:
                    return_list.Add(new Archetype(Bucket));
                    return_list.Add(new NameComponent("a wooden bucket"));
                    return_list.Add(new Drinkable());
                    return_list.Add(new Fillable());
                    return_list.Add(new Emptyable());
                    return_list.Add(new Mobility(Portable, new XY(99,99)));
                    break;
                case Well:
                    return_list.Add(new Archetype(Well));
                    return_list.Add(new NameComponent("a stone well"));
                    return_list.Add(new Drinkable());
                    return_list.Add(new Fillable());
                    return_list.Add(new Mobility(Immovable, new XY(99,99)));
                    break;
                case Tea:
                case Plant:
                    return_list.Add(new Archetype(Plant));
                    return_list.Add(new NameComponent());
                    return_list.Add(new Inventory());
                    return_list.Add(new Growable(0));
                    return_list.Add(new Mobility(Immovable, new XY(99,99)));
                    break;
                case Item:
                    return_list.Add(new Archetype(Item));
                    return_list.Add(new NameComponent());
                    return_list.Add(new Mobility(Portable, new XY(99,99)));
                    break;
                default:
                    break;
            }
            return return_list;
        }
        // These might only be useful if I make the lists private, so that you have to go through a method
        // That might be necessary when we start seeing concurrency issues
        public static int EntityByComponent(IComponent component)
        {
            return component.EntityID;
        }
        public static List<int> AllEntitiesWithComponentType<T>()
        {
            List<int> entity_list = new List<int>();

            foreach (KeyValuePair<int, List<IComponent>> entity in AllEntities)
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
        public static List<IComponent> AllComponentsOfEntity(int entity_id)
        {
            return AllEntities[entity_id];
        }
        public static List<T> AllComponentsOfType<T>() where T : IComponent
        {
            List<T> component_list = new List<T>();

            foreach (KeyValuePair<int, List<IComponent>> entity in AllEntities)
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
        public static T? ComponentOfEntityByType<T>(int entity_id) where T : IComponent
        {
            var list = AllEntities[entity_id];

            foreach (IComponent component in list)
            {
                if (component.GetType() is T)
                {
                    T return_component = (T)component;
                }
            }
            return default(T);
        }
        public static XY EntityLocation(int entity_id)
        {
            List<IComponent> component_list = AllComponentsOfEntity(entity_id);
            foreach (IComponent component in component_list)
            {
                if (component.GetType() == typeof(Mobility))
                {
                    Mobility location = (Mobility)component;
                    return location.Location;
                }
            }
            return new XY(99,99); // error code
        }
    }
}
// components have no methods        | no behavior
// systems have no fields/properties | no state
// consider the singleton component? for system state