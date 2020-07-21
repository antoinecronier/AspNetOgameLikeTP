namespace ASPNetOgameLikeTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Partie71 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Universes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SolarSystems", "Universe_Id", c => c.Long());
            CreateIndex("dbo.SolarSystems", "Universe_Id");
            AddForeignKey("dbo.SolarSystems", "Universe_Id", "dbo.Universes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SolarSystems", "Universe_Id", "dbo.Universes");
            DropIndex("dbo.SolarSystems", new[] { "Universe_Id" });
            DropColumn("dbo.SolarSystems", "Universe_Id");
            DropTable("dbo.Universes");
        }
    }
}
