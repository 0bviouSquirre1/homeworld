using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;

namespace homeworld
{
    public static class Create
    {
        public static void PopulateWorld()
        {
            Create.Player(new XY(1,1));  // id: 1
            Create.Well(new XY(3,3));    // id: 2
            Create.Bucket(new XY(-2,4)); // id: 3
            Create.Kettle(new XY(1,1));  // id: 4

            // TODO: more sophisticated name generation
            Create.RandomPlants("tomato");
            Create.RandomPlants("mint");
            Create.RandomPlants("thyme");
            Create.RandomPlants("sunflower");
            Create.RandomPlants("nightshade");

            Create.RandomItems("a teacup");
            Create.RandomItems("a silver spoon");
            Create.RandomItems("a saucer");
        }
        public static Entity Player(XY location)
        {
            Entity player = EntityManager.CreateEntity(Archetype.States.Player, "player", location, Mobility.States.Movable);
            return player;
        }
        public static Entity Well(XY location)
        {
            Entity well = EntityManager.CreateEntity(Archetype.States.Well, "a stone well", location, Mobility.States.Immovable);
            return well;
        }
        public static Entity Bucket(XY location)
        {
            Entity bucket = EntityManager.CreateEntity(Archetype.States.Bucket, "a wooden bucket", location, Mobility.States.Portable);
            return bucket;
        }
        public static Entity Kettle(XY location)
        {
            Entity kettle = EntityManager.CreateEntity(Archetype.States.Kettle, "an iron kettle", location, Mobility.States.Portable);
            return kettle;
        }
        public static Entity Plant(string name, XY location)
        {
            string plant_name = $"a {name} plant";
            Entity plant = EntityManager.CreateEntity(Archetype.States.Plant, plant_name, location, Mobility.States.Immovable);
            GrowSystem.SetProduce(plant.EntityID, name);
            return plant;
        }
        public static Entity Item(string name, XY location)
        {
            Entity item;
            if (name.Equals("a teacup"))
            {
                item = EntityManager.CreateEntity(Cup, name, location, Mobility.States.Portable);
            }
            else
            {
                item = EntityManager.CreateEntity(Archetype.States.Item, name, location, Mobility.States.Portable);
            }
            NameSystem.ChangeName(item.EntityID, name);
            return item;
        }
        public static void RandomPlants(string name)
        {
            for (int i = 0; i <= 2; i++)
            {
                XY location = XY.RandomLocation();
                var plant = Plant(name, location);
            }
        }
        public static void WalkThePlayerAround(XY player_location, int steps)
        {
            for (int i = 0; i <= steps; i++)
            {
                var rand = new Random();
                List<XY> nearby_rooms = Lookup.NearbyRooms(player_location);
                XY next_location = nearby_rooms[rand.Next(4)];
                player_location = next_location;
                Movement.MovePlayer(next_location);
            }
        }
        public static void RandomItems(string name)
        {
            for (int i = 0; i <= 2; i++)
            {
                XY location = XY.RandomLocation();
                Item(name, location);
            }
        }
    }
}