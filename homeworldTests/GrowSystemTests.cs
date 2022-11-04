using NUnit.Framework;

namespace homeworld;

public class GrowSystemTests
{
    [SetUp]
    public void Setup()
    {
        EntityManager.KillAllEntities();
    }

    [Test]
    public void GrowTest()
    {
        // Arrange
        XY here = new XY(2,2);
        string name = "tomato";
        Entity plant = Create.Plant(name, here);

        // Act
        GrowSystem.Grow(plant, name);

        // Assert
        Assert.AreEqual(2, plant.Inventory().Count);
    }
}