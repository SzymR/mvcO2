using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repozytorium.Models
{
    public class ZakazaneSlowo
    {
        [Key]
        public int id { set; get; }
        public string słowo { set; get; }
    }
}