using System;
using static homeworld.Mobility.States;
using static homeworld.Consumable.States;

namespace homeworld
{
    public class Program
    {
        public static void Main()
        {
            var a = new XYComponent(1,1);
            var b = new XYComponent(1,1);
            bool truth = (a.Equals(b));
            Console.WriteLine(truth);

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

            CreateWell(new XYComponent(3,3));
            CreateBucket(new XYComponent(-2,4));
            CreateKettle(new XYComponent(1,1));

            Display.OverheadMap();

            // TODO: move player around and see if map updates appropriately

            // CreateABunchOfEntities();
            // KillABunchOFEntities();
            // Display.AllEntities();
            // Display.AllComponentsOfEntity(1);
            // Display.AllEntitiesWithComponentType<Inventory>();
            // Display.AllComponentsOfType<MobilityComponent>();
        }

        public static void CreateABunchOfEntities()
        {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    List<IComponent> components = new List<IComponent>
                    {
                        new NameComponent($"Jod{i}{j}"),
                        new XYComponent(i,j)
                    };
                    EntityManager.CreateEntity(components);
                }
            }
        }
        public static void KillABunchOFEntities()
        {
            var random = new Random();
            for (int i = 0; i <= 10; i++)
            {
                int kill = random.Next(1,EntityManager.AllEntities.Count);
                EntityManager.DestroyEntity(kill);
            }
        }
        public static void CreatePlayer()
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("player"),
                new XYComponent(1,1),
                new Inventory(),
                new Mobility(Movable)
            };
            int player = EntityManager.CreateEntity(components);
        }
        public static void CreateRandomPlants(string name, Func<int> CreateProduce)
        {
            for (int i = 0; i <= 4; i++)
            {
                XYComponent location = XYComponent.RandomLocation();
                CreatePlant(name, location, CreateProduce);
            }
        }
        public static void CreateRandomItems(string name)
        {
            for (int i = 0; i <= 2; i++)
            {
                XYComponent location = XYComponent.RandomLocation();
                CreateItem(name, location);
            }
        }
        public static int CreatePlant(string name, XYComponent location, Func<int> CreateProduce)
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent($"a {name} plant"),
                location,
                new Inventory(),
                new Mobility(Immovable),
                new Growable(CreateProduce())
            };
            int plant = EntityManager.CreateEntity(components);
            return plant;
        }
        public static int CreateItem(string name, XYComponent location)
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent(name),
                location,
                new Mobility(Portable),
            };

            if (name.Equals("a teacup"))
            {
                components.Add(new Drinkable());
                components.Add(new Fillable());
                components.Add(new Emptyable());
            }

            int item = EntityManager.CreateEntity(components);
            return item;
        }
        public static int CreateWell(XYComponent location)
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a stone well"),
                location,
                new Mobility(Immovable),
                new Drinkable(),
                new Fillable()
            };
            int well = EntityManager.CreateEntity(components);
            return well;
        }
        public static int CreateBucket(XYComponent location)
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a wooden bucket"),
                location,
                new Mobility(Portable),
                new Drinkable(),
                new Fillable(),
                new Emptyable()
            };
            int bucket = EntityManager.CreateEntity(components);
            return bucket;
        }
        public static int CreateKettle(XYComponent location)
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("an iron kettle"),
                location,
                new Mobility(Portable),
                new Drinkable(),
                new Fillable(),
                new Emptyable(),
                new BrewCapable()
            };
            int kettle = EntityManager.CreateEntity(components);
            return kettle;
        }
        public static int CreateTomato()
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a tomato"),
                new Mobility(Portable),
                new Brewable(),
                new Consumable(Edible)
            };
            int tomato = EntityManager.CreateEntity(components);
            return tomato;
        }
        public static int CreateMint()
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a sprig of mint"),
                new Mobility(Portable),
                new Brewable(),
                new Consumable(Edible)
            };
            int mint = EntityManager.CreateEntity(components);
            return mint;
        }
        public static int CreateThyme()
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a sprig of thyme"),
                new Mobility(Portable),
                new Brewable(),
                new Consumable(Edible)
            };
            int thyme = EntityManager.CreateEntity(components);
            return thyme;
        }
        public static int CreateSunflower()
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a a sunflower"),
                new Mobility(Portable),
                new Brewable(),
                new Consumable(Edible)
            };
            int sunflower = EntityManager.CreateEntity(components);
            return sunflower;
        }
        public static int CreateNightshade()
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent("a handful of nightshade leaves"),
                new Mobility(Portable),
                new Brewable(),
                new Consumable(Edible)
            };
            int nightshade = EntityManager.CreateEntity(components);
            return nightshade;
        }
    }
}