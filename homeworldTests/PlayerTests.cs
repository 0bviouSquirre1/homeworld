using NUnit.Framework;
using homeworld;
using System;


// These test the basic interactions of the Player class with the fabric of the universe
[TestFixture]
public class PlayerTests { 
    private Player player;
    private Item[] invArray = Array.Empty<Item>();
    private Item sword = new Item("a rusty sword");
    private Item flask = new Item("a blue flask");
    private Item stone = new Item("a piece of starstone");

    [SetUp]
    public void Setup() {
        player = new Player();
        for (int x = -2; x < 3; x++) {
            for (int y = -2; y < 3; y++) {
                Room room = new Room(new XY(x, y));
            }
        }
    }

    [Test]
    public void TestPlayer_InitializesCorrectly() {
        
        Assert.That(player.Name, Is.EqualTo("jod"));
        Assert.That(player.Location, Is.EqualTo(new XY()));
        Assert.That(player.Description, Is.EqualTo("a strapping young adventurer bound for glory or a hole"));
        Assert.That(player.Inventory, Is.Empty);
    }

    [Test]
    public void TestPlayer_CanAddAndRemoveFromInventory() {
        player.GetItem(sword);
        player.GetItem(flask);
        player.GetItem(stone);

        foreach (Item item in player.Inventory) {
            Console.WriteLine($"{item}");
        }

        player.DropItem(flask);

        Assert.That(player.Inventory, Does.Not.Contain(flask));
    }

    [TestCase("north")]
    [TestCase("east")]
    [TestCase("south")]
    [TestCase("west")]
    public void TestPlayer_CanMoveLikeAPlayer(string direction) {
        Console.WriteLine($"player is at {player.Location}");
        XY location = XY.CheckDirection(direction);

        Console.WriteLine($"moving to {location}");
        player.Move(direction);
        Console.WriteLine($"player is now at {player.Location}");

        Assert.AreEqual(player.Location, location);
    }
}