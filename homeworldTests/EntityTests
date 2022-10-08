using NUnit.Framework;
using homeworld;

// These test the basic functions of the Entity class
[TestFixture]
public class EntityTests {
    private Entity entity;

    [SetUp]
    public void Setup() {
        entity = new Entity();
    }

    [Test]
    public void TestEntity_InitializesCorrectly() {
        Assert.That(entity.Name, Is.EqualTo("jort"));
        Assert.That(entity.Description, Is.EqualTo(""));
        Assert.That(entity.Location, Is.EqualTo(new XY()));
        Assert.That(entity.Inventory, Is.Empty);
    }
}