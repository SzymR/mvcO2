using Repozytorium.IRepo;
using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Repozytorium.Repo
{
    public class KategoriaRepo : IKategoriaRepo
    {
        private readonly IOglContext _db;

        public KategoriaRepo(IOglContext db)
        {
        _db = db;
        }

        public IQueryable<Kategoria> PobierzKategorie()
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var kategorie = _db.Kategorie.AsNoTracking();
            return kategorie;
        }
        public IQueryable<Ogloszenie> PobierzOgloszeniaZKategorii(int id)
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var ogloszenia = from o in _db.Ogloszenia
                             join k in _db.Ogloszenie_Kategoria on o.Id equals k.Id
                             where k.KategoriaId == id
                             select o;
            return ogloszenia;
        }
        public string NazwaDlaKategorii(int id)
        {
            var nazwa = _db.Kategorie.Find(id).Nazwa;
            return nazwa;
        }


        public void DodajKategorie(Models.View.KategoriaZRodzicem model)
        {
            Kategoria temp = model.kategoria;
            temp.ParentId = model.selectedOjciec;
            temp.MainParent = model.selectedGłownyOjciec;
            _db.Kategorie.Add(temp);
            _db.SaveChanges();
        }


        public List<Atrybut> PobierzAtrybutyzKategori(int p)
        {
            var list = (from o in _db.Kategoria_Atrybut
                        join k in _db.Atrybut on o.IdAtrybut equals k.Id
                        where o.IdKategoria==p
                        select k).ToList();
            return list;
        }

        public Kategoria PobierzKategorie(int p)
        {

            return _db.Kategorie.Where(x => x.Id == p).SingleOrDefault();
        }


        public void UsunAtrybutZKategorii(int p1, int p2)
        {
            var col = _db.Kategoria_Atrybut.Where(x => x.IdAtrybut == p1 && x.IdKategoria == p2);

            foreach (var item in col)
            {
                _db.Kategoria_Atrybut.Remove(item);
            }

            _db.SaveChanges();
        }


        public void dodajAtrybutDoKategorii(Models.View.KategoriaZAtrybutami model)
        {
            _db.Kategoria_Atrybut.Add(new Kategoria_Atrybut()
            {

                 IdAtrybut=model.Selected,
                 IdKategoria=model.kategoria.Id
            });

            _db.SaveChanges();
       
        }


        public List<Atrybut> PobierzAtrybuty()
        {
            return _db.Atrybut.ToList();
        }


        public bool SprawdzCzyKategoriaPosiadaTakiAtrybut(Models.View.KategoriaZAtrybutami model)
        {
            return _db.Kategoria_Atrybut.Any(x=>x.IdAtrybut==model.Selected && x.IdKategoria==model.kategoria.Id);
        }

        public void UsunWartoscAtrybutu(int p1, int p2)
        {
            var item = _db.AtrybutWartosc.Where(x => x.Id == p1).SingleOrDefault();
            _db.AtrybutWartosc.Remove(item);
            _db.SaveChanges();
        }
    }
}