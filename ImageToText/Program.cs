using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ImageToText
{
    class Program
    {
        static Dictionary<Color, char> charList = new Dictionary<Color, char>();
        static List<string> strings = new List<string>();

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Beep boop boop.");
                return;
            }
            else if (args.Length == 1)
            {
                var filename = args[0];
                Console.WriteLine("File exists: " + File.Exists(filename));
                Console.WriteLine("File extension: " + Path.GetExtension(filename));
                if (File.Exists(filename) && Path.GetExtension(filename) == ".png")
                {
                    //make a bitmap from the file
                    Bitmap bmp = new Bitmap(filename);
                    int w = bmp.Width;
                    int h = bmp.Height;
                    for (int x = 0; x < w; x++)
                    {
                        for (int y = 0; y < h; y++)
                        {
                            Color at = bmp.GetPixel(x, y);
                            AddChar(at);
                        }
                    }

                    foreach(var thing in charList)
                    {
                        var str = $"{thing.Value}={thing.Key.ToArgb()}";
                        strings.Add(str);
                    }

                    strings.Add(";");

                    Console.WriteLine("Added " + charList.Keys.Count + " colors");

                    //once all characters thats needed have been added,
                    //loop over the image, getting each pixel at x and y.
                    //if the color is found in the dictionary, add the character to a string.
                    for(int y = 0; y < h; y++)
                    {
                        string currString = "";
                        for(int x = 0; x < w; x++)
                        {
                            Color at = bmp.GetPixel(x, y);
                            if (charList.ContainsKey(at))
                            {
                                //get the character
                                char chAt = charList[at];
                                Console.WriteLine("Character " + chAt + " at color " + at);
                                //add it to the current string
                                currString += chAt;
                            }
                        }
                        Console.WriteLine(currString);
                        strings.Add(currString);
                    }

                    File.WriteAllLines(filename + ".txt", strings);

                }
            }
        }

        static char GetRandomCharacter()
        {
            string alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!#$%&*+=?@~";
            char ch = alphabet[new Random().Next(alphabet.Length)];
            return ch;
        }


        static int recCounter = 0;
        static void AddChar(Color at)
        {
            if (recCounter >= 10)
            {
                Console.WriteLine("Tried getting random character 10 times..");
                return;
            }

            if(at == Color.Transparent)
            {
                charList.Add(at, ' ');
                return;
            }

            if (!charList.ContainsKey(at))
            {
                char randomChar = GetRandomCharacter();
                if (!charList.ContainsValue(randomChar))
                    charList.Add(at, randomChar);
                else
                {
                    AddChar(at);
                    recCounter++;
                }
            }
        }
    }
}
