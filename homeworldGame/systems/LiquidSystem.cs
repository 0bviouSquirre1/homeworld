namespace homeworld
{
    public static class LiquidSystem
    {
        public static void Transfer(Entity from_container, int volume, string liquid, Entity? to_container = null)
        {
            var from_capacity = LiquidSystem.GetCapacity(from_container);
            var from_contents = LiquidSystem.GetContents(from_container);

            if (volume >= from_contents)
            {
                LiquidSystem.Empty(from_container);
            }
            else
            {
                LiquidSystem.SetContents(from_container, from_contents - volume);
            }

            if (to_container is not null)
            {
                var to_capacity = LiquidSystem.GetCapacity(to_container);
                var to_contents = LiquidSystem.GetContents(to_container);

                if (volume + to_contents >= to_capacity)
                {
                    LiquidSystem.Fill(to_container, liquid);
                }
                else
                {
                    LiquidSystem.SetContents(to_container, to_contents + volume);
                }
                BrewingSystem.SetLiquid(to_container, liquid);
                // Add consumable(potable) component
                EntityManager.AddComponent<Consumable>(to_container);
                Intake.SetState(to_container, Consumable.States.Potable);
            }
        }
        public static void Empty(Entity container)
        {
            LiquidSystem.SetContents(container, 0);
            Intake.SetState(container, Consumable.States.None);
        }
        public static void Fill(Entity to_container, string liquid, Entity? from_container = null)
        {
            var to_capacity = LiquidSystem.GetCapacity(to_container);
            var to_contents = LiquidSystem.GetContents(to_container);

            if (from_container is not null)
            {
                liquid = BrewingSystem.GetLiquid(from_container);
                var from_capacity = LiquidSystem.GetCapacity(from_container);
                var from_contents = LiquidSystem.GetContents(from_container);

                LiquidSystem.SetContents(from_container, from_contents - (to_capacity - to_contents));
            }

            LiquidSystem.SetContents(to_container, to_capacity);
            BrewingSystem.SetLiquid(to_container, liquid);
            Intake.SetState(to_container, Consumable.States.Potable);
        }
        public static void SetContents(Entity container, int volume)
        {
            var capacity_component = Lookup.ComponentOfEntityByType<CapacityComponent>(container);
            capacity_component.MatchSome(cc => cc.Contents = volume);
        }
        public static int GetCapacity(Entity container)
        {
            int capacity = 0;
            var capacity_component = Lookup.ComponentOfEntityByType<CapacityComponent>(container);
            capacity_component.MatchSome(cc => capacity = cc.Capacity);
            return capacity;
        }
        public static int GetContents(Entity container)
        {
            int contents = 0;
            var capacity_component = Lookup.ComponentOfEntityByType<CapacityComponent>(container);
            capacity_component.MatchSome(cc => contents = cc.Contents);
            return contents;
        }
    }
}