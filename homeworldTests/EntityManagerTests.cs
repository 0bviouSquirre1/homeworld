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
    public void RemoveComponent()
    {
        XY here = new XY(3,3);
        Entity entity = EntityManager.CreateEntity(Archetype.States.None, here);
        var component = Lookup.ComponentOfEntityByType<Location>(entity.EntityID);

        EntityManager.RemoveComponent<Location>(entity.EntityID);

        component.MatchSome(c => Assert.That(!Lookup.AllComponentsOfEntity(entity.EntityID).Contains(c)));
    }
    [Test]
    public void AddComponent()
    {
        // Arrange
        XY here = new XY(3,3);
        Entity entity = EntityManager.CreateEntity(Archetype.States.None, here);
        var component = Lookup.ComponentOfEntityByType<Location>(entity.EntityID);
        EntityManager.RemoveComponent<Location>(entity.EntityID);

        // Act
        EntityManager.AddComponent<Location>(entity.EntityID);

        // Assert
        component.MatchSome(c => Assert.That(!Lookup.AllComponentsOfEntity(entity.EntityID).Contains(c)));
    }
}