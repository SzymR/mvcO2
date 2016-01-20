using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repozytorium.Models
{
    public class OgloszenieAtrybutWartosc
    {
        [Key]
        public int id { set; get; }

        public int idOgloszenia { set; get; }
        public int IdAtrybut { set; get; }

        public int IdAtrybutWartosc { get; set; }
    }

    
}