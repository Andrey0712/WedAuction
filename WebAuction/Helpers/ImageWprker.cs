﻿using System.Drawing;

namespace WebAuction.Helpers
{
    public static class ImageWprker
    {
        public static Bitmap FromBase64StringToImage(this string base64String)
            {
            byte[] byteBuffer=Convert.FromBase64String(base64String);
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(byteBuffer))
                {
                    memoryStream.Position = 0;
                    Image imgReturn;
                    imgReturn = Image.FromStream(memoryStream);
                    memoryStream.Close();
                    byteBuffer = null;
                    return new Bitmap(imgReturn);
                }
            }
            catch { return null; }
            }
    }
}
