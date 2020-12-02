using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TextToImage
{
    class Program
    {
        public static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Wrong usage.\nUsage: test <textfile>");
                return;
            }

            if (args[0] == "--dump-colors")
            {
                string clrText = "";
                //Dump colors
                Console.WriteLine("Dumping colors...");
                Type colorType = typeof(System.Drawing.Color);
                // We take only static property to avoid properties like Name, IsSystemColor ...
                PropertyInfo[] propInfos = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
                foreach (PropertyInfo propInfo in propInfos)
                {
                    clrText += propInfo.Name + "\n";
                    Console.WriteLine(propInfo.Name);
                }

                File.WriteAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "colors.txt"), clrText);
                return;
            }
            else if (args[0] == "--resize-image")
            {
                //Needs 2 arguments
                if (args.Length >= 3)
                {
                    Console.WriteLine("args[0]: " + args[0]);
                    Console.WriteLine("args[1]: " + args[1]);
                    int scale = 4;
                    var file = args[1];
                    if (int.TryParse(args[2], out int res))
                    {
                        scale = res;
                    }
                    Console.WriteLine("Scale: " + scale);
                    //get original image
                    //TODO: add more image extensions
                    if (File.Exists(args[1]) && Path.GetExtension(args[1]) == ".png")
                    {
                        Bitmap toResize = new Bitmap(file);
                        Console.WriteLine("Found image to resize");
                        //Get the dimensions of the bitmap
                        Size size = new Size()
                        {
                            width = toResize.Width,
                            height = toResize.Height
                        };

                        Bitmap newBmp = ResizeImage(toResize, size.width * scale, size.height * scale); //for now, upscale 4 times

                        string newFileName = args[1];

                        newFileName = newFileName.Insert(0, "(Resized)");

                        newBmp.Save(newFileName);
                    }
                }
                else
                {
                    Console.WriteLine("Wrong usage.\nUsage: TextToImage.exe --resize-image <filename> <scale>" +
                        "\n<filename>: the file you want to resize" +
                        "\n<scale>: the scale in which you want to resize (integer value)");
                    return;
                }
                return;
            }


            int offset = 0;

            Dictionary<char, Color> colors = new Dictionary<char, Color>();
            List<string> lines = new List<string>();
            var text = File.ReadAllLines(args[0]);

            for (var i = 0; i < text.Length; i++)
            {
                var line = text[i];
                int idx = line.IndexOf('=');
                if (idx == 1)
                {
                    //add one to offset
                    offset++;
                    //Console.WriteLine(line.Substring(idx + 1, 1));
                    if (line.Substring(idx + 1, 1) == "-")
                    {
                        //add it as a color from Argb
                        Color cl = Color.FromArgb(int.Parse(line.Substring(idx + 1)));
                        //Console.WriteLine(cl);
                        colors.Add(line.Substring(0, 1)[0], cl);
                    }
                    else
                    {
                        colors.Add(line.Substring(0, 1)[0], Color.FromName(line.Substring(idx + 1)));
                    }
                    continue;
                }
                if (line.StartsWith(";"))
                {
                    offset++;
                    continue;
                }

                lines.Add(line);

            }

            Console.WriteLine("offset: " + offset);
            Console.WriteLine("pairs: " + colors.Count);
            Console.WriteLine("Lines: " + lines.Count);

            int width = 0;
            int height = lines.Count;

            for (var i = 0; i < lines.Count; i++)
            {
                if (lines[i].Length > width) width = lines[i].Length;
            }

            Console.WriteLine($"Wanted Width:{width}\nWanted Height:{height}");

            Bitmap bmp = new Bitmap(width, height);
            Console.WriteLine("Bmp width / height: " + bmp.Width + " , " + bmp.Height);

            for (int i = 0; i < height; i++)
            {
                //if (i < offset) continue;
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char at = lines[i][j];
                    if (colors.ContainsKey(at))
                    {
                        bmp.SetPixel(j, i, colors[at]);
                    }

                }
            }

            bmp.Save(args[0] + ".png");

        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }

    public class Size
    {
        public int width;
        public int height;
    }
}
