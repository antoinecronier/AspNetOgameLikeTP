namespace ASPNetOgameLikeTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Partie6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Resources", "LastUpdate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Resources", "LastUpdate", c => c.DateTime(nullable: false));
        }
    }
}
