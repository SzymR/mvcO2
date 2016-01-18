namespace Repozytorium.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Repozytorium.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repozytorium.Models.OglContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            
            
        }

        protected override void Seed(Repozytorium.Models.OglContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // Do debugowania metody seed
            // Zastosowanie tego kodu podczas startu metody Seed() uruchomi druga instancje VisualStudio,
            // w ktorej bedzie mozliwosc debugowania i sprawdzenia, jakie bledy zawiera metoda.

            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();
            //SeeedRoles(context);
            // SeedUsers(context);
            // SeedOgloszenia(context);
           //SeedKategorie(context);
           ///// SeedOgloszenie_Kategoria(context);
           //SeedAtrybut(context);
           // SeedAtrybutWartosc(context);
           // SeedKategoriaAtrybut(context);
        }

        private void SeedUsers(Models.OglContext context)
        {
            var store = new UserStore<Uzytkownik>(context);
            var manager = new UserManager<Uzytkownik>(store);
            if (!context.Users.Any(u => u.UserName == "Administrator"))
            {
                var user = new Uzytkownik { UserName = "Admin@asp.pl", Wiek=21 };
                var adminresult = manager.Create(user, "12345678");
                if (adminresult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }
            if(!context.Users.Any(u=>u.UserName=="Marek")){
                var user = new Uzytkownik {UserName="marek@asp.pl"};
                var adminresult = manager.Create(user,"1234Abc");
                if(adminresult.Succeeded){
                    manager.AddToRole(user.Id, "Pracownik");
                }

            }
            if(!context.Users.Any(u=>u.UserName=="Prezes")){
                var user = new Uzytkownik {UserName="prezes@asp.pl"};
                var adminresult = manager.Create(user,"1234Abc");
                if(adminresult.Succeeded){
                    manager.AddToRole(user.Id, "Admin");
                }
            }


        }
         //AddOrUpdate nie bedzie duplikowac danych przy kazdym wywolaniu metody Seed()
        //private void SeedOgloszenia(Models.OglContext context)
        //{
        //    var idUzytkownika = context.Set<Uzytkownik>().Where(u => u.UserName == "Admin").FirstOrDefault().Id;
        //    for (int i = 1; i < 10; i++)
        //    {
        //        var ogl = new Ogloszenie(){
        //            Id = i,
        //            UzytkownikId = idUzytkownika,
        //            Tresc = "Treść ogłoszenia" + i.ToString(),
        //            Tytul = "Tytuł ogłoszenia" + i.ToString(),
        //            DataDodania = DateTime.Now.AddDays(-i)
        //        };
        //        context.Set<Ogloszenie>().AddOrUpdate(ogl);
        //    }
        //    context.SaveChanges();            
        //}

        //private void SeedKategorie(Models.OglContext context)
        //{
        //    //for (int i = 1; i < 10; i++)
        //    //{
        //    //    var kat = new Kategoria()
        //    //    {
        //    //        Id = i,
        //    //        Nazwa = "Nazwa kategorii" + i.ToString(),
        //    //        Tresc = "Treœæ og³oszenia" + i.ToString(),
        //    //        MetaTytul = "Tytu³ kategorii" + i.ToString(),
        //    //        MetaOpis = "Opis kategorii" + i.ToString(),
        //    //        MetaSlowa = "S³owa kluczowe do kategorii" + i.ToString(),
        //    //        ParentId = i,
        //    //        MainParent = 1
        //    //    };
        //    //    context.Set<Kategoria>().AddOrUpdate(kat);
        //    //}


        //    var kat = new Kategoria()
        //    {
        //        Id = 1,
        //        Nazwa = "Odziez",
        //        Tresc = "Odziez",
        //        MetaTytul = "Odziez",
        //        MetaOpis = "Odziez",
        //        MetaSlowa = "Odziez",
        //        ParentId = 0,
        //        MainParent = 1
        //    };

        //    context.Set<Kategoria>().AddOrUpdate(kat);

        //    var kat2 = new Kategoria()
        //    {
        //        Id = 2,
        //        Nazwa = "Odziez Damska",
        //        Tresc = "Odziez Damska",
        //        MetaTytul = "Odziez Damska",
        //        MetaOpis = "Odziez Damska",
        //        MetaSlowa = "Odziez Damska",
        //        ParentId = 1,
        //        MainParent = 1
        //    };

        //    context.Set<Kategoria>().AddOrUpdate(kat2);

        //    var kat3 = new Kategoria()
        //    {
        //        Id = 3,
        //        Nazwa = "Skarpety",
        //        Tresc = "Skarpety",
        //        MetaTytul = "Skarpety",
        //        MetaOpis = "Skarpety",
        //        MetaSlowa = "Skarpety",
        //        ParentId = 2,
        //        MainParent = 1
        //    };

        //    context.Set<Kategoria>().AddOrUpdate(kat3);


        //    var kat4 = new Kategoria()
        //    {
        //        Id = 4,
        //        Nazwa = "Odziez Meska",
        //        Tresc = "Odziez Meska",
        //        MetaTytul = "Odziez Meska",
        //        MetaOpis = "Odziez Meska",
        //        MetaSlowa = "Odziez Meska",
        //        ParentId = 1,
        //        MainParent = 1
        //    };

        //    context.Set<Kategoria>().AddOrUpdate(kat4);


        //    var kat5 = new Kategoria()
        //    {
        //        Id = 5,
        //        Nazwa = "RTV",
        //        Tresc = "RTV",
        //        MetaTytul = "RTV",
        //        MetaOpis = "RTV",
        //        MetaSlowa = "RTV",
        //        ParentId = 0,
        //        MainParent = 1
        //    };

        //    context.Set<Kategoria>().AddOrUpdate(kat5);

        //    var kat6 = new Kategoria()
        //    {
        //        Id = 6,
        //        Nazwa = "Telewizory",
        //        Tresc = "Telewizory",
        //        MetaTytul = "Telewizory",
        //        MetaOpis = "Telewizory",
        //        MetaSlowa = "Telewizory",
        //        ParentId = 5,
        //        MainParent = 5
        //    };

        //    context.Set<Kategoria>().AddOrUpdate(kat6);




        //    context.SaveChanges();
        //}

        //private void SeedOgloszenie_Kategoria(Models.OglContext context)
        //{
        //    for (int i = 1; i < 10; i++)
        //    {
        //        var okat = new Ogloszenie_Kategoria()
        //        {
        //            Id = i,
        //            OgloszenieId = i,
        //            KategoriaId = i
        //        };
        //        context.Set<Ogloszenie_Kategoria>().AddOrUpdate(okat);
        //    }
        //    context.SaveChanges();
        //}

        //private void SeeedRoles(Models.OglContext context)
        //{
        //    var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>
        //    (new RoleStore<IdentityRole>());

        //    if (!roleManager.RoleExists("Admin"))
        //    {
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Admin";
        //        roleManager.Create(role);
        //    }
        //    if (!roleManager.RoleExists("Pracownik"))
        //    {
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Pracownik";
        //        roleManager.Create(role);
        //    }

        //}

        //private void SeedAtrybut(Models.OglContext context)
        //{
        //    var atr = new Atrybut()
        //    {
        //        Id = 1,
        //        Nazwa = "Rozmiar"
        //    };
        //    context.Set<Atrybut>().AddOrUpdate(atr);

        //    var atr2 = new Atrybut()
        //    {
        //        Id = 2,
        //        Nazwa = "Wielkosc"
        //    };
        //    context.Set<Atrybut>().AddOrUpdate(atr2);

        //    var atr3 = new Atrybut()
        //    {
        //        Id = 3,
        //        Nazwa = "Marka"
        //    };
        //    context.Set<Atrybut>().AddOrUpdate(atr3);

        //    context.SaveChanges();
        //}

        //private void SeedKategoriaAtrybut(Models.OglContext context)
        //{
        //    var kat = new Kategoria_Atrybut()
        //    {
        //        IdAtrybut = 1,
        //        IdKategoria = 2

        //    };
        //    context.Set<Kategoria_Atrybut>().AddOrUpdate(kat);

        //    var kat2 = new Kategoria_Atrybut()
        //    {
        //        IdAtrybut = 2,
        //        IdKategoria = 6

        //    };
        //    context.Set<Kategoria_Atrybut>().AddOrUpdate(kat2);

        //    var kat3 = new Kategoria_Atrybut()
        //    {
        //        IdAtrybut = 2,
        //        IdKategoria = 6

        //    };
        //    context.Set<Kategoria_Atrybut>().AddOrUpdate(kat3);

        //    context.SaveChanges();
        //}

        //private void SeedAtrybutWartosc(Models.OglContext context)
        //{
        //    var kat = new AtrybutWartosc()
        //    {
        //        IdAtrybut = 3,
        //        Wartosc = "Samsung"
        //    };
        //    context.Set<AtrybutWartosc>().AddOrUpdate(kat);
        //    var kat2 = new AtrybutWartosc()
        //    {
        //        IdAtrybut = 3,
        //        Wartosc = "LG"
        //    };
        //    context.Set<AtrybutWartosc>().AddOrUpdate(kat2);
        //    var kat3 = new AtrybutWartosc()
        //    {
        //        IdAtrybut = 3,
        //        Wartosc = "Lenovo"
        //    };
        //    context.Set<AtrybutWartosc>().AddOrUpdate(kat3);
        //    context.SaveChanges();
        //}



    }
}

