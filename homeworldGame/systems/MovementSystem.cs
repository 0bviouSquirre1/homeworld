using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;
using Optional;

namespace homeworld
{
    public static class Movement
    {
        public static void UpdateEntityLocation(int entity_id, XY location)
        {
            Option<Location> location_component = Lookup.ComponentOfEntityByType<Location>(entity_id);
            location_component.MatchSome(lc => lc.Coordinates = location);
        }
        public static void MovePlayer(XY next_location)
        {
            XY previous_location = Lookup.EntityLocation(1);
            Display.PlayerMoved(previous_location, next_location);
            UpdateEntityLocation(1, next_location);
            Map.ExploreRoom(next_location);
            Display.EntitiesAtLocation(next_location);
        }
    }
}