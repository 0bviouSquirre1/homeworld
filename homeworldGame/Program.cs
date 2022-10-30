using System;
using static homeworld.Mobility.States;
using static homeworld.Consumable.States;

namespace homeworld
{
    public class Program
    {
        public static void Main()
        {
            Map.Setup();
            CreatePlayer();

            CreateRandomPlants("tomato", CreateTomato);
            CreateRandomPlants("mint", CreateMint);
            CreateRandomPlants("thyme", CreateThyme);
            CreateRandomPlants("sunflower", CreateSunflower);
            CreateRandomPlants("nightshade", CreateNightshade);

            CreateRandomItems("a teacup");
            CreateRandomItems("a silver spoon");
            CreateRandomItems("a saucer");

            CreateWell(new XY(3,3));
            CreateBucket(new XY(-2,4));
            CreateKettle(new XY(1,1));

            //Display.AllEntities();

            Display.OverheadMap();
            Console.WriteLine();

            // TODO: move player around and see if map updates appropriately
            Movement.MovePlayer(new XY(1,2));
            Movement.MovePlayer(new XY(2,2));
            Movement.MovePlayer(new XY(2,3));
            Movement.MovePlayer(new XY(3,3));
            Movement.MovePlayer(new XY(3,4));
            Movement.MovePlayer(new XY(3,3));
            Movement.MovePlayer(new XY(3,2));

            Display.OverheadMap();
        }
        public static void CreatePlayer()
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("player"),
                new Inventory(),
                new Mobility(1, Movable, new XY(1,1))
            };
            int player = EntityManager.CreateEntity(components);
        }
        public static void CreateRandomPlants(string name, Func<int> CreateProduce)
        {
            for (int i = 0; i <= 2; i++)
            {
                XY location = XY.RandomLocation();
                CreatePlant(name, location, CreateProduce);
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
        public static int CreatePlant(string name, XY location, Func<int> CreateProduce)
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent($"a {name} plant"),
                new Inventory(),
                new Growable(CreateProduce())
            };
            int plant = EntityManager.CreateEntity(components);
            EntityManager.AddComponentToEntity(plant, new Mobility(plant, Immovable, location)); // Added Mobility after the fact to account for needing the entity's ID
            return plant;
        }
        public static int CreateItem(string name, XY location)
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent(name),
            };

            if (name.Equals("a teacup"))
            {
                components.Add(new Drinkable());
                components.Add(new Fillable());
                components.Add(new Emptyable());
            }

            int item = EntityManager.CreateEntity(components);
            EntityManager.AddComponentToEntity(item, new Mobility(item, Portable, location)); // Added Mobility after the fact to account for needing the entity's ID
            return item;
        }
        public static int CreateWell(XY location)
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a stone well"),
                new Drinkable(),
                new Fillable()
            };
            int well = EntityManager.CreateEntity(components);
            EntityManager.AddComponentToEntity(well, new Mobility(well, Immovable, location)); // Added Mobility after the fact to account for needing the entity's ID
            return well;
        }
        public static int CreateBucket(XY location)
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a wooden bucket"),
                new Drinkable(),
                new Fillable(),
                new Emptyable()
            };
            int bucket = EntityManager.CreateEntity(components);
            EntityManager.AddComponentToEntity(bucket, new Mobility(bucket, Portable, location)); // Added Mobility after the fact to account for needing the entity's ID
            return bucket;
        }
        public static int CreateKettle(XY location)
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("an iron kettle"),
                new Drinkable(),
                new Fillable(),
                new Emptyable(),
                new BrewCapable()
            };
            int kettle = EntityManager.CreateEntity(components);
            EntityManager.AddComponentToEntity(kettle, new Mobility(kettle, Portable, location)); // Added Mobility after the fact to account for needing the entity's ID
            return kettle;
        }
        public static int CreateTomato()
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a tomato"),
                new Brewable(),
                new Consumable(Edible)
            };
            int tomato = EntityManager.CreateEntity(components);
            EntityManager.AddComponentToEntity(tomato, new Mobility(tomato, Portable, new XY(99,99))); // Added Mobility after the fact to account for needing the entity's ID
            // 99,99 counts as an error value for now, for things that are not in the world. this will have to be fixed asap
            return tomato;
        }
        public static int CreateMint()
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a sprig of mint"),
                new Brewable(),
                new Consumable(Edible)
            };
            int mint = EntityManager.CreateEntity(components);
            EntityManager.AddComponentToEntity(mint, new Mobility(mint, Portable, new XY(99,99))); // Added Mobility after the fact to account for needing the entity's ID
            // 99,99 counts as an error value for now, for things that are not in the world. this will have to be fixed asap
            return mint;
        }
        public static int CreateThyme()
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a sprig of thyme"),
                new Brewable(),
                new Consumable(Edible)
            };
            int thyme = EntityManager.CreateEntity(components);
            EntityManager.AddComponentToEntity(thyme, new Mobility(thyme, Portable, new XY(99,99))); // Added Mobility after the fact to account for needing the entity's ID
            // 99,99 counts as an error value for now, for things that are not in the world. this will have to be fixed asap
            return thyme;
        }
        public static int CreateSunflower()
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a a sunflower"),
                new Brewable(),
                new Consumable(Edible)
            };
            int sunflower = EntityManager.CreateEntity(components);
            EntityManager.AddComponentToEntity(sunflower, new Mobility(sunflower, Portable, new XY(99,99))); // Added Mobility after the fact to account for needing the entity's ID
            return sunflower;
        }
        public static int CreateNightshade()
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a handful of nightshade leaves"),
                new Brewable(),
                new Consumable(Edible)
            };
            int nightshade = EntityManager.CreateEntity(components);
            EntityManager.AddComponentToEntity(nightshade, new Mobility(nightshade, Portable, new XY(99,99))); // Added Mobility after the fact to account for needing the entity's ID
            return nightshade;
        }
    }
}