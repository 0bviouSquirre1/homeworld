namespace homeworld
{
    public static class Intake
    {
        public static void Consume(Entity entity, Entity consumable)
        {
            if (entity.Inventory().Contains(consumable))
            {
                InventorySystem.RemoveFromInventory(entity, consumable);

                var consume_component = Lookup.ComponentOfEntityByType<Consumable>(consumable);
                consume_component
                    .Map(cc => cc.State)
                    .MatchSome(state => Display.ConsumedItem(entity, consumable, state));
            }
        }
    }
}