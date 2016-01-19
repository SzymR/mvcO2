using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repozytorium.IRepo
{
    public interface IAdminRepo
    {

        void DodajWiadomosc(Wiadomosc wiadomosc);
        IEnumerable<Wiadomosc> PobierzWiadomosc();

        object PobierzSlowaZakazane();

   

        void dodajSlowoZakazane(ZakazaneSlowo slowo);



         void UsunSlowo(int p);
    }
}
