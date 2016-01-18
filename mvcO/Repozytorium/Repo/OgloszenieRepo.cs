using Repozytorium.IRepo;
using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Repozytorium.Repo
{
    public class OgloszenieRepo : IOgloszenieRepo
    {
        private readonly IOglContext _db;

        /// <summary>
        /// Konstruktor w ktorym wstrzykiwana jest instancja repozytorium
        /// </summary>
        /// <param name="db">Baza</param>
        public OgloszenieRepo(IOglContext db)
        {
            _db = db;
        }

        public IQueryable<Ogloszenie> PobierzOgloszenia()
        {
            //_db.Database.Log = message => Trace.WriteLine(message); // sledzenie zapytan wysylanych do bazy
            //var ogloszenia = db.Ogloszenia.Include(o => o.Uzytkownik);
            var x = _db.AtrybutWartosc.AsNoTracking();
            var ogloszenia = _db.Ogloszenia.AsNoTracking();
            return ogloszenia;
        }
        public Ogloszenie GetOgloszenieById(int id)
        {
            Ogloszenie ogloszenie = _db.Ogloszenia.Find(id);
            return ogloszenie;
        }


        public void UsunOgloszenie(int id)
        {
            UsunPowiazanieOgloszenieKategoria(id);
            Ogloszenie ogloszenie = _db.Ogloszenia.Find(id);
            _db.Ogloszenia.Remove(ogloszenie);
        }


        private void UsunPowiazanieOgloszenieKategoria(int idOgloszenia)
        {
            var list = _db.Ogloszenie_Kategoria.Where(o => o.OgloszenieId == idOgloszenia);
            foreach (var el in list)
            {
                _db.Ogloszenie_Kategoria.Remove(el);
            }
        }


        public IQueryable<Ogloszenie> PobierzStrone(int? page = 1, int? pageSize = 10)
        {
            var ogloszenia = _db.Ogloszenia
                .OrderByDescending(o => o.DataDodania) // Sortowanie malejąco, od najnowszych ogłoszeń
                .Skip((page.Value - 1) * pageSize.Value) // Opuszcza x wybranych elementów
                .Take(pageSize.Value); // Pobranie tylko x najnowszych
            return ogloszenia;
        }


        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void Dodaj(Ogloszenie ogloszenie, string[] category)
        {
            _db.Ogloszenia.Add(ogloszenie);
            if (category != null)
            {
                DodajKategorieDoOgloszenia(ogloszenie, category);
            }

        }

        public void Aktualizuj(Ogloszenie ogloszenie)
        {
            _db.Entry(ogloszenie).State = EntityState.Modified;
        }


        public void DodajKategorieDoOgloszenia(Ogloszenie ogloszenie, string[] category)
        {
            foreach (var item in category)
            {
                Ogloszenie_Kategoria temp = new Ogloszenie_Kategoria();
                temp.KategoriaId = Convert.ToInt16(item);
                temp.OgloszenieId = ogloszenie.Id;
                _db.Ogloszenie_Kategoria.Add(temp);

            }
            SaveChanges();
        }

        public IQueryable<Atrybut> PobierzAtrybutyZKategorii(int id)
        {

            var test1 = (from o in _db.Kategoria_Atrybut
                         select o).ToList();

            var test2 = (from o in _db.Atrybut
                         select o).ToList();

            var test3 = (from o in _db.AtrybutWartosc
                         select o).ToList();


            var atrybut = from o in _db.Kategoria_Atrybut
                          join k in _db.Atrybut on o.IdAtrybut equals k.Id
                          where o.IdKategoria == id
                          select k;
            return atrybut;
        }

        public IQueryable<AtrybutWartosc> PobierzWartosciAtrybutowZAtrybutu(int id)
        {

            var atrW = from o in _db.Atrybut
                       join k in _db.AtrybutWartosc on o.Id equals k.IdAtrybut
                       where o.Id == id
                       select k;
            return atrW;
        }



    }
}