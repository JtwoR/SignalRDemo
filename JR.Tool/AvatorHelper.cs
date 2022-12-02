using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace JR.Tool
{
    public static class AvatorHelper
    {
        public static string GetAvatar(string name=null,bool isReSet=false)
        {
            if (!isReSet&&File.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\image", $@"{name}.jpg"))) return $@"{name}.jpg";

            int imageItemSize = 20;

            int width = 180;
            int height = width;
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            ImageFormat format = ImageFormat.Jpeg;

            Bitmap bm = new Bitmap(width, height);
            List<Color> color = new List<Color>() {
                Color.FromArgb(254,67,101),
                Color.FromArgb(252,157,154),
                Color.FromArgb(249,205,173),
                Color.FromArgb(147,224,255),
                Color.FromArgb(200,200,169),
                Color.FromArgb(131,175,155),
                Color.FromArgb(175,215,237),
                Color.FromArgb(255,252,153),
            };

            //List<Color> dcolor = new List<Color>() {
            //    Color.FromArgb(127,255,170)//玉绿
            //};

            Random cRandom = new Random();
            int check=cRandom.Next(0, color.Count);
            Color c1 = color[check];
            color.RemoveAt(check);
            check = cRandom.Next(0, color.Count);
            Color c2 = color[check];

            for (int y = 0; y < height / imageItemSize; y++)
            {
                for (int x = 0; x < width / imageItemSize; x++)
                {

                    Random random = new Random();
                    bool flag = random.Next(0, 10) > 3;

                    for (int j = y * imageItemSize; j < (y + 1) * imageItemSize; j++)
                    {
                        for (int i = x * imageItemSize; i < (x + 1) * imageItemSize; i++)
                        {
                            if (flag)
                            {
                                bm.SetPixel(i, j, c1);
                                bm.SetPixel(width - 1 - i, j, c1);

                            }
                            else
                            {
                                bm.SetPixel(i, j, c2);
                                bm.SetPixel(width - 1 - i, j, c2);
                            }
                        }
                    }
                }
            }
            string setName = string.IsNullOrWhiteSpace(name) ? $@"{timestamp}_{bm.GetHashCode()}.jpg" : $@"{name}.jpg";
            string filename = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\image", setName);

            if (!File.Exists(filename))
            {
                FileStream fs = File.Create(filename);
                bm.Save(fs, format);
                fs.Dispose();
            }
            else
            {
                FileStream fs = File.Open(filename, FileMode.Open);
                bm.Save(fs, format);
                fs.Dispose();
            }

            bm.Dispose();

            return setName;
        }
    }
}
