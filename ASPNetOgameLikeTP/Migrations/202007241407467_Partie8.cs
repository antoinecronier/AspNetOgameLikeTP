namespace ASPNetOgameLikeTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Partie8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Resources", "LastQuantity", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Resources", "LastQuantity", c => c.Int());
        }
    }
}
