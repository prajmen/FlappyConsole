using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyConsole
{
    public class Player
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool IsFlying { get; set; }
        public int HangTime { get; set; }
        public bool GameOver { get; set; }
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
            if (IsFlying || HangTime < 2)
            {
                if (HangTime == 0)
                {
                    IsFlying = false;
                    HangTime = 2;
                }
                else
                {
                    if (PositionY == 0)
                    {
                        PositionY = 0;
                    }
                    else
                    {
                        PositionY--;
                    }

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
