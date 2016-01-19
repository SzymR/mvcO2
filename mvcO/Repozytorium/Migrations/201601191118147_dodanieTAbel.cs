namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanieTAbel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OgloszenieAtrybutWartosc",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        IdAtrybut = c.Int(nullable: false),
                        IdAtrybutWartosc = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Wiadomosc",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        tresc = c.String(),
                        dataDodania = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ZakazaneSlowo",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        słowo = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ZakazaneSlowo");
            DropTable("dbo.Wiadomosc");
            DropTable("dbo.OgloszenieAtrybutWartosc");
        }
    }
}
