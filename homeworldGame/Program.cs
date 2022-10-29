namespace homeworld
{
    public class Program
    {
        public static void Main()
        {
            CreatePlayer();
            CreatePlants();
            CreateABunchOfEntities();
            // KillABunchOFEntities();

            Display.AllEntities();
            Display.AllComponentsOfEntity(1);

            Display.AllEntitiesWithComponentType<Inventory>();
            Display.AllComponentsOfType<MobilityComponent>();
        }

        public static void CreateABunchOfEntities()
        {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    EntityManager.CreateEntity($"Jod{i}{j}", new XYComponent(i,j));
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
            Console.WriteLine($"player creator");
            int player = EntityManager.CreateEntity("player", new XYComponent());
            EntityManager.AddComponentToEntity(player, new Inventory());
            EntityManager.AddComponentToEntity(player, new MobilityComponent((MobilityComponent.States)2));
        }
        public static void CreatePlants()
        {
            int plant = EntityManager.CreateEntity("plant", new XYComponent());
            EntityManager.AddComponentToEntity(plant, new Inventory());
            EntityManager.AddComponentToEntity(plant, new MobilityComponent(0));

        }
    }
}