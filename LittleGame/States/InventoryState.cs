using System;
using System.Collections.Generic;
using System.Text;

namespace LittleGame.States
{
    public class InventoryState : State
    {

        int pageIndex = 0;

        int MaxPages
        {
            get
            {
                return (int)Math.Floor((double)(Ship.Inventory.Count / 5));
            }
        }

        public Ship Ship;
        public InventoryState(Ship ship)
        {
            Ship = ship;
        }
        //(int) Math.Ceiling((double)(Ship.Inventory.Count / 5))
        public override void DrawScreen()
        {
            var inv = Ship.Inventory;
            int offset = pageIndex * 5;
            int amount = 5;
            if (offset + amount > inv.Count) amount = inv.Count - offset;
            var ranged = inv.GetRange(offset, amount);

            WriteLine("<-Inventory->");
            WriteLine(Game.Seperator);
            for (var i = 0; i < ranged.Count; i++)
            {
                WriteLine($"{ranged[i].Name} x{ranged[i].Count}");
            }

            WriteLine(Game.Seperator);
            if (inv.Count > 5)
            {
                WriteLine($"<-[L]    [R]->");
            }

        }

        public override void HandleInput(string command)
        {
            //TODO: Wrap around if it goes below max item count
            if (Ship.Inventory.Count > 5)
            {
                switch (command.ToLower())
                {
                    case "r":
                        pageIndex++;
                        break;
                    case "l":
                        pageIndex--;
                        break;
                }

                if (pageIndex < 0) pageIndex = 0;
                if (pageIndex > MaxPages) pageIndex = MaxPages;


            }
        }

    }
}
