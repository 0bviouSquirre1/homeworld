using Optional;

namespace homeworld
{
    public class Program
    {
        public static void Main()
        {
            Map.Setup();
            XY here = new XY(1,1);
            Entity player = Create.Player(here);
            Entity tomato_plant = GrowSystem.CreatePlant("tomato", here);
            Entity nightshade_plant = GrowSystem.CreatePlant("nightshade", here);
            
            // player starts at 1,1
            Entity tomato = GrowSystem.Harvest(player, tomato_plant);
            Display.EntityInventory(player);
            Entity nightshade = GrowSystem.Harvest(player, nightshade_plant);
            Display.EntityInventory(player);

            Intake.Consume(player, tomato);
            Intake.Consume(player, nightshade);
            Display.EntityInventory(player);
        }        
    }
}