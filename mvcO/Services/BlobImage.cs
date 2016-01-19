using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services
{
    public class BlobImage
    {
        // folder/typ_wielkosci/nazwa_obrazka
        // zdjecia/small/1.jpg

        public byte[] ImgBytes;
        public string SizeName;
        public string ImageName;
    }
}