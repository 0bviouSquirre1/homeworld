using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;

namespace homeworld
{
    public static class Movement
    {
        public static void UpdateEntityLocation(int entity_id, XY location)
        {
            XY leaving_location = Lookup.EntityLocation(entity_id);
            Entity entity       = Lookup.EntityById(entity_id);
            var entry           = new Dictionary<int, Entity>()
            {
                { entity_id, entity }
            };

            // Remove them from the old location
            Lookup.EntitiesByLocation[leaving_location].Remove(entity_id);

            // Add them to the new location
            if (Lookup.EntitiesByLocation.ContainsKey(location))
                Lookup.EntitiesByLocation[location].Add(entity.EntityID, entity);
            else
                Lookup.EntitiesByLocation.Add(location, entry);
        }
        public static void MovePlayer(XY nextLocation)
        {
            UpdateEntityLocation(1, nextLocation);
            Map.ExploreRoom(nextLocation);
        }
    }
}