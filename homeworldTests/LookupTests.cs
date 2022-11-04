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
        XY here = new XY(2,2);
        Entity entity = EntityManager.CreateEntity(Archetype.States.None, here);

        var return_location = Lookup.EntityLocation(entity.EntityID);

        Assert.AreEqual(here, return_location);
    }

    [Test]
    public void EntitiesAtLocationTest()
    {
        XY here = new XY(2,2);
        Entity entity = EntityManager.CreateEntity(Archetype.States.None, here);
        Entity entity2 = EntityManager.CreateEntity(Archetype.States.None, here);
        Entity entity3 = EntityManager.CreateEntity(Archetype.States.None, here);

        var return_list = Lookup.EntitiesAtLocation(here);

        Assert.That(return_list.Count.Equals(3));
        Assert.That(return_list.Contains(entity));
    }
}