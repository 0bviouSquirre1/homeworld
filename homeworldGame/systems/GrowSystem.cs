using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;
using Optional;

namespace homeworld
{
    public static class GrowSystem
    {
        public static void SetProduce(Entity plant, string name)
        {
            Entity produce = CreateProduce(plant, name);
            var grow_component = Lookup.ComponentOfEntityByType<Growable>(plant);
            grow_component
                .MatchSome(c => c.ProduceID = produce.EntityID);
            InventorySystem.AddToInventory(plant, produce);
        }
        public static Entity CreateProduce(Entity plant, string name)
        {
            Entity produce = EntityManager.CreateEntity(Produce, new XY(99,99));
            NameSystem.ChangeName(produce, name);
            return produce;
        }
        public static void Grow(Entity plant, string name)
        {
            if (plant.Inventory().Count <= 5)
            {
                Entity produce = CreateProduce(plant, name);
                InventorySystem.AddToInventory(plant, produce);
            }
        }
    }
}