using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HorseRacing
{
    public class Horse
    {
        public Random rnd = new Random();
        int lottery;
        int positionX;
        int positionY;
        string name;


        public Horse(int posx, int posy, string name) {
            lottery = 1;
            positionX = posx;
            positionY = posy;
            this.name = name;
        }
        public bool Move()
        {
            if (lottery == rnd.Next(1, 1001))
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.Write("-");
                positionX += 1;
                if (positionX == 57)
                {
                    Console.SetCursorPosition(28, 4);
                    Console.Write($"{name}");
                    return true;
                }
                return false;
            }
            return false;
        }

    }
}
