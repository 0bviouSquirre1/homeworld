namespace homeworld
{
    public static class Map
    {
        public static Dictionary<XY, bool> ExploredMap = new Dictionary<XY, bool>();

        public static void Setup()
        {
            for (int y = -5; y < 6; y++)
            {
                for (int x = -5; x < 6; x++)
                {
                    if (x == 1 && y == 1)
                        ExploredMap.Add(new XY(x,y), true);
                    else
                        ExploredMap.Add(new XY(x,y), false);
                }
            }
        }
        public static void ExploreRoom(XY location)
        {
            ExploredMap[location] = true;
        }
    }
}