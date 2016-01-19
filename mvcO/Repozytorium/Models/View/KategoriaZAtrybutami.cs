using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repozytorium.Models.View
{
    public class KategoriaZAtrybutami
    {

        public Kategoria kategoria { set; get; }

        public List<Atrybut> atrybuty { set; get; }

        public IEnumerable<SelectListItem>  wszystkieAtrybuty { set; get; }

        public int Selected { set;  get; }
    }
}