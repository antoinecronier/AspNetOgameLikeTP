namespace ASPNetOgameLikeTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Partie62 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buildings", "Print", c => c.String());
            AddColumn("dbo.Planets", "Print", c => c.String());
            AddColumn("dbo.Resources", "Print", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resources", "Print");
            DropColumn("dbo.Planets", "Print");
            DropColumn("dbo.Buildings", "Print");
        }
    }
}
