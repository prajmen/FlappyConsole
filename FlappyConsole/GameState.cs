namespace FlappyConsole
{
    public class GameState
    {
        public Game? Game { get; set; }
        public int HighestScore { get; set; }

        public void StartScreen()
        {
            Console.WriteLine(@"  ______ _                           ____  _         _   ");
            Console.WriteLine(@" |  ____| |                         |  _ \(_)       | | ");
            Console.WriteLine(@" | |__  | | __ _ _ __  _ __  _   _  | |_) |_ _ __ __| |___");
            Console.WriteLine(@" |  __| | |/ _` | '_ \| '_ \| | | | |  _ <| | '__/ _` / __|   ");
            Console.WriteLine(@" | |    | | (_| | |_) | |_) | |_| | | |_) | | | | (_| \__ \  ");
            Console.WriteLine(@" |_|    |_|\__,_| .__/| .__/ \__, | |____/|_|_|  \__,_|___/  ");
            Console.WriteLine(@"                | |   | |     __/ |                          ");
            Console.WriteLine(@"   _____        |_|   |_|    |___/      _____ _                 ");
            Console.WriteLine(@"  / ____|                    | |       / ____| |                ");
            Console.WriteLine(@" | |     ___  _ __  ___  ___ | | ___  | |    | | ___  _ __   ___ ");
            Console.WriteLine(@" | |    / _ \| '_ \/ __|/ _ \| |/ _ \ | |    | |/ _ \| '_ \ / _ \");
            Console.WriteLine(@" | |___| (_) | | | \__ \ (_) | |  __/ | |____| | (_) | | | |  __/");
            Console.WriteLine(@"  \_____\___/|_| |_|___/\___/|_|\___|  \_____|_|\___/|_| |_|\___|");
            Console.WriteLine();
            Console.WriteLine("\t\t\t Press Space to play");

            while (true)
            {
                var keyPress = Console.ReadKey();

                if (keyPress.Key == ConsoleKey.Spacebar)
                {
                    break;
                }
            }

            NewGame();
        }

        public void NewGame()
        {
            Game = new Game();

            while (true)
            {
                if (!Game.NewFrame())
                {
                    break;
                }

                Thread.Sleep(20);
            }

            if (Game.Player.Points > HighestScore)
            {
                HighestScore = Game.Player.Points;
            }

            Console.Clear();
            Console.WriteLine("\t\t\tGame Over");
            Console.WriteLine("\t\t\tHighscore: " + HighestScore);

            StartScreen();
        }
    }
}
