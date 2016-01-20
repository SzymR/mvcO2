namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class znacznikiHtml : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ZakazaneSlowo", "typ");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ZakazaneSlowo", "typ", c => c.String());
        }
    }
}
