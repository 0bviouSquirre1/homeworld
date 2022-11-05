namespace homeworld
{
    public static class LiquidSystem
    {
        public static void Transfer(Entity from_container, int volume, Entity? to_container = null)
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

                if (volume + to_contents > to_capacity)
                {
                    LiquidSystem.Fill(to_container);
                }
                else
                {
                    LiquidSystem.SetContents(to_container, to_contents + volume);
                }
            }
        }
        public static void Empty(Entity container)
        {
            LiquidSystem.SetContents(container, 0);
        }
        public static void Fill(Entity container)
        {
            var capacity = LiquidSystem.GetCapacity(container);
            var contents = LiquidSystem.GetContents(container);
            LiquidSystem.SetContents(container, capacity);
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