using LittleGame.States;
using System;
using System.Timers;

namespace LittleGame
{
    public class Game
    {

        public const string Seperator = "<----------->";


        public Ship ship;
        public static Game Instance;

        public GameState gameState;
        public InventoryState inventoryState;

        public State currentState;

        bool running = true;

        public Game()
        {
            Instance = this;

            Log("Starting game!");
            
            ship = new Ship();

            gameState = new GameState(ship);
            inventoryState = new InventoryState(ship);

            currentState = gameState;

        }


        public void Run()
        {

            while (running)
            {
                Console.Clear();
                
                currentState.DrawScreen();

                var cmd = Console.ReadLine();
                currentState.HandleInput(cmd);
            }

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

        public void SwitchState(State state)
        {
            currentState = state;
        }
    }
}