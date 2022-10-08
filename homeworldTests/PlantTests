using NUnit.Framework;
using homeworld;

// These test the basic functions of the Plant class
[TestFixture]
public class PlantTests
{
    private Plant plant;

    [SetUp]
    public void Setup()
    {
        plant = new Plant();
    }

    [Test]
    public void TestPlant_InitializesCorrectly()
    {
        string plantName = "a mandrake plant";
        string plantDescription = "a menacing rosette of tough, dark leaves spreads out from a center of showy purple flowers";
        var plantStatus = plant.PlantStatus;

        Assert.That(plant.Name, Is.EqualTo(plantName));
        Assert.That(plant.Description, Is.EqualTo(plantDescription));
        Assert.That(plantStatus, Is.EqualTo(Plant.PlantFlags.None));
    }

    [Test]
    public void TestPlant_CanGrow()
    {
        plant.Grow();
        Assert.That(plant.PlantStatus, Is.EqualTo(Plant.PlantFlags.Harvestable));
    }

    [Test]
    public void TestPlant_CanBeHarvested()
    {
        plant.Grow();
        Item harvestedItem = plant.Harvest();
        Assert.That(harvestedItem.Name, Is.EqualTo("a mandrake root"));
    }
}