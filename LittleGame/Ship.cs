using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LittleGame
{
    public class Ship
    {
        public int Fuel;
        public int MaxFuel;
        public int Crew;
        public int MaxCrew;

        public List<Item> Inventory = new List<Item>();

        public Ship()
        {
            MaxFuel = 100;
            Fuel = MaxFuel;
            MaxCrew = 10;
            Crew = 1;
        }

    }
}
