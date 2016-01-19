using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repozytorium.IRepo
{
    public interface IKategoriaRepo
    {
        IQueryable<Kategoria> PobierzKategorie();
        IQueryable<Ogloszenie> PobierzOgloszeniaZKategorii(int id);
        string NazwaDlaKategorii(int id);

        void DodajKategorie(Models.View.KategoriaZRodzicem model);

        List<Atrybut> PobierzAtrybutyzKategori(int p);

        Kategoria PobierzKategorie(int p);

        void UsunAtrybutZKategorii(int p1, int p2);

        void dodajAtrybutDoKategorii(Models.View.KategoriaZAtrybutami model);

        List<Atrybut> PobierzAtrybuty();

        bool SprawdzCzyKategoriaPosiadaTakiAtrybut(Models.View.KategoriaZAtrybutami model);
    }
}