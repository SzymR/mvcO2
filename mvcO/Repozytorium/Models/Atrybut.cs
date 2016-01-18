using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Repozytorium.Models
{
    public class Atrybut
    {
        public Atrybut()
        {
            this.Atrybut_Wartosc = new HashSet<AtrybutWartosc>();
            this.Kategoria_Atrybut = new HashSet<Kategoria_Atrybut>();
        }

        [Key]
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<AtrybutWartosc> Atrybut_Wartosc { get; set; }
        public virtual ICollection<Kategoria_Atrybut> Kategoria_Atrybut { get; set; }
    }
}