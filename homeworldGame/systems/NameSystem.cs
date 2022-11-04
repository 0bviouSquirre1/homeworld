namespace homeworld
{
    public static class NameSystem
    {
        public static void ChangeName(Entity entity, string name)
        {
            var name_component = Lookup.ComponentOfEntityByType<NameComponent>(entity);
            name_component
                .MatchSome(c => c.Name = name);
        }
    }
}