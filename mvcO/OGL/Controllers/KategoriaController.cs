using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repozytorium.Models;
using Repozytorium.IRepo;
using Repozytorium.Models.View;

namespace OGL.Controllers
{
    public class KategoriaController : Controller
    {
        private readonly IKategoriaRepo _repo;
        public KategoriaController(IKategoriaRepo repo)
        {
            _repo = repo;
        }
        // GET: Kategoria
        public ActionResult Index()
        {
            var kategorie = _repo.PobierzKategorie().AsNoTracking();
            return View(kategorie);
        }

        public ActionResult PokazOgloszenia(int id)
        {
            var ogloszenia = _repo.PobierzOgloszeniaZKategorii(id);
            OgloszeniaZKategoriiViewModels model = new OgloszeniaZKategoriiViewModels();
            model.Ogloszenia = ogloszenia.ToList();
            model.NazwaKategorii = _repo.NazwaDlaKategorii(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult PobierzKategoriePrzyDodawaniuOgloszenia()
        {
            var kateogorie = _repo.PobierzKategorie().ToList();
            return Json(kateogorie, JsonRequestBehavior.AllowGet);
        }

    }
}
