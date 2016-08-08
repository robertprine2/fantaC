namespace FantaC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class textarea : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ApplicationUserId", c => c.String());
        }
    }
}
