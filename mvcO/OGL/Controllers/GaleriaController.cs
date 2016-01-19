using Repozytorium.IRepo;
using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Services;

namespace OGL.Controllers
{
    public class GaleriaController : Controller
    {
        private IZdjecieRepo _zdjecieRepo;
        public GaleriaController(IZdjecieRepo zdjecieRepo)
        {
            _zdjecieRepo = zdjecieRepo;
        }
        // GET: Galeria
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lista()
        {
            List<Zdjecie> zdjecia = _zdjecieRepo.GetAllImages(User.Identity.GetUserId());
            return View(zdjecia);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(HttpPostedFileBase fileBase)
        {
            if (fileBase != null && fileBase.ContentLength > 0)
            {
                try
                {
                    ImageUpload imageUpload = new ImageUpload();
                    string nameWithExtension = imageUpload.UploadImageAndReturnImageName(fileBase);
                    if (nameWithExtension == null)
                        return RedirectToAction("Lista", "Galeria");
                    try
                    {
                        Zdjecie img = new Zdjecie()
                        {
                            UzytkownikId = User.Identity.GetUserId(),
                            Name = nameWithExtension
                        };
                        _zdjecieRepo.AddImage(img);
                        _zdjecieRepo.SaveChanges();
                    }
                    catch
                    {
                        imageUpload.DeleteImageByNameWithMiniatures(nameWithExtension);
                    }
                }
                catch { }
            }
            return RedirectToAction("Lista", "Galeria");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool DeleteImages(string blobName)
        {
            if (blobName == null)
            {
                return false;
            }
            try{
                ImageUpload imageUpload = new ImageUpload();
                imageUpload.DeleteImageByNameWithMiniatures(blobName);
                try{
                    _zdjecieRepo.DeleteImageByBlobName(blobName);
                    _zdjecieRepo.SaveChanges();
                    return true;
                }
                catch{
                    return false;
                }
            } catch{
                return false;
            }
        }
    }
}