namespace homeworld
{
    public static class InventorySystem
    {
        public static void AddToInventory(int entity_id, Entity added_item)
        {
            Inventory? inventory = Lookup.ComponentOfEntityByType<Inventory>(entity_id);
            inventory!.InventoryList.Add(added_item);
        }
        // TODO: add check inventory method
    }
}