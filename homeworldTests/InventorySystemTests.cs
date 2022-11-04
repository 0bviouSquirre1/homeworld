using NUnit.Framework;

namespace homeworld;

public class InventorySystemTests
{
    [SetUp]
    public void Setup()
    {
        EntityManager.KillAllEntities();
    }
    [Test]
    public void AddToInventoryTest()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity player = EntityManager.CreateEntity(Archetype.States.Player, here);
        Entity item = EntityManager.CreateEntity(Archetype.States.Item, here);

        // Act
        InventorySystem.AddToInventory(player, item);
        bool result = false;
        var list = Lookup.ComponentOfEntityByType<Inventory>(player);
        list.Map(inv => inv.InventoryList)
            .MatchSome(inv_list => result = inv_list.Contains(item));

        // Assert
        Assert.IsTrue(result);
    }
    [Test]
    public void RemoveFromInventoryTest()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity player = EntityManager.CreateEntity(Archetype.States.Player, here);
        Entity item = EntityManager.CreateEntity(Archetype.States.Item, here);
        InventorySystem.AddToInventory(player, item);

        // Act
        InventorySystem.RemoveFromInventory(player, item);

        // Assert
        Assert.That(!player.Inventory().Contains(item));
    }
    [Test]
    public void GetItemTest()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity player = EntityManager.CreateEntity(Archetype.States.Player, here);
        Entity item = EntityManager.CreateEntity(Archetype.States.Item, here);
        Assert.That(!player.Inventory().Contains(item));

        // Act
        InventorySystem.GetItem(player, item);

        // Assert
        Assert.That(!Lookup.EntitiesAtLocation(here).Contains(item));
        Assert.Contains(item, player.Inventory());
    }
    [Test]
    public void DropItemTest()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity player = EntityManager.CreateEntity(Archetype.States.Player, here);
        Entity item = EntityManager.CreateEntity(Archetype.States.Item, here);
        InventorySystem.GetItem(player, item);

        // Act
        InventorySystem.DropItem(player, item);

        // Assert
        Assert.That(!player.Inventory().Contains(item));
        Assert.Contains(item, Lookup.EntitiesAtLocation(here));
    }
    [Test]
    public void EntityInventoryContainsTest()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity player = EntityManager.CreateEntity(Archetype.States.Player, here);
        Entity item = EntityManager.CreateEntity(Archetype.States.Item, here);
        InventorySystem.AddToInventory(player, item);

        // Act
        var result = InventorySystem.EntityInventoryContains(player, item);

        // Assert
        Assert.IsTrue(result);
    }
}