namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanieCzyreport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ogloszenie", "czyZreportowane", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ogloszenie", "czyZreportowane");
        }
    }
}
