using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

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
            WriteLine("[I]nventory");
            WriteLine("[Q]uests");
            WriteLine("[C]rew stats");
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
            }

        }
    }
}
