using NUnit.Framework;
using System.Collections.Generic;
using System;
using homeworld;

public class Tests
{
    private DatabaseController dbController;

    [SetUp]
    public void Setup()
    {
        dbController = new DatabaseController();
        // dbController.KillAllEntities();
    }

    [Test]
    public void TestDatabaseController_CanReturnAllComponentTypes()
    {
        // Arrange
        List<string> tester = new List<string>();
        tester.Add("XY");
        tester.Add("HealthPoints");
        tester.Add("Grows");

        // Act
        List<string> result = dbController.GetAllComponentTypes();

        // Assert
        Assert.That(result, Is.EquivalentTo(tester));
    }

    [Test]
    public void TestDatabaseController_CanReturnAllEntities()
    {
        // dbController.KillAllEntities();

        // Arrange
        List<int> tester = new List<int>();
        tester.Add(1);
        tester.Add(2);
        tester.Add(3);
        dbController.CreateEntity();
        dbController.CreateEntity();
        dbController.CreateEntity();

        // Act
        List<int> result = dbController.GetAllEntities();

        // Assert
        Assert.That(result, Is.EquivalentTo(tester));

        // dbController.KillAllEntities();
    }

    [Test]
    public void TestDatabaseController_CanGetLastEntityID()
    {
        // Arrange

        // Act
        int result = dbController.GetLastEntityID();

        // Assert
        Assert.That(result, Is.AtLeast(1));
    }

    [Test]
    public void TestDatabaseController_CanCreateAnEntity()
    {
        // Arrange
        int lastEntityID;

        // Act
        int entity = dbController.CreateEntity();
        lastEntityID = dbController.GetLastEntityID();

        // Assert
        Assert.That(entity, Is.EqualTo(lastEntityID));
    }

    [Test]
    public void TestDatabaseController_CanKillAllEntities()
    {
        // Arrange
        List<int> result = dbController.GetAllEntities();
        
        // Act
        dbController.KillAllEntities();
        result = dbController.GetAllEntities();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestDatabaseController_EntityDoesExist()
    {
        // Arrange
        dbController.CreateEntity();

        // Act
        var result = dbController.EntityExists(1);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void TestDatabaseController_EntityDoesNotExist()
    {
        // Arrange
        dbController.KillAllEntities();

        // Act
        var result = dbController.EntityExists(1);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void TestDatabaseController_KillAnEntity()
    {
        // Arrange
        int targetEntity = dbController.CreateEntity();

        // Act
        dbController.KillEntity(targetEntity);
        var result = dbController.EntityExists(targetEntity);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void TestDatabaseController_AddAComponent()
    {
        // Arrange
        int targetEntity = dbController.CreateEntity();
        XY locationComponent = new XY();

        // Act
        int component_data_id = dbController.AddComponent(targetEntity, locationComponent);
        var result = dbController.EntityHasComponent(targetEntity, locationComponent);

        // Assert
        Assert.That(result, Is.True);
    }
}