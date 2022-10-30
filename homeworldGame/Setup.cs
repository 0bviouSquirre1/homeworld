namespace homeworld
{
    public static class Setup
    {
        public static void CreateRandomPlants(string name)
        {
            for (int i = 0; i <= 2; i++)
            {
                XY location = XY.RandomLocation();
                Program.CreatePlant(name, location);
            }
        }
        public static void CreateRandomItems(string name)
        {
            for (int i = 0; i <= 2; i++)
            {
                XY location = XY.RandomLocation();
                Program.CreateItem(name, location);
            }
        }
    }
}