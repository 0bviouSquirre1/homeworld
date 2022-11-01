
namespace homeworld
{
    public class Program
    {
        public static void Main()
        {
            Map.Setup();
            Setup.CreatePlayer(new XY(1,1));  // id: 1
            Setup.CreateWell(new XY(3,3));    // id: 2
            Setup.CreateBucket(new XY(-2,4)); // id: 3
            Setup.CreateKettle(new XY(1,1));  // id: 4

            // TODO: more sophisticated name generation
            Setup.CreateRandomPlants("tomato");
            Setup.CreateRandomPlants("mint");
            Setup.CreateRandomPlants("thyme");
            Setup.CreateRandomPlants("sunflower");
            Setup.CreateRandomPlants("nightshade");

            Setup.CreateRandomItems("a teacup");
            Setup.CreateRandomItems("a silver spoon");
            Setup.CreateRandomItems("a saucer");

            // TODO: figure out how to keep {name} nearby
            Display.EntityInventory(5);
            GrowSystem.Grow(5, "tomato");
            Display.EntityInventory(5);
            
            /*Display.AllEntities();
            Display.OverheadMap();
            Console.WriteLine();
            Movement.MovePlayer(new XY(1,2));
            Movement.MovePlayer(new XY(2,2));
            Movement.MovePlayer(new XY(2,3));
            Movement.MovePlayer(new XY(3,3));
            Movement.MovePlayer(new XY(3,4));
            Movement.MovePlayer(new XY(3,3));
            Movement.MovePlayer(new XY(3,2));
            Display.OverheadMap();*/
        }
    }
}