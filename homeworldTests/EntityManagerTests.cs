using NUnit.Framework;
using homeworld;
using System.Collections.Generic;
using System;

// These test the basic functions of the EntityManager class
[TestFixture]
public class EntityManagerTests
{
    private EntityManager entityManager;

    [SetUp]
    public void Setup()
    {
        entityManager = new EntityManager();
    }

    [Test]
    public void TestEntityManager_InitializesCorrectly()
    {
        Assert.That(entityManager.lowestUnassignedEntityID, Is.EqualTo(1));
        Assert.That(entityManager.allEntities, Is.Empty);
        Assert.That(entityManager.ComponentStores, Is.Empty);
    }

    [Test]
    public void TestEntityManager_CreateEntity()
    {
        // Arrange
        int entity;
        
        // Act
        entity = entityManager.CreateEntity();

        // Assert
        Assert.That(entity, Is.EqualTo(1));
    }

    [Test]
    public void TestEntityManager_GenerateNewEntityID()
    {
        // Arrange
        int newID;

        // Act
        newID = entityManager.GenerateNewEntityID();

        // Assert
        Assert.That(newID, Is.EqualTo(1));
    }

    [Test]
    public void TestEntityManager_KillEntity()
    {
        // Arrange
        int entity;
        entity = entityManager.CreateEntity();

        // Act
        entityManager.KillEntity(entity);

        // Assert
        Assert.That(entityManager.allEntities, Does.Not.Contain(entity));
    }

    [Test]
    public void TestEntityManager_AddComponentToEntity()
    {
        // Arrange
        int entity = entityManager.CreateEntity();
        IComponent component = new XY(0,0);
        Dictionary<int, IComponent> innerRow = new Dictionary<int, IComponent>();
        innerRow.Add(entity, component);

        // Act
        entityManager.AddComponent(entity, component);

        // Assert
        Assert.That(entityManager.ComponentStores, Does.ContainKey(component.GetType()));
        Assert.That(entityManager.ComponentStores, Does.ContainValue(innerRow));
    }

    [Test]
    public void TestEntityManager_RemoveComponentFromEntity()
    {
        // Arrange
        int entity = entityManager.CreateEntity();
        IComponent component = new XY(0,0);
        Dictionary<int, IComponent> innerRow = new Dictionary<int, IComponent>();
        innerRow.Add(entity, component);
        entityManager.AddComponent(entity, component);
        
        // Act
        entityManager.RemoveComponent(entity, component);

        // Assert
        Assert.That(entityManager.ComponentStores, Does.Not.ContainValue(innerRow));
        
    }

    [Test]
    public void TestEntityManager_GetComponentFromList()
    {
        // Arrange
        int entity = entityManager.CreateEntity();
        IComponent component = new XY(0,0);
        Type type = component.GetType();
        Dictionary<int, IComponent> innerRow = new Dictionary<int, IComponent>();
        innerRow.Add(entity, component);
        entityManager.AddComponent(entity, component);

        // Act
        IComponent gotComponent = entityManager.GetComponent(entity, type);

        // Assert
        Assert.That(gotComponent, Is.EqualTo(component));
    }

    [Test]
    public void TestEntityManager_GetListOfAllComponentsOfType()
    {
        // Arrange
        int entity = entityManager.CreateEntity();
        int entity2 = entityManager.CreateEntity();
        int entity3 = entityManager.CreateEntity();

        IComponent component = new XY(0,0);
        IComponent component2 = new XY(1,1);
        IComponent component3 = new XY(2,2);

        Type type = component.GetType();

        entityManager.AddComponent(entity, component);
        entityManager.AddComponent(entity2, component2);
        entityManager.AddComponent(entity3, component3);

        List<IComponent> tester = new List<IComponent>();
        tester.Add(component);
        tester.Add(component2);
        tester.Add(component3);

        // Act
        List<IComponent> result = entityManager.GetAllComponentsOfType(type);
        foreach(var comp in result)
        {
            Console.WriteLine($"{comp}");
        }

        // Assert
        Assert.That(result, Is.EquivalentTo(tester));
    }

    [Test]
    public void TestEntityManager_GetAllEntitiesPossessingComponent()
    {
        // Arrange
        int entity = entityManager.CreateEntity();
        int entity2 = entityManager.CreateEntity();
        int entity3 = entityManager.CreateEntity();

        IComponent component = new XY(0,0);
        IComponent component2 = new XY(1,1);
        IComponent component3 = new XY(2,2);

        Type type = component.GetType();

        entityManager.AddComponent(entity, component);
        entityManager.AddComponent(entity2, component2);
        entityManager.AddComponent(entity3, component3);

        List<int> tester = new List<int>();
        tester.Add(entity);
        tester.Add(entity2);
        tester.Add(entity3);

        // Act
        List<int> resultList = entityManager.GetAllEntitiesPossessingComponent(type);

        // Assert
        Assert.That(resultList, Is.EquivalentTo(tester));
    }
}