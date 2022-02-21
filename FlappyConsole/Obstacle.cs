namespace FlappyConsole
{
    public class Obstacle
    {
        public int XPosition { get; set; }
        public char[] YPositions { get; set; }

        public Obstacle(int xPosition, int xLength)
        {
            XPosition = xPosition;
            YPositions = new char[xLength];

            int number = Random.Shared.Next(0, xLength - 3);

            for (int i = 0; i < xLength; i++)
            {
                if (number == i || number + 1 == i || number + 2 == i)
                {
                    YPositions[i] = ' ';
                }
                else
                {
                    YPositions[i] = 'X';
                }

            }
        }
    }
}
