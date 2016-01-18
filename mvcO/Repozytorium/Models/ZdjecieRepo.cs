using Repozytorium.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repozytorium.Models
{
    public class ZdjecieRepo : IZdjecieRepo
    {
        private IOglContext _db;
        public ZdjecieRepo(IOglContext db)
        {
            _db = db;
        }


        public void AddImage(Zdjecie img)
        {
            _db.Zdjecia.Add(img);
        }

        public void DeleteImageByBlobName(string blobName)
        {
            Zdjecie img = _db.Zdjecia.Where(x => x.Name == blobName).FirstOrDefault();
            _db.Zdjecia.Remove(img);
        }

        public List<Zdjecie> GetAllImages(string userId)
        {
            return _db.Zdjecia.Where(x => x.UzytkownikId == userId).ToList();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}