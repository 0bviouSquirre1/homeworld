using NUnit.Framework;
using homeworld;
using System;


// These test the basic interactions of the Player class with the fabric of the universe
[TestFixture]
public class PlayerTests { 
    private Player player;
    private Item sword = new Item("a rusty sword");
    private Item flask = new Item("a blue flask");
    private Item stone = new Item("a piece of starstone");

    [SetUp]
    public void Setup()
    {
        player = new Player();
        for (int x = -2; x < 3; x++)
        {
            for (int y = -2; y < 3; y++)
            {
                Room room = new Room(new XY(x, y));
            }
        }
    }

    [Test]
    public void TestPlayer_InitializesCorrectly()
    {
        // Arrange & Act occured during Setup();

        // Assert that all members are initialized correctly
        Assert.That(player.Name, Is.EqualTo("jod"));
        Assert.That(player.Location, Is.EqualTo(new XY()));
        Assert.That(player.Description, Is.EqualTo("a strapping young adventurer bound for glory or a hole"));
        Assert.That(player.Inventory, Is.Empty);
    }

    [Test]
    public void TestPlayer_CanAddToInventory() {
        // Arrange
        player.GetItem(sword);
        player.GetItem(flask);
        player.GetItem(stone);

        // Assert that the items are in the player's inventory
        Assert.That(player.Inventory, Does.Contain(sword));
        Assert.That(player.Inventory, Does.Contain(flask));
        Assert.That(player.Inventory, Does.Contain(stone));
    }

    [Test]
    public void TestPlayer_CanRemoveFromInventory() {
        // Arrange
        player.GetItem(sword);
        player.GetItem(flask);
        player.GetItem(stone);

        // Act
        player.DropItem(flask);

        // Assert that the item is no longer in the player's inventory
        Assert.That(player.Inventory, Does.Not.Contain(flask));
        Assert.That(player.Inventory, Does.Contain(sword));
        Assert.That(player.Inventory, Does.Contain(stone));
    }

    [TestCase("north")]
    [TestCase("east")]
    [TestCase("south")]
    [TestCase("west")]
    public void TestPlayer_CanMoveLikeAPlayer(string direction) {
        // Arrange
        XY location = XY.CheckDirection(direction);

        // Act
        player.Move(direction);

        // Assert that the player's location has changed
        Assert.AreEqual(player.Location, location);
    }
}