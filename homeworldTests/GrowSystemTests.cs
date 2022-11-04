using static homeworld.Archetype.States;
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
        Entity plant = GrowSystem.CreatePlant(name, here);

        // Act
        GrowSystem.Grow(plant, name);

        // Assert
        Assert.AreEqual(2, plant.Inventory().Count);
    }
    [Test]
    public void HarvestTest()
    {
        // Arrange
        XY here = new XY(1,1);
        XY there = new XY(1,2);
        Entity player = Create.Player(here);
        Entity plant = GrowSystem.CreatePlant("tomato", here);
        int count = plant.Inventory().Count;
        var growable = Lookup.ComponentOfEntityByType<Growable>(plant);
        Entity produce = new Entity(Produce);
        growable.Map(gc => gc.GetProduce()).MatchSome(prod => produce = prod);

        // Act
        GrowSystem.Harvest(player, plant);

        // Assert
        Assert.Contains(produce, player.Inventory());
        Assert.AreNotEqual(count, plant.Inventory().Count);
    }
}