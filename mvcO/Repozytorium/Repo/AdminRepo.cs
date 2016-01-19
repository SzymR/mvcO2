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
    }
}