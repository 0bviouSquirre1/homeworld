namespace homeworld
{
    public static class NameSystem
    {
        public static void ChangeName(int entity_id, string name)
        {
            var name_component = Lookup.ComponentOfEntityByType<NameComponent>(entity_id);
            name_component
                .MatchSome(c => c.Name = name);
        }
    }
}