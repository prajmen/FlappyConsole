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

            int number = Random.Shared.Next(0, xLength - 4);

            // Fill the array with 'X' initially
            for (int i = 0; i < xLength; i++)
            {
                YPositions[i] = 'X';
            }

            // Create the gap
            YPositions[number] = ' ';
            YPositions[number + 1] = ' ';
            YPositions[number + 2] = ' ';
            YPositions[number + 3] = ' ';
        }
    }
}
