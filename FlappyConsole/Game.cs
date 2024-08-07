using System.Text;

namespace FlappyConsole
{
    public class Game
    {
        private readonly char[,] _emptyGameGrid;
        private readonly string _topBottomBorder;

        public ConsoleKeyInfo Keypress = new ();
        public char[,] GameGrid { get; set; }
        public int GridLength { get; set; } = 64;
        public int GridHeight { get; set; } = 12;
        public List<Obstacle> Obstacles { get; set; } = new List<Obstacle>();
        public int ObstacleTimer { get; set; }
        public Player Player { get; set; }
        public bool GameOver { get; set; }
        
        public Game()
        {
            ObstacleTimer = 15;
            GameGrid = new char[GridHeight,GridLength];
            Player = new();
            _topBottomBorder = String.Concat(Enumerable.Repeat("|", GridLength));
            _emptyGameGrid = GenerateEmptyGameGrid();
        }
        public bool NewFrame()
        {
            CheckInput();
            ObstacleTimer++;

            if (ObstacleTimer > 15)
            {
                GenerateObstacle();
                ObstacleTimer = 0;
            }

            foreach (var obstacle in Obstacles)
            {
                obstacle.XPosition--;
            }

            GameGrid = _emptyGameGrid.Clone() as char[,];

            PaintObstacles();
            SetPlayer();

            if (GameOver)
            {
                return false;
            }
            else
            {
                GenerateNewView();
                return true;
            }
        }
        private void CheckInput()
        {
            if (Console.KeyAvailable)
            {
                Keypress = Console.ReadKey(true);

                if (Keypress.Key == ConsoleKey.Spacebar)
                {
                    Player.Fly();
                }
            }
        }

        private char[,] GenerateEmptyGameGrid()
        {
            char[,] gameGrid = new char[GridHeight, GridLength];
            for (int i = 0; i < GridHeight; i++)
            {
                for (int j = 0; j < GridLength; j++)
                {
                    if (j == 0 || j == (GridLength - 1))
                    {
                        gameGrid[i, j] = '|';
                    }
                    else
                        gameGrid[i, j] = ' ';
                }
            }

            return gameGrid;
        }

        private void GenerateObstacle()
        {
            Obstacles.Add(new Obstacle(GameGrid.GetLength(1) - 1, GameGrid.GetLength(0)));       
        }

        private void PaintObstacles()
        {
            foreach (var obstacle in Obstacles.ToList())
            {
                if (obstacle.XPosition < 0)
                {
                    Obstacles.Remove(obstacle);
                }
                else
                {
                    for (int i = 0; i < GridHeight; i++)
                    {
                        GameGrid[i, obstacle.XPosition] = obstacle.YPositions[i];
                    }
                }
            }
        }

        private void SetPlayer()
        {
            Player.SetPlayerCoordinates();

            if (Player.PositionY <= GridHeight-1)
            {
                if (GameGrid[Player.PositionY, Player.PositionX] == ' ')
                {
                    GameGrid[Player.PositionY, Player.PositionX] = 'O';
                }
                else
                {
                    GameOver = true;
                }
            }
           
            else
            {
                GameOver = true;
            }

            if (GameGrid[0, Player.PositionX] == 'X' || GameGrid[GridHeight - 1, Player.PositionX] == 'X')
            {
                Player.AddPoint();
            }
        }

        private void GenerateNewView()
        {
            var stringBuilder = new StringBuilder();

            // Build the top border
            stringBuilder.AppendLine(_topBottomBorder);

            // Build the game grid
            for (int i = 0; i < GameGrid.GetLength(0); i++)
            {
                for (int j = 0; j < GameGrid.GetLength(1); j++)
                {
                    stringBuilder.Append(GameGrid[i, j]);
                }
                stringBuilder.AppendLine();
            }

            // Build the bottom border and points display
            stringBuilder.AppendLine(_topBottomBorder);
            stringBuilder.AppendLine($"\nPoints: {Player.Points}");

            // Move the cursor to the top-left corner and write the frame content
            Console.SetCursorPosition(0, 0);
            Console.Write(stringBuilder.ToString());
        }
    }
}
