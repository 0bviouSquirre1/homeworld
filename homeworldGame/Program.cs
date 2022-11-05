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
            Entity mint_plant = GrowSystem.CreatePlant("mint", here);
            Entity nightshade_plant = GrowSystem.CreatePlant("nightshade", here);
            
            // player starts at 1,1
            Entity mint = GrowSystem.Harvest(player, mint_plant);
            Entity nightshade = GrowSystem.Harvest(player, nightshade_plant);
            Display.EntityInventory(player);

            Entity cup = Create.Item("a teacup", here);
            InventorySystem.GetItem(player, cup);
            Entity kettle = Create.Kettle(here);
            LiquidSystem.Fill(kettle, "water");
            Display.ContainerContents(kettle);

            string liquid = BrewingSystem.BrewTea(player, mint, kettle);
            Display.ContainerContents(kettle);
            Display.EntityInventory(player);

            LiquidSystem.Fill(cup, liquid, kettle);
            Display.ContainerContents(cup);

            Intake.Drink(player, cup);
            Display.ContainerContents(cup);
        }        
    }
}

// shout out to sazzer, bbuck, thiez of the MUD coders guild slack for literally invaluable assistance