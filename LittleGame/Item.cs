namespace LittleGame
{
    public class Item
    {

        public string Name { get; set; }
        public int Count { get; set; }
        public int MaxCount { get; set; }


        public Item()
        {

        }

        public Item(string name, int stacksize)
        {
            Name = name;
            MaxCount = stacksize;
            
        }

        public void Add(int amt)
        {
            Count += amt;
        }

        public void Take(int amt)
        {
            Count -= amt;
            if (Count < 0) Count = 0;
        }

        public Item Copy()
        {
            Item item = new Item();
            item.Name = Name;
            item.Count = Count;
            item.MaxCount = MaxCount;

            return item;
        }

    }
}