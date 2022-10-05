using NUnit.Framework;
using homeworld;

// These test the basic functions of the CombatDummy class
[TestFixture]
public class CombatDummyTests {
    private CombatDummy combatDummy;

    [SetUp]
    public void Setup() {
        combatDummy = new CombatDummy();
    }

    [Test]
    public void TestCombatDummy_InitializesCorrectly() {
        Assert.That(combatDummy.Name, Is.EqualTo("a combat practice dummy"));
        Assert.That(combatDummy.Description, Is.EqualTo(""));
        Assert.That(combatDummy.Health, Is.EqualTo(10));
        Assert.That(combatDummy.Inventory, Is.Empty);
    }

    [Test]
    public void TestCombatDummy_CanTakeDamage() {
        combatDummy.TakeDamage(5);
        Assert.That(combatDummy.Health, Is.EqualTo(5));
    }

    [Test]
    public void TestCombatDummy_CanAttack() {
        CombatDummy enemy = new CombatDummy();
        combatDummy.Attack(enemy);
        Assert.That(enemy.Health, Is.EqualTo(5));
        Assert.That(combatDummy.Health, Is.EqualTo(10));
    }

    [Test]
    public void TestCombatDummy_CanDie() {
        combatDummy.Die();
        Assert.That(combatDummy.Health, Is.EqualTo(0));
    }
}