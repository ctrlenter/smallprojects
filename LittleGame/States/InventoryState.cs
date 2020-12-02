using System;
using System.Collections.Generic;
using System.Text;

namespace LittleGame.States
{
    public class InventoryState : State
    {

        int pageIndex = 1;

        public Ship Ship;
        public InventoryState(Ship ship)
        {
            Ship = ship;
        }

        public override void DrawScreen()
        {

        }

    }
}
