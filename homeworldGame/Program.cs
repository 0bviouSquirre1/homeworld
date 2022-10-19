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
            
            MovementTest(player);
            ItemTest(player);

            Item produce = new Item("tomato");
            Plant plant = new Plant("tomato plant", new XYComponent(0,-2), produce);

            player.Move("south");
            player.Move("south");

            player.Gather(plant, produce);

            DisplayProcessor.DisplayAllPlantsInWorld();
            DisplayProcessor.DisplayInventory(plant);
            DisplayProcessor.DisplayInventory(player);

            player.Gather(plant, produce);
            player.Gather(plant, produce);
            player.Gather(plant, produce);

            DisplayProcessor.DisplayInventory(plant);
            DisplayProcessor.DisplayInventory(player);
        }

        public static void MovementTest(Player player)
        {

            player.Move("north");
            player.Move("south");
            player.Move("east");
            player.Move("west");
            player.Move("first");
            // output: jod has moved north.
        }

        public static void ItemTest(Player player)
        {
            Item tomato = new Item("tomato", new XYComponent(1,1));
            Item hyssop = new Item("hyssop flower");
            Item marigold = new Item("marigold bloom");
            DisplayProcessor.DisplayAllItemsInWorld();

            player.Get(tomato);
            player.Get(hyssop);
            player.Get(marigold);
            DisplayProcessor.DisplayInventory(player);
            DisplayProcessor.DisplayAllItemsInWorld();

            player.Move("south");
            player.Move("west");

            player.Get(hyssop);
            DisplayProcessor.DisplayInventory(player);
            DisplayProcessor.DisplayAllItemsInLocation(player.Location);

            player.Drop(tomato);
            DisplayProcessor.DisplayInventory(player);
            DisplayProcessor.DisplayAllItemsInLocation(player.Location);
        }
    }
}