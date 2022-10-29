namespace homeworld
{
    public static class Map
    {
        public static Dictionary<XYComponent, bool> ExploredMap = new Dictionary<XYComponent, bool>();

        public static void Setup()
        {
            for (int x = -5; x < 6; x++)
            {
                for (int y = -5; y < 6; y++)
                {
                    if (x == 1 && y == 1)
                        ExploredMap.Add(new XYComponent(x,y), true);
                    else
                        ExploredMap.Add(new XYComponent(x,y), false);
                }
            }
        }
        public static void ExploreRoom(XYComponent location)
        {
            ExploredMap[location] = true;
        }
    }
}