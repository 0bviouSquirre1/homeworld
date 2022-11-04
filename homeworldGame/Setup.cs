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
            Entity player = EntityManager.CreateEntity(Archetype.States.Player, location);
            return player;
        }
        public static Entity Well(XY location)
        {
            Entity well = EntityManager.CreateEntity(Archetype.States.Well, location);
            return well;
        }
        public static Entity Bucket(XY location)
        {
            Entity bucket = EntityManager.CreateEntity(Archetype.States.Bucket, location);
            return bucket;
        }
        public static Entity Kettle(XY location)
        {
            Entity kettle = EntityManager.CreateEntity(Archetype.States.Kettle, location);
            return kettle;
        }
        public static Entity Item(string name, XY location)
        {
            Entity item;
            if (name.Equals("a teacup"))
            {
                item = EntityManager.CreateEntity(Cup, location);
            }
            else
            {
                item = EntityManager.CreateEntity(Archetype.States.Item, location);
            }
            NameSystem.ChangeName(item, name);
            return item;
        }
        public static void RandomPlants(string name)
        {
            for (int i = 0; i <= 2; i++)
            {
                XY location = XY.RandomLocation();
                var plant = GrowSystem.CreatePlant(name, location);
            }
        }
        public static void WalkThePlayerAround(Entity player, XY player_location, int steps)
        {
            for (int i = 0; i <= steps; i++)
            {
                var rand = new Random();
                List<XY> nearby_rooms = Lookup.NearbyRooms(player_location);
                XY next_location = nearby_rooms[rand.Next(4)];
                player_location = next_location;
                Movement.MovePlayer(player, next_location);
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