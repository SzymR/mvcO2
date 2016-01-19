namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanieSlow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wiadomosc", "tytul", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Wiadomosc", "tytul");
        }
    }
}
