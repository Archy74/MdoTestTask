using System;
using System.Drawing;
using System.IO;

namespace Data
{
    public class Picture
    {
        public int id;
        public string Name;
        public string Format;
        public byte[] Data;

        public static byte[] ToByte(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image ToImage(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private static bool ThumbnailCallback()
        {
            return false;
        }

        public Image GetThumbnail(int height, int width)
        {
            Image.GetThumbnailImageAbort myCallback = ThumbnailCallback;
            Bitmap myBitmap = (Bitmap)ToImage(Data);
            return myBitmap.GetThumbnailImage(width, height, myCallback, IntPtr.Zero); ;
        }
    }
}
