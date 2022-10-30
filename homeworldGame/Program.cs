using System;
using static homeworld.Mobility.States;
using static homeworld.Consumable.States;

namespace homeworld
{
    public class Program
    {
        public static void Main()
        {
            // TODO: Lookup class? Lookup.EntityByID, .EntityByComponentID, .ComponentsByEntity, ComponentOfTypeByEntity, etc
            Map.Setup();
            CreatePlayer();

            Setup.CreateRandomPlants("tomato");
            Setup.CreateRandomPlants("mint");
            Setup.CreateRandomPlants("thyme");
            Setup.CreateRandomPlants("sunflower");
            Setup.CreateRandomPlants("nightshade");

            Setup.CreateRandomItems("a teacup");
            Setup.CreateRandomItems("a silver spoon");
            Setup.CreateRandomItems("a saucer");

            CreateWell(new XY(3,3));
            CreateBucket(new XY(-2,4));
            CreateKettle(new XY(1,1));

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
        public static int CreatePlant(string name, XY location)
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent($"a {name} plant"),
                new Inventory(),
                new Growable(CreateProduce(name))
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
        public static int CreateProduce(string name)
        {
            List<IComponent> components = new List<IComponent>
            {
                new NameComponent(name),
                new Brewable(),
                new Consumable(Edible)
            };
            int produce = EntityManager.CreateEntity(components);
            EntityManager.AddComponentToEntity(produce, new Mobility(produce, Portable, new XY(99,99))); // Added Mobility after the fact to account for needing the entity's ID
            // 99,99 counts as an error value for now, for things that are not in the world. this will have to be fixed asap
            return produce;
        }
    }
}