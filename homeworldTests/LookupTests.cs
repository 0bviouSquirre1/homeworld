using NUnit.Framework;

namespace homeworld;

public class LookupTests
{
    [SetUp]
    public void Setup()
    {
        EntityManager.KillAllEntities();
    }
    [Test]
    public void EntityLocationTest()
    {
        // Arrange
        XY here = new XY(2,2);
        Entity entity = EntityManager.CreateEntity(Archetype.States.Item, here);

        // Act
        var return_location = Lookup.EntityLocation(entity);

        // Assert
        Assert.AreEqual(here, return_location);
    }

    [Test]
    public void EntitiesAtLocationTest()
    {
        // Arrange
        XY here = new XY(2,2);
        Entity entity = EntityManager.CreateEntity(Archetype.States.Item, here);
        Entity entity2 = EntityManager.CreateEntity(Archetype.States.Item, here);
        Entity entity3 = EntityManager.CreateEntity(Archetype.States.Item, here);

        // Act
        var return_list = Lookup.EntitiesAtLocation(here);

        // Assert
        Assert.That(return_list.Count.Equals(3));
        Assert.That(return_list.Contains(entity));
        Assert.That(return_list.Contains(entity2));
        Assert.That(return_list.Contains(entity3));
    }
}