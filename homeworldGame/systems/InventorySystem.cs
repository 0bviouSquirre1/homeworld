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
        public static bool EntityInventoryContains(int entity_id, Entity item)
        {
            List<Entity> inventory = Lookup.EntityById(entity_id).Inventory();
            return inventory.Contains(item);
        }
        public static void DropItem(int entity_id, Entity item)
        {
            if (EntityInventoryContains(entity_id, item))
            {
                List<Entity> inventory = Lookup.EntityById(entity_id).Inventory();
                inventory.Remove(item);

                XY here = Lookup.EntityLocation(entity_id);
                List<int> room_inventory = Lookup.EntitiesAtLocation(here);
                room_inventory.Add(item.EntityID);
            }
        }
        public static void GetItem(int entity_id, Entity item)
        {
            List<int> entity_list = Lookup.EntitiesAtLocation(Lookup.EntityLocation(entity_id));
            if (entity_list.Contains(item.EntityID))
            {
                entity_list.Remove(item.EntityID);

                Lookup.EntityById(entity_id).Inventory().Add(item);
            }
        }
    }
}