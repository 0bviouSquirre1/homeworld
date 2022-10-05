using NUnit.Framework;
using homeworld;

// These test the basic functions of the Item class
[TestFixture]
public class ItemTests {
    private Item item;

    [SetUp]
    public void Setup() {
        item = new Item();
    }

    [Test]
    public void TestItem_InitializesCorrectly() {
        Assert.That(item.Name, Is.EqualTo("a journal"));
        Assert.That(item.Description, Is.EqualTo("a tattered old journal"));
        Assert.That(item.Inventory, Is.Empty);
    }
}