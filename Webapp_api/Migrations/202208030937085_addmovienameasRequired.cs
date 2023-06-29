namespace Webapp_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmovienameasRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Name", c => c.String());
        }
    }
}
