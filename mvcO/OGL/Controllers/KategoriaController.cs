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
        private readonly IOgloszenieRepo _repoO;
        public KategoriaController(IKategoriaRepo repo, IOgloszenieRepo repoO)
        {
            _repo = repo;
            _repoO = repoO;
        }
        // GET: Kategoria
        public ActionResult Index()
        {
            var kategorie = _repo.PobierzKategorie().AsNoTracking();
            return View(kategorie);
        }


        public ActionResult Create()
        {
            KategoriaZRodzicem temp = new KategoriaZRodzicem();
            var list = _repo.PobierzKategorie().ToList();
            temp.ojciec = list.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nazwa });
            temp.głownyOjciec = list.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nazwa });
            return View(temp);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(KategoriaZRodzicem model)
        {
            if (ModelState.IsValid)
            {
                _repo.DodajKategorie(model);
                TempData["Message"] = "Dodano kategorię ! Gratulacje !";
                return RedirectToAction("Index");
            }
            else
                return View(model);
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

        [Authorize]
        [HttpGet]
        public ActionResult DefiniujAtrybuty(int? id)
        {
            if (id.HasValue)
            {


                KategoriaZAtrybutami temp = new KategoriaZAtrybutami();
                var list = _repo.PobierzAtrybuty();
                temp.kategoria = _repo.PobierzKategorie(id.Value);
                temp.atrybuty = _repo.PobierzAtrybutyzKategori(id.Value);
                temp.wszystkieAtrybuty = list.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nazwa });
                return View(temp);
            }
            else
                return null;

        }
        [Authorize]
        [HttpPost]
        public ActionResult DefiniujAtrybuty(KategoriaZAtrybutami model)
        {
            if (_repo.SprawdzCzyKategoriaPosiadaTakiAtrybut(model))
            {
                ModelState.AddModelError("error", "Kategoria posiada już taki atrybut !");

                var list = _repo.PobierzAtrybuty();
                model.atrybuty = _repo.PobierzAtrybutyzKategori(model.kategoria.Id);
                model.wszystkieAtrybuty = list.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nazwa });

                return View(model);
            }
            _repo.dodajAtrybutDoKategorii(model);
            return RedirectToAction("DefiniujAtrybuty", new { id = model.kategoria.Id });

        }

        [Authorize]
        public ActionResult Usun(int? id, int? idKat)
        {
            if (id.HasValue)
            {


                _repo.UsunAtrybutZKategorii(id.Value, idKat.Value);
                return RedirectToAction("DefiniujAtrybuty", new { id = idKat.Value });
            }
            else
                return null;

        }
        [Authorize]
        public ActionResult EdytujWartosciAtrybutow(int? id)
        {
            if (!id.HasValue)
            {
                var list = _repo.PobierzAtrybuty();
                AtrybutyZWartosciamiSLownik temp = new AtrybutyZWartosciamiSLownik();
                temp.wszystkieAtrybuty = list.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nazwa });
                temp.Atrybut = list.First();
                temp.wszystkieAtrybuty = list.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nazwa });
                temp.wartosciZaznaczonegoAtrybutu = _repoO.PobierzWartosciAtrybutowZAtrybutu(temp.Atrybut.Id);
                temp.zaznaczonyAtrybut = 0;
                //temp.nowaWartoscAtrybutu = "";
                return View(temp);

            }

            else
            {
                var list = _repo.PobierzAtrybuty();
                AtrybutyZWartosciamiSLownik temp = new AtrybutyZWartosciamiSLownik();
                temp.wszystkieAtrybuty = list.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nazwa });
                temp.Atrybut = list.Where(x => x.Id == id.Value).SingleOrDefault();
                temp.wszystkieAtrybuty = list.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nazwa });
                temp.wartosciZaznaczonegoAtrybutu = _repoO.PobierzWartosciAtrybutowZAtrybutu(temp.Atrybut.Id);
                temp.zaznaczonyAtrybut = id.Value;
                //temp.nowaWartoscAtrybutu = "";
                return View(temp);
            }

            return null;
        }
        [Authorize]
        [HttpPost]
        public ActionResult EdytujWartosciAtrybutow(AtrybutyZWartosciamiSLownik model)
        {

            if (model.nowaWartoscAtrybutu == null)
            {
                var list = _repo.PobierzAtrybuty();
                AtrybutyZWartosciamiSLownik temp = new AtrybutyZWartosciamiSLownik();
                temp.wszystkieAtrybuty = list.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nazwa });
                temp.Atrybut = list.Where(x => x.Id == model.zaznaczonyAtrybut).SingleOrDefault();
                temp.wszystkieAtrybuty = list.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nazwa });
                temp.wartosciZaznaczonegoAtrybutu = _repoO.PobierzWartosciAtrybutowZAtrybutu(temp.Atrybut.Id);
                temp.zaznaczonyAtrybut = model.zaznaczonyAtrybut;
                //temp.nowaWartoscAtrybutu = "";
                return View(temp);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (model.nowaWartoscAtrybutu.Id == null || model.nowaWartoscAtrybutu == null)
                    {
                        return RedirectToAction("EdytujWartosciAtrybutow");
                    }
                    model.nowaWartoscAtrybutu.IdAtrybut = model.Atrybut.Id;
                    _repoO.dodajAtrybutyZWartosciami(model.nowaWartoscAtrybutu);
                    return RedirectToAction("EdytujWartosciAtrybutow", new { id = model.Atrybut.Id });
                }
                else
                    return RedirectToAction("EdytujWartosciAtrybutow");

            }

        }

        [Authorize]
        public ActionResult UsunWartoscAtrybutu(int? id)
        {

            _repo.UsunWartoscAtrybutu(id.Value, 0);
            return RedirectToAction("EdytujWartosciAtrybutow");
        }

    }
}
