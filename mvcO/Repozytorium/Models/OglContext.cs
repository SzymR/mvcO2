using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using Repozytorium.IRepo;

namespace Repozytorium.Models
{
    // You can add profile data for the user by adding more properties to your Uzytkownik class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    

    public class OglContext : IdentityDbContext, IOglContext
    {
    
        public OglContext()
            : base("DefaultConnection")
        {
        }

        public static OglContext Create()
        {
            return new OglContext();
        }

        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Ogloszenie> Ogloszenia { get; set; }
        public DbSet<Uzytkownik> Uzytkownik { get; set; }
        public DbSet<Ogloszenie_Kategoria> Ogloszenie_Kategoria { get; set; }
        public DbSet<Atrybut> Atrybut { get; set; }
        public DbSet<AtrybutWartosc> AtrybutWartosc { get; set; }
        public DbSet<Kategoria_Atrybut> Kategoria_Atrybut { get; set; }
        public DbSet<Zdjecie> Zdjecia { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Potrzebne dla klas Identity
            base.OnModelCreating(modelBuilder);

            //using System.Data.Entity.ModelConfiguration.Conventions;
            //Wylacz konwencje, ktora automatycznie tworzy liczbe mnoga dla nazw tabel w bazie danych
            //Zamiast Kategorie zostalaby utworzona tabela Kategories
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Wylacza konwencje CascadeDelete
            //CascadeDelete zostanie wlaczone za pomoca Fluent API
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Uzywa sie Fluent API, aby ustalic powiazanie pomiedzy tabelami
            // i wylaczyc CascadeDelete dla tego powiazania
            modelBuilder.Entity<Ogloszenie>().HasRequired(x => x.Uzytkownik)
                .WithMany(x => x.Ogloszenia)
                .HasForeignKey(x => x.UzytkownikId)
                .WillCascadeOnDelete(true);
        }


    }

    
}