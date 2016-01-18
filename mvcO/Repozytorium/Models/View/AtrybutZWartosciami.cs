using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repozytorium.Models.View
{
    public class AtrybutZWartosciami
    {
        public Atrybut atrybut { set; get; }
        public IQueryable<AtrybutWartosc> atrybutWartosc { set; get; }
        public IEnumerable<SelectListItem> list { get; set; }

        [Display(Name = "User Role")]
        public int Selected { get; set; }

    }
}