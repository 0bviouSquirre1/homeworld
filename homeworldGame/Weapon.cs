namespace homeworld
{
    public class Weapon : Item
    {
        public Weapon(string name = "a rusty sword", string description = "an old rusty broadsword: the blade is pitted from years of neglect and the edge is chipped and dull")
        {
            Name = name;
            Description = description;
        }

        // METHODS

        public void BeEquipped() {
            this.ItemStatus = Status.Equipped;
        }
    }
}