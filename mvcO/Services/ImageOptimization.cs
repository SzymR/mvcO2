using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace Services
{
    public static class ImageOptimization
    {
        public static ImageExtension GetImageExtension(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM"); // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");
            var png = new byte[] {127, 80, 78, 71};
            var jpeg = new byte[] { 255, 216, 255, 224 };
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
            {
                return ImageExtension.bmp;
            }
            if (gif.SequenceEqual(bytes.Take(gif.Length)))
            {
                return ImageExtension.gif;
            }
            if (png.SequenceEqual(bytes.Take(png.Length)))
            {
                return ImageExtension.png;
            }
            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
            {
                return ImageExtension.jpeg;
            }
            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
            {
                return ImageExtension.jpeg;
            }
            return ImageExtension.unknown;
        }

        // sprawdzanie czy obraz jest poprawnego typu
        public static bool ValidateImage(byte[] image)
        {
            if (GetImageExtension(image) != ImageExtension.unknown) { return true; }
            return false;
        }

        // metoda zmieniajaca rozmiar zdjec przy pomocy biblioteki ImageResizer
        public static byte[] OptimizeImageFromBytes(int imgWidth, int imgHeight, byte[] imgBytes)
        {
            var settings = new ResizeSettings
            {
                MaxWidth = imgWidth,
                MaxHeight = imgHeight
            };
            MemoryStream ms = new MemoryStream();
            ImageBuilder.Current.Build(imgBytes, ms, settings);
            return ms.ToArray(); // zwraca pomniejszone zdjecie w postaci tablicy bajtow
        }

    }
}