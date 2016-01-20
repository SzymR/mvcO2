using Repozytorium.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repozytorium.Repo
{
    public class AdminRepo : IAdminRepo
    {

        private readonly IOglContext _db;

        public AdminRepo(IOglContext db)
        {
        _db = db;
        }

        public void DodajWiadomosc(Models.Wiadomosc wiadomosc)
        {
            _db.Wiadomosc.Add(wiadomosc);
            _db.SaveChanges();
        }


        public IEnumerable<Models.Wiadomosc> PobierzWiadomosc()
        {
            var x = _db.Wiadomosc.AsEnumerable() ;
            return x;
        }


        public object PobierzSlowaZakazane()
        {
            var x = _db.ZakazaneSlowo.AsEnumerable();
            return x;
        }


        public void dodajSlowoZakazane(Models.ZakazaneSlowo slowo)
        {
            _db.ZakazaneSlowo.Add(slowo);
            _db.SaveChanges();
        }


    


        public void UsunSlowo(int p)
        {
            var slowo = _db.ZakazaneSlowo.Where(x => x.id == p).SingleOrDefault();
            _db.ZakazaneSlowo.Remove(slowo);
            _db.SaveChanges();
        }


        public object PobierzDozwoloneZnacznikiHtml()
        {
            var x = _db.DozwolonyZnacznikHtml.AsEnumerable();
            return x;
        }

        public void dodajZnacznikHtml(Models.DozwolonyZnacznikHtml znacznikk)
        {
            _db.DozwolonyZnacznikHtml.Add(znacznikk);
            _db.SaveChanges();
        }

        public void UsunZnacznikHtml(int p)
        {
            var znacznik = _db.DozwolonyZnacznikHtml.Where(x => x.id == p).SingleOrDefault();
            _db.DozwolonyZnacznikHtml.Remove(znacznik);
            _db.SaveChanges();
        }
    }
}