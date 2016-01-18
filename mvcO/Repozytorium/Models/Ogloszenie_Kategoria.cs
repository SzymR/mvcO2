using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repozytorium.Models
{
    public class Ogloszenie_Kategoria
    {
        public Ogloszenie_Kategoria()
        {

        }
        public int Id { get; set; }
        public int KategoriaId { get; set; }
        public int OgloszenieId { get; set; }

        public virtual Kategoria Karegoria { get; set; }
        public virtual Ogloszenie Oglosznie { get; set; }
    }
}
