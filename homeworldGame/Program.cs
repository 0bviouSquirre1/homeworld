namespace homeworld
{
    public class Program
    {
        public static void Main()
        {
            CreateABunchOfEntities();
            KillABunchOFEntities();
            Display.AllEntities();
            Display.AllComponentsOfEntity(027);
            NameComponent name = new NameComponent("fake");
            Type type = name.GetType();
            Display.AllComponentsOfType(type);
        }

        public static void CreateABunchOfEntities()
        {
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= 5; j++)
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
    }
}