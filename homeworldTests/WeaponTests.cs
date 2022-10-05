using NUnit.Framework;
using homeworld;

// These test the basic functions of the Weapon class
[TestFixture]
public class WeaponTests
{
    private Weapon weapon;
    private Player player;

    [SetUp]
    public void Setup()
    {
        weapon = new Weapon();
    }

    [Test]
    public void TestWeapon_InitializesCorrectly()
    {
        Assert.That(weapon.Name, Is.EqualTo("a rusty sword"));
        Assert.That(weapon.Description, Is.EqualTo("an old rusty broadsword: the blade is pitted from years of neglect and the edge is chipped and dull"));
        Assert.That(weapon.Inventory, Is.Empty);
    }

    public void TestWeapon_CanBeEquipped()
    {
        weapon.BeEquipped();
        Assert.That(weapon.ItemStatus, Is.EqualTo(Weapon.Status.Equipped));
    }
}