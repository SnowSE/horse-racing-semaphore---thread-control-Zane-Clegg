using System;
using System.Collections.Generic;
using System.Threading;

namespace HorseRacing
{
    class Program
    {
        //Todo: 1) Create 3 threads (one per horse)
        //Todo: 2) establish 3 AutoResetEvents  (one for each child thread).
        //Todo: 3) The main thread will then send a pulse out to all 3 threads by calling .Set()
        //Todo: 4) Each Horse thread will wait for a signal from main, and upon hearing it:
        //      4a) run a lottery 1/1000
        //      4b) if lottery won, then advance their horse 1 space (put a '-' on the screen)
        //Todo: 5) The first horse to travel 50 spaces is pronounced the winner
        //Todo: 6) 2 - All kids die appropriately  (hint - how did we do this in Matrix??)

        static SemaphoreSlim semaphore = new SemaphoreSlim(1,1);
        static bool horseWon = false;
        public static void HThread(Horse h)
        {
            while (true)
            {
                semaphore.Wait();
                if (horseWon)
                {
                    semaphore.Release();
                    return;
                }
                if (h.Move() == true)
                {
                    horseWon = true;
                }
                semaphore.Release();
                if (horseWon)
                {
                    return;
                }
            }
        }
        public static void DrawScreen()
        {
            string[] names = new string[3] { "HorseA", "HorseB", "HorseC" };
            Random rnd = new Random();
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("HorseA:");
            Console.WriteLine("HorseB:");
            Console.WriteLine("HorseC:");
            List<Thread> horses = new List<Thread>();

            for (int i = 0; i < 3; i++)
            {
                Horse h = new Horse(7,i+1, names[i]);
                horses.Add(new Thread(()=>HThread(h)));
            }

            foreach (Thread t in horses)
            {
                t.Start();
            }
            foreach (Thread t in horses)
            {
                t.Join();
            }
        }

        static void Main(string[] args)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Welcome to the Racing Program:");
            DrawScreen();
            Console.SetCursorPosition(0, 4);
            Console.WriteLine("The winner of this race was");
        }
    }
}
