using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlappyConsole
{
    public class Game
    {
        public ConsoleKeyInfo keypress = new();
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
            GameGrid = new char[GridHeight, GridLength];
            Player = new();
        }
        public bool NewFrame()
        {
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

            GenerateEmptyGameGrid();
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
        public void GenerateEmptyGameGrid()
        {
            for (int i = 0; i < GridHeight; i++)
            {
                for (int j = 0; j < GridLength; j++)
                {
                    if (j == 0 || j == (GridLength - 1))
                    {
                        GameGrid[i, j] = '|';
                    }
                    else
                        GameGrid[i, j] = ' ';
                }
            }
        }

        public void CheckInput()
        {
            while (Console.KeyAvailable)
            {
                keypress = Console.ReadKey(true);

                if (keypress.Key == ConsoleKey.Spacebar)
                {

                    Player.Fly();
                }
            }
        }

        public void GenerateObstacle()
        {
            Obstacles.Add(new Obstacle(GameGrid.GetLength(1) - 1, GameGrid.GetLength(0)));
        }


        public void PaintObstacles()
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

        public void SetPlayer()
        {
            Player.SetPlayerCoordinates();

            if (Player.PositionY <= GridHeight - 1)
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

        public void GenerateNewView()
        {
            Console.Clear();
            string topBottomBorder = String.Concat(Enumerable.Repeat("|", GridLength));

            Console.WriteLine(topBottomBorder);

            for (int i = 0; i < GameGrid.GetLength(0); i++)
            {
                for (int j = 0; j < GameGrid.GetLength(1); j++)
                {

                    Console.Write(GameGrid[i, j]);
                }

                Console.Write("\n");
            }
            Console.WriteLine(topBottomBorder);

            Console.WriteLine("\nPoints: " + Player.Points);
        }
    }
}
