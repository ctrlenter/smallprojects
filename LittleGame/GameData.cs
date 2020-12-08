using System;
using System.Collections.Generic;
using System.Text;

namespace LittleGame
{
    public class GameData
    {
        #region items
        public static Item Bolt { get; private set; }
        public static Item SteelPipe { get; private set; }
        public static Item IronBar { get; private set; }
        public static Item GoldBar { get; private set; }
        public static Item SteelBar { get; private set; }
        public static Item Hammer { get; private set; }
        

        #endregion

        public static void Init()
        {
            Bolt = new Item("Bolt", 24);
            SteelPipe = new Item("Steel Pipe", 16);
            IronBar = new Item("Iron Bar", 30);
            GoldBar = new Item("Gold Bar", 30);
            SteelBar = new Item("Steel Bar", 30);
            Hammer = new Item("Hammer", 1);
        }
    }
}
