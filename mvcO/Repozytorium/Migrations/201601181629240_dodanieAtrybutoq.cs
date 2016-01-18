namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanieAtrybutoq : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kategoria", "MainParent", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kategoria", "MainParent");
        }
    }
}
