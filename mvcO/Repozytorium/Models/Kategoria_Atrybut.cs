using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Repozytorium.Models
{
    public class Kategoria_Atrybut
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Id atrybutu:")]
        public int IdAtrybut { get; set; }
        [Display(Name = "Id kategori:")]
        public int IdKategoria { get; set; }
        
        public virtual Atrybut Atrybut { get; set; }
        public virtual Kategoria Kategoria { get; set; }

    }
}