using Optional;

namespace homeworld
{
    public class Program
    {
        public static void Main()
        {
            Map.Setup();
            Entity player = Create.Player(new XY(1,1));
            Entity saucer = Create.Item("a saucer", new XY(1,2));
            Entity cup = Create.Item("a cup", new XY(2,1));

            Display.OverheadMap(player);

            Display.AllComponentsOfEntity(player);

            // player starts at 1,1
            Display.EntityInventory(player);

            Movement.MovePlayer(player, new XY(1,2));
            Display.OverheadMap(player);
            InventorySystem.GetItem(player, saucer);
            Display.EntityInventory(player);

            Movement.MovePlayer(player, new XY(2,2));
            Display.OverheadMap(player);
            InventorySystem.DropItem(player, saucer);
            Display.EntityInventory(player);

            Movement.MovePlayer(player, new XY(2,1));
            Display.OverheadMap(player);
            InventorySystem.GetItem(player, cup);
            Display.EntityInventory(player);

            Movement.MovePlayer(player, new XY(2,0));
            Display.OverheadMap(player);
            InventorySystem.DropItem(player, cup);
            Display.EntityInventory(player);

            Display.OverheadMap(player);
        }        
    }
}