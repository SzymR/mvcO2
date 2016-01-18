using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repozytorium.Models;
using System.Diagnostics;
using Repozytorium.IRepo;
using Microsoft.AspNet.Identity;
using PagedList;
using Repozytorium.Models.View;

namespace OGL.Controllers
{
    public class OgloszenieController : Controller
    {
        private readonly IOgloszenieRepo _repo;
        public OgloszenieController(IOgloszenieRepo repo)
        {
            _repo = repo;
        }

        //private OglContext db = new OglContext();
        //OgloszenieRepo repo = new OgloszenieRepo();
        // GET: Ogloszenie
        public ActionResult Index(int? page, string sortOrder)
        {
            int currentPage = page ?? 1;
            int naStronie = 10;


            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSort = String.IsNullOrEmpty(sortOrder) ? "IdAsc" : "";
            ViewBag.DataDodaniaSort = sortOrder == "DataDodania" ? "DataDodaniaAsc" : "DataDodania";
            ViewBag.TrescSort = sortOrder == "TrescAsc" ? "Tresc" : "TrescAsc";
            ViewBag.TytulSort = sortOrder == "TytulAsc" ? "Tytul" : "TytulAsc";
            //ViewBag.idUser = User.Identity.GetUserId();

            var ogloszenia = _repo.PobierzOgloszenia();

            switch (sortOrder)
            {
                case "DataDodania":
                    ogloszenia = ogloszenia.OrderByDescending(s => s.DataDodania);
                    break;
                case "DataDodaniaAsc":
                    ogloszenia = ogloszenia.OrderBy(s => s.DataDodania);
                    break;
                case "Tytul":
                    ogloszenia = ogloszenia.OrderByDescending(s => s.Tytul);
                    break;
                case "TytulAsc":
                    ogloszenia = ogloszenia.OrderBy(s => s.Tytul);
                    break;
                case "Tresc":
                    ogloszenia = ogloszenia.OrderByDescending(s => s.Tresc);
                    break;
                case "TrescAsc":
                    ogloszenia = ogloszenia.OrderBy(s => s.Tresc);
                    break;
                case "IdAsc":
                    ogloszenia = ogloszenia.OrderBy(s => s.Uzytkownik);
                    break;
                default:
                    ogloszenia = ogloszenia.OrderByDescending(s => s.DataDodania);
                    break;
            }

            return View(ogloszenia.ToPagedList<Ogloszenie>(currentPage, naStronie));
        }

        public ActionResult Partial(int? page)
        {
            int currentPage = page ?? 1;
            int naStronie = 3;
            var ogloszenia = _repo.PobierzOgloszenia();
            ogloszenia = ogloszenia.OrderByDescending(d => d.DataDodania);
            return PartialView("Index", ogloszenia.ToPagedList<Ogloszenie>(currentPage, naStronie));
        }

        public ActionResult MojeOgloszenia(int? page)
        {
            int currentPage = page ?? 1;
            int naStronie = 3;
            string userId = User.Identity.GetUserId();
            var ogloszenia = _repo.PobierzOgloszenia();
            ogloszenia = ogloszenia.OrderByDescending(d => d.DataDodania)
                .Where(o => o.UzytkownikId == userId);

            return View(ogloszenia.ToPagedList<Ogloszenie>(currentPage, naStronie));
        }






        [HttpPost]

        public ActionResult DodajOgloszenie([Bind(Include = "Tresc,Tytul")] Ogloszenie advert, string[] category)
        {
            if (ModelState.IsValid)
            {
                // Automatyczne przypisanie Id użytkownika, który dodaje ogłoszenie
                advert.UzytkownikId = User.Identity.GetUserId();
                // Automatyczne przypisanie aktualnej daty jako DataDodania
                advert.DataDodania = DateTime.Now;
                // W razie wystąpienia błędu powrót do widoku dodawania
                try
                {
                    _repo.Dodaj(advert, category);

                    _repo.SaveChanges();



                }
                catch (Exception)
                {
                    return View(advert);
                }

            }

            return null;
        }

        public ActionResult WypelnijAtrybuty(string category, string tytul, string nazwa)
        {
            List<AtrybutZWartosciami> model = new List<AtrybutZWartosciami>();

            string[] _category = category.Split(',');
            foreach (var item2 in _category)
            {
                var atrybuty = _repo.PobierzAtrybutyZKategorii(Convert.ToInt16(item2));


                foreach (var item in atrybuty)
                {
                    AtrybutZWartosciami temp = new AtrybutZWartosciami();
                    temp.atrybut = item;

                    temp.atrybutWartosc = _repo.PobierzWartosciAtrybutowZAtrybutu(item.Id);
                    model.Add(temp);

                }
            }

            return View(model);
        }

        #region MetodyDodawaniaUsuwaniaITP

        #region Details
        // GET: Ogloszenie/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie ogloszenie = _repo.GetOgloszenieById((int)id);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie);
        }
        #endregion

        #region Create

        // GET: Ogloszenie/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ogloszenie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize] // niezalogowani nie moga dodawac ogloszen
        [HttpPost]
        // Zabezpieczenie przez atakami CSRF
        // Podczas tworzenia widoku generowany jest klucz, który przesyłany jest do przeglądarki
        // w formie ukrytego pola, następnie przeglądarka odsyła klucz w treści żądania POST, atrybut sprawdza oba klucze
        // Dla każdego żądania generowany jest inny klucz
        [ValidateAntiForgeryToken]

        // Bind Include aby okreslic ktore dane beda zapisywane, nadmiar zostanie zignorowany
        // Zapobiega przypisanie ogloszenia innemu uzytkownikowi,
        public ActionResult Create([Bind(Include = "Tresc,Tytul")] Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                // Automatyczne przypisanie Id użytkownika, który dodaje ogłoszenie
                ogloszenie.UzytkownikId = User.Identity.GetUserId();
                // Automatyczne przypisanie aktualnej daty jako DataDodania
                ogloszenie.DataDodania = DateTime.Now;
                // W razie wystąpienia błędu powrót do widoku dodawania
                try
                {
                    _repo.Dodaj(ogloszenie, null);
                    _repo.SaveChanges();
                    return RedirectToAction("MojeOgloszenia");
                }
                catch (Exception)
                {
                    return View(ogloszenie);
                }

            }


            return View(ogloszenie);
        }
        #endregion

        #region Edit
        // GET: Ogloszenie/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie ogloszenie = _repo.GetOgloszenieById((int)id);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            // Uzytkownik ktory nie jest wlascicielem ogloszenia nie bedzie mial dostepu do edycji
            else if (ogloszenie.UzytkownikId != User.Identity.GetUserId() &&
                !(User.IsInRole("Admin") || User.IsInRole("Pracownik")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(ogloszenie);
        }

        // POST: Ogloszenie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tresc,Tytul,DataDodania,UzytkownikId")] Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //ogloszenie.UzytkownikId = "dafd";
                    _repo.Aktualizuj(ogloszenie);
                    _repo.SaveChanges();
                }
                catch
                {
                    ViewBag.Blad = true;
                    return View(ogloszenie);
                }
            }
            ViewBag.Blad = false;
            return View(ogloszenie);
        }

        #endregion

        #region Delete
        // GET: Ogloszenie/Delete/5
        [Authorize]
        public ActionResult Delete(int? id, bool? blad)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ogloszenie ogloszenie = _repo.GetOgloszenieById((int)id);

            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            else if (ogloszenie.UzytkownikId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Obsluga bledu usuwania
            // /Ogloszenie/Delete/1?blad=True
            if (blad != null)
                ViewBag.Blad = true;
            return View(ogloszenie);
        }

        //// POST: Ogloszenie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.UsunOgloszenie(id);
            try
            {
                _repo.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Delete", new { id = id, blad = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        #endregion
    }
}
