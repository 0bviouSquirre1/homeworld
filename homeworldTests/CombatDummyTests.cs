using NUnit.Framework;
using homeworld;

// These test the basic functions of the CombatDummy class
[TestFixture]
public class CombatDummyTests
{
    private CombatDummy combatDummy;

    [SetUp]
    public void Setup()
    {
        combatDummy = new CombatDummy();
    }

    [Test]
    public void TestCombatDummy_InitializesCorrectly()
    {
        // Arrange

        // Act
        
        // Assert that the dummy initializes correctly
        Assert.That(combatDummy.Name, Is.EqualTo("a combat practice dummy"));
        Assert.That(combatDummy.Description, Is.EqualTo(""));
        Assert.That(combatDummy.Health, Is.EqualTo(10));
        Assert.That(combatDummy.Inventory, Is.Empty);
    }

    [Test]
    public void TestCombatDummy_CanTakeDamage()
    {
        // Arrange

        // Act
        combatDummy.TakeDamage(5);

        // Assert that the dummy's health is now 5
        Assert.That(combatDummy.Health, Is.EqualTo(5));
    }

    [Test]
    public void TestCombatDummy_CanAttack()
    {
        // Arrange
        CombatDummy enemy = new CombatDummy();

        // Act
        combatDummy.Attack(enemy);

        // Assert that the enemy is damaged but our dummy is not
        Assert.That(enemy.Health, Is.EqualTo(5));
        Assert.That(combatDummy.Health, Is.EqualTo(10));
    }

    [Test]
    public void TestCombatDummy_CanDie()
    {
        // Arrange

        // Act
        combatDummy.Die();

        // Assert that the dummy's health is now 0
        Assert.That(combatDummy.Health, Is.EqualTo(0));
    }
}