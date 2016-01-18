using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repozytorium.Models.View
{
    public class AtrybutZWartosciami
    {
        public Atrybut atrybut {set; get;}
        public IQueryable<AtrybutWartosc> atrybutWartosc { set; get; }
    }
}