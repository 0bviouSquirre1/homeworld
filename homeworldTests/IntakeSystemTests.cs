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
        Intake.Consume(player, produce);

        // Assert
        Assert.AreEqual(0, player.Inventory().Count);
    }
}