using System;
using System.Timers;

namespace LittleGame
{
    public class Game
    {

        private const string Seperator = "<----------->";


        public Ship ship;

        bool running = true;

        public Game()
        {
            Log("Starting game!");
            ship = new Ship();


        }


        public void Run()
        {

            while (running)
            {
                Console.Clear();

                Log(Seperator);
                Log("[T]asks");
                Log("[Q]uests");
                Log("[I]nventory");
                Log(Seperator);
                Log(">> ", false);

                //Todo: Update
                HandleInput();

            }

        }

        private void HandleInput()
        {
            var cmd = Console.ReadLine().ToLower();
            if (cmd == "q") running = false;

        }


        public static void Log(object message, bool newLine = true)
        {
            if (newLine)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }
        }
    }
}