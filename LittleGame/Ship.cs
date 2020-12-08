using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LittleGame
{
    public class Ship
    {
        public int Fuel;
        public int MaxFuel;
        public int Crew;
        public int MaxCrew;
        public int Coins;

        public List<Item> Inventory = new List<Item>();

        public Ship()
        {
            MaxFuel = 100;
            Fuel = MaxFuel;
            MaxCrew = 10;
            Crew = 1;
            Coins = 100;

            AddItem(GameData.SteelBar, 69);
            AddItem(GameData.SteelBar, 5);
        }

        public void AddItem(Item item, int count)
        {
            Item it = null;

            var byName = FindItemsByName(item.Name);
            if(byName.Count > 0)
            {
                //this means we found item with name.
                for(var i = 0; i < byName.Count; i++)
                {
                    var at = byName[i];
                    if (at.Count == item.MaxCount) continue;

                    //get current stack
                    var withAdded = at.Count + count;
                    if (withAdded > item.MaxCount)
                    {
                        int rest = count % item.MaxCount;
                        int extraStacks = (int)Math.Floor((double)count / item.MaxCount);
                        for (int j = 0; j < extraStacks; j++)
                        {
                            var copy = item.Copy();
                            copy.Add(item.MaxCount);
                            Inventory.Add(copy);
                        }
                        var remainder = item.Copy();
                        remainder.Add(rest);
                        Inventory.Add(remainder);
                    }
                }
            }

            if (count > item.MaxCount)
            {
                int rest = count % item.MaxCount;
                int extraStacks = (int)Math.Floor((double)count / item.MaxCount);
                for (int i = 0; i < extraStacks; i++)
                {
                    var copy = item.Copy();
                    copy.Add(item.MaxCount);
                    Inventory.Add(copy);
                }
                var remainder = item.Copy();
                remainder.Add(rest);
                Inventory.Add(remainder);

            }

            /*
            if ((it = Inventory.Find(i => i.Name == item.Name)) != null)
            {
                //TODO: Optimize this. it looks like a mess
                int totalCount = it.Count + count;
                if (totalCount > item.MaxCount)
                {
                    Inventory.Add(it.Copy());
                }
                else
                {
                    it.Add(count);
                }

                //TODO: Check if the count is bigger than stacksize. if it is, add a new stack with the remaining
            }
            else
            {
                copy.Add(count);
                Inventory.Add(copy);
            }
            */
        }

        public void TakeItem(Item item, int count)
        {
            Item it = null;
            if ((it = Inventory.Find(i => i.Name == item.Name)) != null)
            {
                it.Count -= count;
                if (it.Count <= 0) Inventory.Remove(it);
            }
        }

        public List<Item> FindItemsByName(string name)
        {
            return Inventory.FindAll(r => r.Name == name);
        }

    }
}
