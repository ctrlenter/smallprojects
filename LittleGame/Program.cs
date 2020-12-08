using System;
using System.Timers;

namespace LittleGame
{
    class Program
    {
        public static bool Running;

        static void Main(string[] args)
        {

            GameData.Init();

            Game game = new Game();

            game.Run();

        }

        private static void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("poggers");
        }
    }
}
