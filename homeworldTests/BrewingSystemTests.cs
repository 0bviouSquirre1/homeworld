using NUnit.Framework;

namespace homeworld;

public class BrewingSystemTests
{
    [SetUp]
    public void Setup()
    {
        EntityManager.KillAllEntities();
    }

    [Test]
    public void SetAndGetLiquidTest()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity container = Create.Kettle(here);
        string liquid = "aspen tea";

        // Act
        BrewingSystem.SetLiquid(container, liquid);

        // Assert
        Assert.AreSame(liquid, BrewingSystem.GetLiquid(container));
    }
    [Test]
    public void BrewTeaTest()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity container = Create.Kettle(here);
        Entity player = Create.Player(here);
        Entity plant = GrowSystem.CreatePlant("mint", here);
        string liquid = "water";
        string tea = "mint tea";
        var growable = Lookup.ComponentOfEntityByType<Growable>(plant);
        Entity produce = new Entity(Archetype.States.Produce);
        growable.Map(gc => gc.GetProduce()).MatchSome(prod => produce = prod);

        // Act
        GrowSystem.Harvest(player, plant);
        LiquidSystem.Fill(container, liquid);
        BrewingSystem.BrewTea(player, produce, container);
        System.Console.WriteLine(BrewingSystem.GetLiquid(container));

        // Assert
        Assert.That(BrewingSystem.GetLiquid(container) == tea, "liquids do not match");
    }
    [Test]
    public void TransferTeaTest()
    {
        // Arrange
        XY here = new XY(1,1);
        Entity container = Create.Kettle(here);
        Entity cup = Create.Item("a teacup", here);
        Entity player = Create.Player(here);
        Entity plant = GrowSystem.CreatePlant("mint", here);
        string liquid = "water";
        string tea = "mint tea";
        InventorySystem.GetItem(player, cup);
        Entity produce = GrowSystem.Harvest(player, plant);
        LiquidSystem.Fill(container, liquid);
        Display.ContainerContents(cup);
        Display.ContainerContents(container);
        liquid = BrewingSystem.BrewTea(player, produce, container);
        Display.ContainerContents(container);

        // Act
        LiquidSystem.Fill(cup, liquid);
        Display.ContainerContents(cup);

        // Assert
        Assert.That(BrewingSystem.GetLiquid(cup) == tea, "liquids do not match");
    }
}