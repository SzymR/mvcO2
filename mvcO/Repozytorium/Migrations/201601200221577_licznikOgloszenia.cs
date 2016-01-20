namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class licznikOgloszenia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ogloszenie", "Licznik", c => c.Int(nullable: false));
            AlterColumn("dbo.AtrybutWartosc", "Wartosc", c => c.String(nullable: false));
            AlterColumn("dbo.Wiadomosc", "tytul", c => c.String(nullable: false));
            AlterColumn("dbo.Wiadomosc", "tresc", c => c.String(nullable: false));
            AlterColumn("dbo.ZakazaneSlowo", "słowo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ZakazaneSlowo", "słowo", c => c.String());
            AlterColumn("dbo.Wiadomosc", "tresc", c => c.String());
            AlterColumn("dbo.Wiadomosc", "tytul", c => c.String());
            AlterColumn("dbo.AtrybutWartosc", "Wartosc", c => c.String());
            DropColumn("dbo.Ogloszenie", "Licznik");
        }
    }
}
