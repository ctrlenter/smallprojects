using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;

namespace LittleGame.States
{
    public class GameState : State
    {
        public Ship Ship;

        public GameState(Ship ship)
        {
            Ship = ship;
        }

        public override void DrawScreen()
        {
            WriteLine(Game.Seperator);
            WriteLine("What would you like to do?");
            WriteLine("[I]nventory");
            WriteLine("[Q]uests");
            WriteLine("[C]rew stats");
            WriteLine("[S]pace collect");
            WriteLine(Game.Seperator);
            Write(">> ");
            base.DrawScreen();
        }

        public override void HandleInput(string command)
        {
            base.HandleInput(command);

            switch (command.ToLower())
            {
                case "i":
                    Game.Instance.SwitchState(Game.Instance.inventoryState);
                    break;
                case "q":
                    break;
                case "c":
                    break;
                case "s":
                    SpaceCollect();
                    break;
            }
        }

        private void SpaceCollect()
        {
            int collectCounter = 0;
            while(collectCounter != 4)
            {
                int progress = 100 / collectCounter;
                collectCounter++;
                //TODO: generate resources
                WriteLine("");
                Thread.Sleep(5000);
            }
        }
    }
}
