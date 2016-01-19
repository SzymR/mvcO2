using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Services
{
    public static class StreamHelpers
    {
        // Statyczna metoda zamienia ciąg wejściowy Stream na tablice bajtów a więc odczytuje 
        // cały plik i zapisuje w tablicy. Ułatwia to dalszą obróbkę zdjęcia, ponieważ 
        // nie pracujemy już na strumieniu danych, ale na całym już wczytanym zdjęciu. 
        public static byte[] ReadFully(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}