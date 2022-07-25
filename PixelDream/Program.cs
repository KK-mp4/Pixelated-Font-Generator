namespace PixelDream
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public class Program
    {
        public static void Main(string[] args)
        {   
            // 3x4, 3x5, 4x4
            int fontWidth = 3;
            int fontHeight = 4;
            Console.WriteLine($"{fontWidth}x{fontHeight} Font");
            int dimentions = Convert.ToInt32(Math.Ceiling(Math.Sqrt(Math.Pow(2, fontWidth * fontHeight))));
            Console.Write($"Image will be {dimentions}x{dimentions} cells ");
            int width = dimentions * fontWidth;
            int height = dimentions * fontHeight;
            Console.WriteLine($"or {width}x{height} pixels");

            Bitmap img = new(width, height);
            using (Graphics g = Graphics.FromImage(img))
            {
                using (SolidBrush brush = new(Color.FromArgb(0, 0, 0)))
                {
                    g.FillRectangle(brush, 0, 0, width, height);
                }
            }

            GenFull(img, fontHeight, height, fontWidth, width);

            //FindClosestMatch(fontWidth, fontHeight);

            img.Save($"../../../../{width}x{height}_RGB.png", ImageFormat.Png);
            Console.WriteLine($"Saved {width}x{height}.png");
        }

        public static void FindClosestMatch(int fontWidth, int fontHeight)
        {
            int scaledWidth = 20;
            int scaledHeight = 20;
            Bitmap img = new(fontWidth, fontHeight);
            Bitmap img2 = new Bitmap(@"C:\WIP Projects\PixelDream\W.png");

            double minDiff = 9999999;
            int minCount = 0;
            for (int counter = 0; counter < 4096; counter++)
            {
                img.Dispose();
                img = new(fontWidth, fontHeight);
                using (Graphics g = Graphics.FromImage(img))
                {
                    using (SolidBrush brush = new(Color.FromArgb(0, 0, 0)))
                    {
                        g.FillRectangle(brush, 0, 0, fontWidth, fontHeight);
                    }
                }

                PaintGlyph(img, 0, 0, fontWidth, fontHeight, counter);
                img = ResizeImage(img, scaledWidth, scaledHeight);

                double diff = 0;
                for (int j = 0; j < scaledHeight; j++)
                {
                    for (int i = 0; i < scaledWidth; i++)
                    {
                        double pix1 = img.GetPixel(j, i).GetBrightness();
                        double pix2 = img2.GetPixel(j, i).GetBrightness();
                        diff += Math.Abs(pix2 - pix1);
                    }
                }

                if (diff <= minDiff)
                {
                    minDiff = diff;
                    minCount = counter;
                    Console.WriteLine($"New closest: {counter} with value {minDiff}");
                }
            }
            img = new(fontWidth, fontHeight);
            using (Graphics g = Graphics.FromImage(img))
            {
                using (SolidBrush brush = new(Color.FromArgb(0, 0, 0)))
                {
                    g.FillRectangle(brush, 0, 0, fontWidth, fontHeight);
                }
            }
            PaintGlyph(img, 0, 0, fontWidth, fontHeight, minCount);
            img.Save($"../../../../Output2.png", ImageFormat.Png);
            img = ResizeImage(img, scaledWidth, scaledHeight);
            img.Save($"../../../../Output sized2.png", ImageFormat.Png);
            img.Dispose();
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
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

        public static void GenFull(Bitmap img, int fontHeight, int height, int fontWidth, int width)
        {
            int counter = 0;
            for (int j = 0; j < height; j += fontHeight)
            {
                for (int i = 0; i < width; i += fontWidth)
                {
                    PaintGlyph(img, i, j, fontWidth, fontHeight, counter);
                    counter++;
                }
            }
        }

        public static void PaintGlyph(Bitmap img, int j, int i, int x, int y, int value)
        {
            BitArray bitArray = new BitArray(new int[] { value });

            Color color = GetColor();
            //Color color = Color.White;

            int counter = 0;
            for (int i2 = i; i2 < i + y; ++i2)
            {
                for (int j2 = j; j2 < j + x; ++j2)
                {
                    if (bitArray[counter] == true) 
                    {
                        img.SetPixel(j2, i2, color);
                    }
                    counter++;
                }
            }
        }

        public static Color GetColor()
        {
            Random Rand = new Random();

            Color color;
            int option = Rand.Next(3);

            if (option == 0)
            {
                color = Color.FromArgb(255, Rand.Next(256), Rand.Next(256));
            }
            else if (option == 1)
            {
                color = Color.FromArgb(Rand.Next(256), 255, Rand.Next(256));
            }
            else
            {
                color = Color.FromArgb(Rand.Next(256), Rand.Next(256), 255);
            }

            return color;
        }
    }
}