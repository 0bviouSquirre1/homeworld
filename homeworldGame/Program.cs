// 1.6 when the assemblage creation is complete, check
// 1.6.1 Check for all components on a specific entity
// 1.6.2 compare that against the list of assemblage parts to confirm
// Cannot create a component without having an entity to attach it to
// 2. active world state checks
// 3.1.1 removed components should have an unload function to make sure they're not losing important information?
// start with the change that needs to be reflected in the database and work backwards
//
// 1. identity components: player is the only one i hneed right now but there should probably be others?
//      use this to "grab" the player
// 2. set defaults for all columns in the database, minimize the need for null
// 3. use database triggers to manage updating multiple tables at once
// 4. replace booleans with presence on a specific table? isHarvestable vs presence on the Harvestable table?
// 5. announce events, react to announcements
// 5.1 aka publish-subscribe model
// 5.2 messaging system?
// events: movement command submitted
// 6 actively choosing to tick on player's valid input only
// 7 each change writes a sql command to the transaction, every five or so changes/ticks, the group writes to the database automatically, like an autosave
// 7.1 if a select query is made, fire off waiting changes to get most up-to-date info
// 8 IComponent interface(?) has update() function
// Actual gameplay idea:
//      you're an exile from another place who has been pushed out into the world. you've gotta make friends and learn the ways of the new place you're in
//      the first family you meet will take you in, but then it's up to you to upgrade your living quarters and your daily employment, build relationships, etc
//      your health is now your reputation in the community-- you start at zero and borrow the reputation of the person who introduces you to a new person, but from then on it's up to you to manage it
//      eventually you have enough reputation with enough individuals in the community that you are Known (of Unknown, Known, Well-Known, Renowned, and Infamous)
//      type the following sentence but replace the words in parentheses with your own pronouns: (He) left (his) umbrella here. Give it to (him) later
// 9 write a logger that can be used to conditionally log events
// CPUs process much faster than memory (caching)


namespace homeworld
{
    public class Program
    {
        public static void Main()
        {
            Player player = new Player("jod", new XYComponent(1,1));
            bool exit = false;
            
            // Create the world
            // Create a bunch of plants on a -5 to +5 XY grid (100 squares)
            Plant.RandomPlants("a tomato plant", new Item("a tomato"));
            Plant.RandomPlants("a mint plant", new Item("a spring of mint"));
            Plant.RandomPlants("a thyme plant", new Item("a spring of thyme"));
            Plant.RandomPlants("a sunflower plant", new Item("a sunflower"));
            Plant.RandomPlants("a nightshade plant", new Item("a handful of nightshade leaves"));

            // Create a bunch of items on a -5 to +5 grid around the starting house (1,1)
            Item.RandomItems("a teacup");
            Item.RandomItems("a silver spoon");
            Item.RandomItems("a saucer");

            // Create the well, bucket, and kettle
            Container well = new Container("a stone well", new XYComponent(3,3));
            Container bucket = new Container("a wooden bucket", new XYComponent(-2,4));
            Container kettle = new Container("an iron kettle suspended over the hearth", new XYComponent(1,1));

            // View the world
            DisplayProcessor.DisplayAllItemsInWorld();
            DisplayProcessor.DisplayAllPlantsInWorld();
        }
    }
}