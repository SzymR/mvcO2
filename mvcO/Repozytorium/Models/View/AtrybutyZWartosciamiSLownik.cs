using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repozytorium.Models.View
{
    public class AtrybutyZWartosciamiSLownik
    {

        public IEnumerable<SelectListItem> wszystkieAtrybuty { set; get; }


        public IEnumerable<AtrybutWartosc> wartosciZaznaczonegoAtrybutu { set; get; }

        public int  zaznaczonyAtrybut { set; get; }

        public AtrybutWartosc nowaWartoscAtrybutu { set; get; }

        public Atrybut Atrybut { set; get; }

    }
}