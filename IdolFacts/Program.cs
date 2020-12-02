using System;
using System.Collections.Generic;
using System.IO;

namespace WordCounter
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Data> data = new List<Data>();

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: WordCounter <filename>");
                return;
            }

            if (args.Length >= 1)
            {
                //check if file exists
                string file = args[0];
                if (File.Exists(file))
                {
                    //read the file
                    var contents = File.ReadAllText(file).Split(" ");
                    //loop thru the contents
                    for (var i = 0; i < contents.Length; i++)
                    {
                        var currWord = contents[i];
                        if (currWord == " ") continue;
                        if (data.Find(r => r.word == currWord) != null)
                        {
                            data.Find(r => r.word == currWord).count++;
                        }
                        else
                        {
                            data.Add(new Data(currWord, 1));
                        }
                    }

                    string output = "";

                    foreach (var d in data)
                    {
                        output += $"Word: {d.word}, count: {d.count}\n";
                    }

                    File.WriteAllText("output-" + file, output);
                    Console.WriteLine("Found " + data.Count + " words.\nOutputting to output-" + file);
                }
            }
        }

        class Data
        {
            public string word;
            public int count;

            public Data(string word, int count)
            {
                this.word = word;
                this.count = count;
            }

            public Data()
            {

            }
        }
    }
}
