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
    public void GetGenericItemCheckInventory()
    {
        XY here = new XY(1,1);
        Entity player = EntityManager.CreateEntity(Archetype.States.Player, here);
        Entity item = EntityManager.CreateEntity(Archetype.States.Item, here);
        Assert.That(!player.Inventory().Contains(item));

        InventorySystem.GetItem(player.EntityID, item);

        Assert.Contains(item, player.Inventory());
    }
    [Test]
    public void GetGenericItemCheckLocation()
    {
        XY here = new XY(1,1);
        Entity player = EntityManager.CreateEntity(Archetype.States.Player, here);
        Entity item = EntityManager.CreateEntity(Archetype.States.Item, here);
        Assert.Contains(item, Lookup.EntitiesAtLocation(here));

        InventorySystem.GetItem(player.EntityID, item);

        Assert.That(!Lookup.EntitiesAtLocation(here).Contains(item));
    }
    [Test]
    public void DropGenericItem()
    {
        XY here = new XY(1,1);
        Entity player = EntityManager.CreateEntity(Archetype.States.Player, here);
        Entity item = EntityManager.CreateEntity(Archetype.States.Item, here);
        InventorySystem.AddToInventory(player.EntityID, item);
        Display.AllEntities();

        InventorySystem.DropItem(player.EntityID, item);

        Assert.That(!player.Inventory().Contains(item));
        Assert.Contains(item, Lookup.EntitiesAtLocation(here));
    }
}