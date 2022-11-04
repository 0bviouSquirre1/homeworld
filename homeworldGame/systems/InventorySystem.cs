namespace homeworld
{
    public static class InventorySystem
    {
        public static void AddToInventory(Entity entity, Entity added_item)
        {
            var inventory = Lookup.ComponentOfEntityByType<Inventory>(entity);
            inventory
                .Map(inv => inv.InventoryList)
                .MatchSome(list =>
                    list.Add(added_item));
        }
        public static void RemoveFromInventory(Entity entity, Entity removed_item)
        {
            var inventory = Lookup.ComponentOfEntityByType<Inventory>(entity);
            inventory
                .Map(inv => inv.InventoryList)
                .MatchSome(list =>
                    list.Remove(removed_item));
        }
        public static bool EntityInventoryContains(Entity entity, Entity item)
        {
            bool result = false;
            var list = Lookup.ComponentOfEntityByType<Inventory>(entity);
            list.Map(inv => inv.InventoryList)
                .MatchSome(inv_list => result = inv_list.Contains(item));
            return result;
        }
        public static void DropItem(Entity entity, Entity item)
        {
            if (EntityInventoryContains(entity, item))
            {
                InventorySystem.RemoveFromInventory(entity, item);

                XY here = Lookup.EntityLocation(entity);
                EntityManager.AddComponent<Location>(item);
                Movement.UpdateEntityLocation(item, here);
                Display.DropItem(item);
            }
        }
        public static void GetItem(Entity entity, Entity item)
        {
            EntityManager.RemoveComponent<Location>(item);

            InventorySystem.AddToInventory(entity, item);
            Display.GetItem(item);
        }
    }
}