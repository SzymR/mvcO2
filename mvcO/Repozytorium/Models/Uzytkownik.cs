using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Repozytorium.Models
{
    
        public class Uzytkownik : IdentityUser
        {
            

            public Uzytkownik()
            {
                this.Ogloszenia = new HashSet<Ogloszenie>();
                this.Zdjecia = new HashSet<Zdjecie>();
            }
            // Klucz podstawowy odziedziczony po klasie IdentityUser
            // Pola Imie i Nazwisko

            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            // Znak zapytania oznacza typ Nullable, dzieki temu wiek nie musi byc ustawiony
            public int? Wiek { get; set; }   

            #region dodatkowe pole notmapped
            [NotMapped]
            [Display(Name = "Pan/Pani:")]
            public string PelneNazwisko
            {
                get { return Imie + " " + Nazwisko; }
            }
            #endregion

            public virtual ICollection<Ogloszenie>
                Ogloszenia { get; private set; }
            public virtual ICollection<Zdjecie> Zdjecia { get; set; }

            public async Task<ClaimsIdentity>
                GenerateUserIdentityAsync(UserManager<Uzytkownik> manager)
            {
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);                
                return userIdentity;
            }


        }
    
}