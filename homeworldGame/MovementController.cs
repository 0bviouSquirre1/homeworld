using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;

namespace homeworld
{
    public static class Movement
    {
        public static void UpdateEntityLocation(int entity_id, XY next_location)
        {
            Lookup.MovableEntities[entity_id].Location = next_location;
        }
        public static void MovePlayer(XY nextLocation)
        {
            UpdateEntityLocation(1, nextLocation);
            Map.ExploreRoom(nextLocation);
        }
        public static void RegisterEntityAtLocation(int entity_id, XY location)
        {
            Lookup.EntitiesByLocation.TryGetValue(location, out List<int>? present_entities);

            if (present_entities is null)
            {
                present_entities = new List<int>();
                Lookup.EntitiesByLocation.Add(location, present_entities);
            }

            present_entities.Add(entity_id);

            Mobility? component = Lookup.ComponentOfEntityByType<Mobility>(entity_id);

            if (component is not null)
            {
                if (component.State == Movable)
                {
                    Lookup.MovableEntities.Add(entity_id, component);
                }
            }
        }
    }
}