using System;
using System.Collections.Generic;

namespace Abbreviator
{
    class Program
    {
        static List<char> chars = new List<char>();
        static bool running = false;
        static void Main(string[] args)
        {

            running = true;

            while (running)
            {
                TextMani();
            }

        }

        static void TextMani()
        {
            Console.WriteLine("What do you want to abbreviate?");
            Console.Write(">> ");
            var toAbbr = Console.ReadLine();

            if (toAbbr.ToLower() == "quit" || toAbbr.ToLower() == "q")
            {
                running = false;
                return;
            }

            var split = toAbbr.Split(" ");

            for (int i = 0; i < split.Length; i++)
            {
                //get current string
                string current = split[i];
                char firstChar = current.Length > 0 ? current[0] : ' ';
                chars.Add(firstChar);
            }

            string str = "";

            foreach (var ch in chars)
            {
                str += $"{char.ToUpper(ch)}.";
            }
            Console.WriteLine("Here's your abbreviated text!");
            Console.WriteLine(str);
            chars.Clear();

        }

    }
}
