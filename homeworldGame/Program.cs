namespace homeworld
{
    public class Program
    {
        public static void Main()
        {
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= 5; j++)
                {
                    EntityManager.CreateEntity($"Jod{i}{j}", new XYComponent(i,j));
                }
            }
            Display.AllEntities();
        }
    }
}