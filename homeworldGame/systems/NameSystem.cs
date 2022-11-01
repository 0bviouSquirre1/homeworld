namespace homeworld
{
    public static class NameSystem
    {
        public static void ChangeName(int entity_id, string name)
        {
            NameComponent? name_component = Lookup.ComponentOfEntityByType<NameComponent>(entity_id);
            name_component!.Name = name;
        }
    }
}