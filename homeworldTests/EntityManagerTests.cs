using NUnit.Framework;

namespace homeworld;

public class EntityManagerTests
{
    [SetUp]
    public void Setup()
    {
        EntityManager.KillAllEntities();
    }
    [Test]
    public void AddComponent()
    {
        // Arrange
        XY here = new XY(3,3);
        Entity entity = EntityManager.CreateEntity(Archetype.States.None, here);
        var component = Lookup.ComponentOfEntityByType<Location>(entity.EntityID);

        // Act
        EntityManager.AddComponent<Location>(entity.EntityID);

        // Assert
        component.MatchSome(c => 
            Assert.That(
                Lookup.AllComponentsOfEntity(entity.EntityID).Contains(c)));
    }
    [Test]
    public void RemoveComponent()
    {
        // Arrange
        XY here = new XY(3,3);
        Entity entity = EntityManager.CreateEntity(Archetype.States.None, here);
        var component = Lookup.ComponentOfEntityByType<Location>(entity.EntityID);

        // Act
        EntityManager.RemoveComponent<Location>(entity.EntityID);

        // Assert
        component.MatchSome(c => Assert.That(!Lookup.AllComponentsOfEntity(entity.EntityID).Contains(c)));
    }
    [Test]
    public void HowManyLocationComponentsInWorld()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity player = EntityManager.CreateEntity(Archetype.States.Player, here);
        Entity item = EntityManager.CreateEntity(Archetype.States.Item, here);
        InventorySystem.AddToInventory(player.EntityID, item);

        // Act

        // Assert
        Assert.AreEqual(2, Lookup.AllComponentsOfType<Location>().Count);
    }
}