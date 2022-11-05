namespace homeworld
{
    public static class Intake
    {
        public static void Eat(Entity entity, Entity food)
        {
            if (entity.Inventory().Contains(food))
            {
                InventorySystem.RemoveFromInventory(entity, food);

                var consume_component = Lookup.ComponentOfEntityByType<Consumable>(food);
                consume_component
                    .Map(cc => cc.State)
                    .MatchSome(state => 
                        Display.ConsumedItem(entity, food, state));
            }
        }
        public static void Drink(Entity entity, Entity drink)
        {
            LiquidSystem.SetContents(drink, LiquidSystem.GetContents(drink) - 1);
            Display.ConsumeDrink(entity, drink);
        }
        public static void SetState(Entity consumable, Consumable.States state)
        {
            var consume_component = Lookup.ComponentOfEntityByType<Consumable>(consumable);
                consume_component
                    .MatchSome(cc => cc.State = state);
        }
    }
}