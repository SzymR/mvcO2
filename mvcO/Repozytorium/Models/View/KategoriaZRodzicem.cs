using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repozytorium.Models.View
{
    public class KategoriaZRodzicem
    {
        public Kategoria kategoria { set; get; }

        public IEnumerable<SelectListItem> ojciec { get; set; }


        public int selectedOjciec { get; set; }

        public IEnumerable<SelectListItem> głownyOjciec { get; set; }


        public int selectedGłownyOjciec { get; set; }


    }
}