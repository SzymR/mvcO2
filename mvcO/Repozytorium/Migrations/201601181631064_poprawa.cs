namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poprawa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Atrybut",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AtrybutWartosc",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdAtrybut = c.Int(nullable: false),
                        Wartosc = c.String(),
                        Atrybut_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Atrybut", t => t.Atrybut_Id)
                .Index(t => t.Atrybut_Id);
            
            CreateTable(
                "dbo.Kategoria_Atrybut",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdAtrybut = c.Int(nullable: false),
                        IdKategoria = c.Int(nullable: false),
                        Atrybut_Id = c.Int(),
                        Kategoria_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Atrybut", t => t.Atrybut_Id)
                .ForeignKey("dbo.Kategoria", t => t.Kategoria_Id)
                .Index(t => t.Atrybut_Id)
                .Index(t => t.Kategoria_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kategoria_Atrybut", "Kategoria_Id", "dbo.Kategoria");
            DropForeignKey("dbo.Kategoria_Atrybut", "Atrybut_Id", "dbo.Atrybut");
            DropForeignKey("dbo.AtrybutWartosc", "Atrybut_Id", "dbo.Atrybut");
            DropIndex("dbo.Kategoria_Atrybut", new[] { "Kategoria_Id" });
            DropIndex("dbo.Kategoria_Atrybut", new[] { "Atrybut_Id" });
            DropIndex("dbo.AtrybutWartosc", new[] { "Atrybut_Id" });
            DropTable("dbo.Kategoria_Atrybut");
            DropTable("dbo.AtrybutWartosc");
            DropTable("dbo.Atrybut");
        }
    }
}
