using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;

namespace homeworld
{
    public static class Setup
    {
        public static Entity CreatePlayer(XY location)
        {
            Entity player = EntityManager.CreateEntity(Player, location);
            return player;
        }
        public static Entity CreateWell(XY location)
        {
            Entity well = EntityManager.CreateEntity(Well, location);
            return well;
        }
        public static Entity CreateBucket(XY location)
        {
            Entity bucket = EntityManager.CreateEntity(Bucket, location);
            return bucket;
        }
        public static Entity CreateKettle(XY location)
        {
            Entity kettle = EntityManager.CreateEntity(Kettle, location);
            return kettle;
        }
        public static Entity CreatePlant(string name, XY location)
        {
            Entity plant = EntityManager.CreateEntity(Plant, location);
            string plant_name = $"a {name} plant";
            NameSystem.ChangeName(plant.EntityID, plant_name);
            GrowSystem.SetProduce(plant.EntityID, name);
            return plant;
        }
        public static Entity CreateItem(string name, XY location)
        {
            Entity item;
            if (name.Equals("a teacup"))
            {
                item = EntityManager.CreateEntity(Cup, location);
            }
            else
            {
                item = EntityManager.CreateEntity(Item, location);
            }
            NameSystem.ChangeName(item.EntityID, name);
            return item;
        }
        public static void CreateRandomPlants(string name)
        {
            for (int i = 0; i <= 2; i++)
            {
                XY location = XY.RandomLocation();
                CreatePlant(name, location);
            }
        }
        public static void CreateRandomItems(string name)
        {
            for (int i = 0; i <= 2; i++)
            {
                XY location = XY.RandomLocation();
                CreateItem(name, location);
            }
        }
    }
}