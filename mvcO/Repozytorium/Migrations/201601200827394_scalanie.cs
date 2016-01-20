namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scalanie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zdjecie", "Ogloszenie_Id", c => c.Int());
            CreateIndex("dbo.Zdjecie", "Ogloszenie_Id");
            AddForeignKey("dbo.Zdjecie", "Ogloszenie_Id", "dbo.Ogloszenie", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zdjecie", "Ogloszenie_Id", "dbo.Ogloszenie");
            DropIndex("dbo.Zdjecie", new[] { "Ogloszenie_Id" });
            DropColumn("dbo.Zdjecie", "Ogloszenie_Id");
        }
    }
}
