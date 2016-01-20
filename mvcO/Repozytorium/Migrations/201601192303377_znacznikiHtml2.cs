namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class znacznikiHtml2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DozwolonyZnacznikHtml",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        znacznik = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DozwolonyZnacznikHtml");
        }
    }
}
