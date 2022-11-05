using NUnit.Framework;

namespace homeworld;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        EntityManager.KillAllEntities();
    }

    [Test]
    public void ConsumeProduceTest()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity player = Create.Player(here);
        Entity plant = GrowSystem.CreatePlant("nightshade", here);
        GrowSystem.Harvest(player, plant);
        Entity produce = player.Inventory()[0];
        
        // Act
        Intake.Eat(player, produce);

        // Assert
        Assert.AreEqual(0, player.Inventory().Count);
    }
    [Test]
    public void DrinkTeaTest()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity player = Create.Player(here);
        Entity cup = Create.Item("a teacup", here);
        InventorySystem.GetItem(player, cup);

        Entity mint_plant = GrowSystem.CreatePlant("mint", here);
        Entity mint = GrowSystem.Harvest(player, mint_plant);

        Entity kettle = Create.Kettle(here);
        LiquidSystem.Fill(kettle, "water");

        string liquid = BrewingSystem.BrewTea(player, mint, kettle);

        LiquidSystem.Fill(cup, liquid, kettle);

        // Act
        Intake.Drink(player, cup);

        // Assert
        Assert.AreEqual(4, LiquidSystem.GetContents(cup));
    }
}