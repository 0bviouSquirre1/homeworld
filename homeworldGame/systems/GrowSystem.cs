using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;
using Optional;

namespace homeworld
{
    public static class GrowSystem
    {
        public static Entity CreatePlant(string name, XY location)
        {
            Entity plant = EntityManager.CreateEntity(Archetype.States.Plant, location);
            string plant_name = $"a {name} plant";
            NameSystem.ChangeName(plant, plant_name);
            GrowSystem.SetProduce(plant, name);
            return plant;
        }
        public static Entity CreateProduce(Entity plant, string name)
        {
            Entity produce = EntityManager.CreateEntity(Produce, new XY(99,99));
            NameSystem.ChangeName(produce, name);
            return produce;
        }
        public static void SetProduce(Entity plant, string name)
        {
            Entity produce = CreateProduce(plant, name);
            // Set Produce ID
            SetProduceID(plant, produce);
            InventorySystem.AddToInventory(plant, produce);
        }
        public static void SetProduceID(Entity plant, Entity produce)
        {
            var grow_component = Lookup.ComponentOfEntityByType<Growable>(plant);
            grow_component
                .MatchSome(c => c.ProduceID = produce.EntityID);
        }
        public static void Grow(Entity plant, string name)
        {
            if (plant.Inventory().Count <= 5)
            {
                Entity produce = CreateProduce(plant, name);
                InventorySystem.AddToInventory(plant, produce);
            }
        }
        public static void Harvest(Entity entity, Entity plant)
        {
            // get plant produce
            Entity produce = CreateProduce(plant, "(no)");
            var grow_component = Lookup.ComponentOfEntityByType<Growable>(plant);
            grow_component
                .Map(gc => gc.ProduceID)
                .MatchSome(prod => produce = Lookup.EntityById(prod));

            // check plant for presence of produce
            if (plant.Inventory().Count > 0)
            {
                InventorySystem.RemoveFromInventory(plant, produce);
                InventorySystem.AddToInventory(entity, produce);
            }
        }
    }
}