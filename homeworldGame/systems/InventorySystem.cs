namespace homeworld
{
    public static class InventorySystem
    {
        public static void AddToInventory(int entity_id, Entity added_item)
        {
            var inventory = Lookup.ComponentOfEntityByType<Inventory>(entity_id);
            inventory
                .Map(inv => inv.InventoryList)
                .MatchSome(list =>
                    list.Add(added_item));
        }
        public static void RemoveFromInventory(int entity_id, Entity removed_item)
        {
            var inventory = Lookup.ComponentOfEntityByType<Inventory>(entity_id);
            inventory
                .Map(inv => inv.InventoryList)
                .MatchSome(list =>
                    list.Remove(removed_item));
        }
        public static bool EntityInventoryContains(int entity_id, Entity item)
        {
            var entity = Lookup.EntityById(entity_id);
            entity.Map(e => e.Inventory());
            List<Entity> inventory = Lookup.EntityById(entity_id).Inventory();
            return inventory.Contains(item);
        }
        public static void DropItem(int entity_id, Entity item)
        {
            if (EntityInventoryContains(entity_id, item))
            {
                InventorySystem.RemoveFromInventory(entity_id, item);

                XY here = Lookup.EntityLocation(entity_id);
                EntityManager.AddComponent<Location>(item.EntityID);
                Movement.UpdateEntityLocation(item.EntityID, here);
            }
        }
        public static void GetItem(int entity_id, Entity item)
        {
            EntityManager.RemoveComponent<Location>(item.EntityID);

            InventorySystem.AddToInventory(entity_id, item);
        }
    }
}