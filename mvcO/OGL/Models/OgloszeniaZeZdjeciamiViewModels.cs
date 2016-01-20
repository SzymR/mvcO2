using Repozytorium.Models;
using Repozytorium.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OGL.Models
{
    public class OgloszeniaZeZdjeciamiViewModels
    {
        public Ogloszenie Ogloszenie { get; set; }
       
        public IList<Zdjecie> Zdjecia { get; set; }

        public List<JedenAtrybutZWartoscią> atrybuty { get; set; }
    }
}