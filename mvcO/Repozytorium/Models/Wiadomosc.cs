using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repozytorium.Models
{
    public class Wiadomosc
    {
        [Key]
        public int id { set; get; }

        [Display(Name = "Tytuł : ")]
        public string tytul { set; get; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Treść wiadomości: ")]
        public string tresc { set; get; }
        [Display(Name = "Data Dodania: ")] 
        public DateTime dataDodania { set; get; }
    }
}