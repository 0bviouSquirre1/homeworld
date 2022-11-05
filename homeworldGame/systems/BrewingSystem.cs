namespace homeworld
{
    public static class BrewingSystem
    {
        public static string BrewTea(Entity entity, Entity produce, Entity container)
        {
            var brew_component = Lookup.ComponentOfEntityByType<Brewable>(produce);
            var brewing_component = Lookup.ComponentOfEntityByType<BrewCapable>(container);
            bool brew = brew_component.HasValue;
            bool brewing = brewing_component.HasValue;
            string liquid = "nothing";

            if(brew && brewing)
            {
                InventorySystem.RemoveFromInventory(entity, produce);
                liquid = $"{Lookup.EntityName(produce)} tea";
                BrewingSystem.SetLiquid(container, liquid);
                // NameSystem.ChangeName(tea, name);
                // Entity tea = EntityManager.CreateEntity(Archetype.States.Tea, new XY(99,99));
            }
            return liquid;
        }
        public static void SetLiquid(Entity container, string liquid)
        {
            var brewing_component = Lookup.ComponentOfEntityByType<CapacityComponent>(container);
            brewing_component.Map(bc => bc.Liquid = liquid);
        }
        public static string GetLiquid(Entity container)
        {
            string liquid = "nothing";
            var brewing_component = Lookup.ComponentOfEntityByType<CapacityComponent>(container);
            brewing_component.Map(bc => liquid = bc.Liquid);
            return liquid;
        }
    }
}