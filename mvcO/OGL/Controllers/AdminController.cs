using Repozytorium.IRepo;
using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OGL.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepo _repo;
        public AdminController(IAdminRepo repo)
        {
            _repo = repo;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult CreateWiadomosc()
        {

           /// model.dataDodania = DateTime.Now();
           /// _repo.DodajWiadomosc(model);

          ///////  TempData["Message"] = "Dodano wiadomosc! Gratulacje !";
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult CreateWiadomosc(Wiadomosc model)
        {
            if (ModelState.IsValid)
            {
                model.dataDodania = DateTime.Now;
                _repo.DodajWiadomosc(model);

                TempData["Message"] = "Dodano wiadomosc! Gratulacje !";
                return RedirectToAction("GetWiadomosci", "Admin");
            }
            else
                return View(model);
        }
        [HttpGet]
        public ActionResult GetWiadomosci()
        {

            var model = _repo.PobierzWiadomosc();
            return View(model); ;
        }

        [HttpGet]
        [Authorize]
        public ActionResult SlowaZakazane()
        {
            var model = _repo.PobierzSlowaZakazane();
            return View(model); ;
        }
        [HttpGet]
        public ActionResult DozwoloneZnacznikiHtml()
        {
            var model = _repo.PobierzDozwoloneZnacznikiHtml();
            return View(model); ;
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateSlowZakazane()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateSlowZakazane(ZakazaneSlowo slowo)
        {
            if (ModelState.IsValid)
            {
                _repo.dodajSlowoZakazane(slowo);
                TempData["Message"] = "Dodano słowo! Gratulacje !";
                return RedirectToAction("SlowaZakazane", "Admin");
            }
            else
                return View(slowo);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                _repo.UsunSlowo(id.Value);
                TempData["Message"] = "Usunięto słowo! Gratulacje !";
                return RedirectToAction("SlowaZakazane", "Admin");
            }
            else
            {
                TempData["Message"] = "Coś poszło nie tak przy usuwaniu słowa !";
                return RedirectToAction("SlowaZakazane", "Admin");
            }
        }

        [HttpGet]
        public ActionResult CreateZnacznikHTML()
        {

            return View();
        }


        [HttpPost]
        public ActionResult CreateZnacznikHTML(DozwolonyZnacznikHtml znacznikk)
        {

            _repo.dodajZnacznikHtml(znacznikk);
            TempData["Message"] = "Dodano nowy znacznik! Gratulacje !";
            return RedirectToAction("DozwoloneZnacznikiHtml", "Admin");
        }

        public ActionResult DeleteZnacznik(int? id)
        {

            _repo.UsunZnacznikHtml(id.Value);
            TempData["Message"] = "Usunięto znacznik! Gratulacje !";
            return RedirectToAction("DozwoloneZnacznikiHtml", "Admin");
        }
     


    }
}