using System;
using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;

namespace homeworld
{
    public class Program
    {
        public static void Main()
        {
            Map.Setup();
            CreatePlayer();             // id: 1
            CreateWell(new XY(3,3));    // id: 2
            CreateBucket(new XY(-2,4)); // id: 3
            CreateKettle(new XY(1,1));  // id: 4

            Setup.CreateRandomPlants("tomato");
            Setup.CreateRandomPlants("mint");
            Setup.CreateRandomPlants("thyme");
            Setup.CreateRandomPlants("sunflower");
            Setup.CreateRandomPlants("nightshade");

            Setup.CreateRandomItems("a teacup");
            Setup.CreateRandomItems("a silver spoon");
            Setup.CreateRandomItems("a saucer");
            
            //Display.AllEntities();

            Display.OverheadMap();
            /*Console.WriteLine();

            Movement.MovePlayer(new XY(1,2));
            Movement.MovePlayer(new XY(2,2));
            Movement.MovePlayer(new XY(2,3));
            Movement.MovePlayer(new XY(3,3));
            Movement.MovePlayer(new XY(3,4));
            Movement.MovePlayer(new XY(3,3));
            Movement.MovePlayer(new XY(3,2));

            Display.OverheadMap();*/
        }
        public static int CreatePlayer()
        {
            int player = EntityManager.CreateEntity(Player);
            return player;
        }
        public static int CreatePlant(string name, XY location)
        {
            int plant = EntityManager.CreateEntity(Plant);
            NameComponent name_component = Lookup.ComponentOfEntityByType<NameComponent>(plant);
            name_component.Name = name;
            Movement.RegisterEntityAtLocation(plant, location);
            //Movement.UpdateEntityLocation(plant, location);
            
            return plant;
        }
        public static int CreateItem(string name, XY location)
        {
            int item;
            if (name.Equals("a teacup"))
            {
                item = EntityManager.CreateEntity(Cup);
            }
            else
            {
                item = EntityManager.CreateEntity(Item);
            }
            NameComponent name_component = Lookup.ComponentOfEntityByType<NameComponent>(item);
            name_component.Name = name;
            Movement.UpdateEntityLocation(item, location);
            return item;
        }
        public static int CreateWell(XY location)
        {
            int well = EntityManager.CreateEntity(Well);
            Movement.RegisterEntityAtLocation(well, location);
            //Movement.UpdateEntityLocation(well, location);
            return well;
        }
        public static int CreateBucket(XY location)
        {
            int bucket = EntityManager.CreateEntity(Bucket);
            Movement.RegisterEntityAtLocation(bucket, location);
            //Movement.UpdateEntityLocation(bucket, location);
            return bucket;
        }
        public static int CreateKettle(XY location)
        {
            int kettle = EntityManager.CreateEntity(Kettle);
            Movement.RegisterEntityAtLocation(kettle, location);
            //Movement.UpdateEntityLocation(kettle, location);
            return kettle;
        }
        public static int CreateProduce(string name)
        {
            int produce = EntityManager.CreateEntity(Produce);
            NameComponent name_component = Lookup.ComponentOfEntityByType<NameComponent>(produce);
            name_component.Name = name;
            return produce;
        }
    }
}