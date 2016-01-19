using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Services
{
    public class ImageUpload
    {
        // metoda generuje nazwe bloba - zdjecia
        // zarzadzanie tym w jaki sposob beda nadawane nazwy plikom
        static string CreateBlobName()
        {
            return System.Guid.NewGuid().ToString();
        }
        // metoda zwracajaca calkowita siezke do pliku
        // w nezwie mozna umiescic tez idUzytkownika
        public static string GetFullBlobName(string sizeName, string imageNameWithExtension)
        {
            return sizeName + "/" + imageNameWithExtension;
        }
        // metoda wywolywana w kontrolerze, aby zapisac zdjecie w chmurze, 
        // jesli zapis sie powiedzie zwraca nazwe pliku, jezeli nie to null
        public string UploadImageAndReturnImageName(HttpPostedFileBase fileBase)
        {
            byte[] image = fileBase.InputStream.ReadFully(); // zaladowanie calego pliku
            if (!ImageOptimization.ValidateImage(image)) // walidacja
                return null;

            List<BlobImage> imagesToUpload = GenerateImageMiniatures(image); // twory liste miniaturek dla zdjecia
            try
            {
                UploadMultipleImagesToBlob(imagesToUpload); // zapisanie zdjecia razem z miniaturkami w chmurze
            }
            catch
            {
                return null;
            }
            // zwraca tylko nazwe pierwszego poniewaz wszystkie sa jednakowe, roznia sie jedynie sciezka
            return imagesToUpload.First().ImageName;
        }

        // generuje miniaturki na podstawie listy wymiarow GalleryDimensionsList z klasy GalleryImages
        // przyjmuje zdjecie w postaci tablicy bytow i zwraca liste miniaturek 
        List<BlobImage> GenerateImageMiniatures(byte[] image)
        {
            List<BlobImage> imagesToUpload = new List<BlobImage>(); // tworzenie puste listy zdjec
            string blobName = CreateBlobName(); // generujemy nazwe dla zdjecia

            foreach (var img in GalleryImages.GalleryDimensionsList) // dla kazdego wymiaru, zdjecie jest skalowane do wybranych rozmiarow
            {
                byte[] imgBytes = ImageOptimization.OptimizeImageFromBytes(img.Width, img.Height, image);
                BlobImage blobImage = new BlobImage(){
                    ImgBytes = imgBytes,
                    SizeName = img.SizeName,
                    ImageName = blobName + "."+ImageOptimization.GetImageExtension(imgBytes).ToString()
                };
            imagesToUpload.Add(blobImage);
            }
            return imagesToUpload;
        }

        // zapisuje w chmurze liste zdjec

        void UploadMultipleImagesToBlob(List<BlobImage> images)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]); // polaczenie z kontem Azure przy pomocy web.config
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            foreach (var img in images)
            {
                CloudBlobContainer container = blobClient.GetContainerReference("zdjecia"); // polaczenie z kontenerem o nazwie zdjecia
                if (container.CreateIfNotExists()) // jezeli nie istenieje to go tworzymy
                {
                    var permissions = container.GetPermissions();
                    permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                    container.SetPermissions(permissions); // ustawienie dostepu na public
                }
                string blobName = GetFullBlobName(img.SizeName, img.ImageName); // gen pelna nazwe i typ
                CloudBlockBlob blockBlop = container.GetBlockBlobReference(blobName); // 
                blockBlop.UploadFromByteArray(img.ImgBytes, 0, img.ImgBytes.Length); // zapisanie w chmurze
            }
        }

        public void DeleteImageByNameWithMiniatures(string imageNameWithExtension)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("zdjecia");
            foreach (var img in GalleryImages.GalleryDimensionsList)
            {
                DeleteImageByName(container, GetFullBlobName(img.SizeName, imageNameWithExtension));
            }
        }

        // przyjmuje parametr kontener z ktorego ma byc usuniete zdjecie oraz nazwe
        void DeleteImageByName(CloudBlobContainer container, string blobName)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.DeleteIfExists();
        }

    }
}