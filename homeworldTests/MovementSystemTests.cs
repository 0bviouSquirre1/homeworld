using NUnit.Framework;

namespace homeworld;

public class MovementSystemTests
{
    [SetUp]
    public void Setup()
    {
        EntityManager.KillAllEntities();
    }

    [Test]
    public void UpdateEntityLocationTest()
    {
        // Arrange
        XY here = new XY(1,1);
        XY there = new XY(1,2);
        Entity player = EntityManager.CreateEntity(Archetype.States.Player, here);

        // Act
        Movement.UpdateEntityLocation(player.EntityID, there);
        var return_location = Lookup.EntityLocation(player.EntityID);

        // Assert
        Assert.AreEqual(there, return_location);
    }
}