namespace homeworld
{
    public static class Map // maybe not a system
    {
        public static void Setup()
        {
            for (int y = -5; y < 6; y++)
            {
                for (int x = -5; x < 6; x++)
                {
                    // To account for the player's starting position (1,1)
                    if (x == 1 && y == 1)
                        Lookup.ExploredMap.Add(new XY(x,y), true);
                    else
                        Lookup.ExploredMap.Add(new XY(x,y), false); 
                }
            }
        }

        public static void ExploreRoom(XY location)
        {
            Lookup.ExploredMap[location] = true;
        }
    }
}