using Optional;

namespace homeworld
{
    public static class Movement
    {
        public static void UpdateEntityLocation(Entity entity, XY location)
        {
            Option<Location> location_component = Lookup.ComponentOfEntityByType<Location>(entity);
            location_component.MatchSome(lc => lc.Coordinates = location);
        }
        public static void MovePlayer(Entity player, XY next_location)
        {
            XY previous_location = Lookup.EntityLocation(player);
            Display.PlayerMoved(previous_location, next_location);
            UpdateEntityLocation(player, next_location);
            Map.ExploreRoom(next_location);
            Display.EntitiesAtLocation(next_location);
        }
    }
}