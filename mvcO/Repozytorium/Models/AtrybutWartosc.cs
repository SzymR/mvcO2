using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Repozytorium.Models
{
    public class AtrybutWartosc
    {
        [Key]
        public int Id { get; set; }

        public int IdAtrybut { get; set; }
        public string Wartosc { get; set; }

        public virtual Atrybut Atrybut { get; set; }

    }
}