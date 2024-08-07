namespace FlappyConsole
{
    public class Player
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool IsFlying { get; set; }
        public int HangTime { get; set; }
        public int Points { get; set; }

        public Player()
        {
            PositionX = 16;
            PositionY = 3;
            HangTime = 2;
        }

        public void AddPoint()
        {
            Points++;
        }

        public void SetPlayerCoordinates()
        {
            if (IsFlying || HangTime < 4)
            {
                if (HangTime == 0)
                {
                    IsFlying = false;
                    HangTime = 4;
                }
                else
                {
                    PositionY = Math.Max(0, PositionY - 1);
                    HangTime--;
                }
            }
            else
            {
                PositionY++;
            }

            IsFlying = false;
        }

        public void Fly()
        {
            IsFlying = true;
        }
    }
}
