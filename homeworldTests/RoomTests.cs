using NUnit.Framework;
using homeworld;
using System.Collections.Generic;

// These test the basic functions of the Room class
[TestFixture]
public class RoomTests
{
    private Room room;
    private Item sword = new Item("a rusty sword");
    private Item flask = new Item("a blue flask");
    private Item stone = new Item("a piece of starstone");
    private List<Item> listOfItems;

    [SetUp]
    public void Setup()
    {
        room = new Room();
    }

    [Test]
    public void TestRoom_InitializesCorrectly()
    {
        // Arrange
        XY origin = new XY(0,0);

        // Assert that the room initialized correctly
        Assert.That(room.Name, Is.EqualTo("(blank)"));
        Assert.That(room.Description, Is.EqualTo(""));
        Assert.That(room.Inventory, Is.Empty);
        Assert.That(room.Location, Is.EqualTo(origin));
    }

    [Test]
    public void TestRoom_CanAddOneItem()
    {
        // Arrange
        Item item = new Item();

        // Act
        room.AddToInventory(item);

        // Assert that the item is in the room's inventory
        Assert.That(room.Inventory, Does.Contain(item));
    }

    [Test]
    public void TestRoom_CanAddManyItems()
    {
        // Arrange
        List<Item> listOfItems = new List<Item>();
        listOfItems.Add(sword);
        listOfItems.Add(flask);
        listOfItems.Add(stone);
        
        // Act
        room.AddToInventory(listOfItems);

        // Assert that the room's inventory contains all the items
        Assert.That(room.Inventory, Does.Contain(sword));
        Assert.That(room.Inventory, Does.Contain(flask));
        Assert.That(room.Inventory, Does.Contain(stone));
    }

    [Test]
    public void TestRoom_Exists_TakesXY_ReturnsTrue()
    {
        // Arrange

        // Act
        var result = Room.Exists(room.Location);

        // Assert that the room does exist
        Assert.That(result, Is.True);
    }

    [Test]
    public void TestRoom_Exists_TakesXY_ReturnsFalse()
    {
        // Arrange
        XY location = new XY(5,5);

        // Act
        var result = Room.Exists(location);

        // Assert that the room does not exist
        Assert.That(result, Is.False);
    }

    public void TestRoom_GetRoom_TakesXY_Returns_Room()
    {
        // Arrange


        // Act
        Room result = Room.GetRoom(room.Location);

        // Assert
        Assert.That(result.Location, Is.EqualTo(room.Location));
    }
}