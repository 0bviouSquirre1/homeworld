namespace homeworld {
    public class Program {
        public static void Main() { 
            // Create 4x4 grid of rooms
            /* for (int x = 0; x < 4; x++) {
                for (int y = 0; y < 4; y++) {
                    Room room = new Room(new XY(x, y));
                }
            }

            XY treasureRoom = new XY(3,3);
            if (Room.Exists(treasureRoom)) {
                Console.WriteLine($"The room exists!");
                Item[] invArray = { new Item("a rusty sword"), 
                    new Item("a blue flask"), 
                    new Item("a piece of starstone") };
                Room.Map.TryGetValue(treasureRoom, out Room? addRoom);
                foreach (Item item in invArray) {
                    addRoom!.Inventory.Add(item);
                    Console.WriteLine($"{item} dropped here.");
                }
            } */
          
            // Create player
            Player player = new Player("jeans", new XY());

            bool exit = false;

            // Main body of the program happens in this loop
            do {
                // Get {input} from the user, verify and chop it up
                Room viewRoom = Room.GetRoom(player.Location);
                viewRoom.Display();
                Console.WriteLine($" - Please enter a command - \n");

                string input = Console.ReadLine() ?? throw new ArgumentNullException(nameof(input));
                (string command, string target, string[] modifiers) validInput = HandleUserInput(input!);

                // The logic of the commands goes here
                switch (validInput.command) {
                    case "move":
                        player.Move(validInput.target);
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine($"The command {validInput.command} is not recognized.");
                        break;
                }
            } while (!exit);
        }

        public static void Setup() {
            
        }

        public static void GameLogic() {
            
        }

        internal static (string,string,string[]) HandleUserInput(string input) {
            string[] words = input.Split(' ');
            string command = words[0].ToLower();
            string target = "";
            string[] modifiers = Array.Empty<string>();
            if (words.Length > 1) {
                target = words[1];
                modifiers = words.Skip(2).ToArray();
            }
            return (command, target, modifiers);
        }
    }
}