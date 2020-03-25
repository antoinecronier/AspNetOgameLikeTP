namespace ASPNetOgameLikeTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Partie3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Level = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Planet_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Planets", t => t.Planet_Id)
                .Index(t => t.Planet_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Buildings", "Planet_Id", "dbo.Planets");
            DropIndex("dbo.Buildings", new[] { "Planet_Id" });
            DropTable("dbo.Buildings");
        }
    }
}
