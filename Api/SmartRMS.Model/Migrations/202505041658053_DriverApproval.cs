namespace SmartRMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverApproval : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drivers", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drivers", "IsApproved");
        }
    }
}
