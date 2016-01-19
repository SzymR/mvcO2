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

            model.dataDodania = DateTime.Now;
            _repo.DodajWiadomosc(model);

            TempData["Message"] = "Dodano wiadomosc! Gratulacje !";
            return RedirectToAction("MojeOgloszenia", "Ogloszenie");
        }
        [HttpGet]
        public ActionResult GetWiadomosci()
        {

            var model = _repo.PobierzWiadomosc();
            return View(model); ;
        }

        [HttpGet]
        public ActionResult SlowaZakazane()
        {
            var model = _repo.PobierzSlowaZakazane();
            return View(model); ;
        }

        [HttpGet]
        public ActionResult CreateSlowZakazane()
        {
            
            return View(); 
        }

        [HttpPost]
        public ActionResult CreateSlowZakazane(ZakazaneSlowo slowo)
        {
           
            _repo.dodajSlowoZakazane(slowo);
            TempData["Message"] = "Dodano słowo! Gratulacje !";
            return RedirectToAction("SlowaZakazane", "Admin");
        }

   
        public ActionResult Delete(int? id)
        {

            _repo.UsunSlowo(id.Value);
            TempData["Message"] = "Usunięto słowo! Gratulacje !";
            return RedirectToAction("SlowaZakazane", "Admin");
        }


    }
}