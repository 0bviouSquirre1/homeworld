using Optional;

namespace homeworld
{
    public class Program
    {
        public static void Main()
        {
            Map.Setup();
            Entity player = Create.Player(new XY(1,1));  // id: 1
            Entity saucer = Create.Item("a saucer", new XY(1,1));
            Entity cup = Create.Item("a cup", new XY(2,1));

            Display.OverheadMap(player);

            Display.AllComponentsOfEntity(player);

            // player starts at 1,1
            /*Display.EntityInventory(player.EntityID);

            Movement.MovePlayer(new XY(1,2));
            Display.OverheadMap();
            InventorySystem.GetItem(player.EntityID, saucer);
            Display.EntityInventory(player.EntityID);

            Movement.MovePlayer(new XY(2,2));
            Display.OverheadMap();
            InventorySystem.DropItem(player.EntityID, saucer);
            Display.EntityInventory(player.EntityID);*/

            /*Movement.MovePlayer(new XY(2,1));
            Display.OverheadMap();
            InventorySystem.GetItem(player.EntityID, cup);
            Display.EntityInventory(player.EntityID);

            Movement.MovePlayer(new XY(2,0));
            Display.OverheadMap();
            InventorySystem.DropItem(player.EntityID, cup);
            Display.EntityInventory(player.EntityID);*/

            Display.OverheadMap(player);
        }        
    }
}