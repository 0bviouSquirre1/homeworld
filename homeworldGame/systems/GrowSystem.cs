using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;
using Optional;

namespace homeworld
{
    public static class GrowSystem
    {
        public static void SetProduce(int plant_id, string name)
        {
            Entity produce = CreateProduce(plant_id, name);
            var grow_component = Lookup.ComponentOfEntityByType<Growable>(plant_id);
            grow_component
                .MatchSome(c => c.ProduceID = produce.EntityID);
            InventorySystem.AddToInventory(plant_id, produce);
        }
        public static Entity CreateProduce(int plant_id, string name)
        {
            Entity produce = EntityManager.CreateEntity(Produce, new XY(99,99));
            NameSystem.ChangeName(produce.EntityID, name);
            return produce;
        }
        public static void Grow(int plant_id, string name)
        {
            // TODO: grow more tomatoes
            // check plant's inventory by plant_id for fullness
            // may need to set a max size for plant inventories
            Entity produce = CreateProduce(plant_id, name);
            InventorySystem.AddToInventory(plant_id, produce);
            // if less than full, add one produce
            // if full, do nothing
        }
    }
}