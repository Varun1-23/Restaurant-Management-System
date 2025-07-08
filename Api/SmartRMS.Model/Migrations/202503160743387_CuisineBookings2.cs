namespace SmartRMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CuisineBookings2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DriverId", c => c.Long());
            CreateIndex("dbo.Users", "DriverId");
            AddForeignKey("dbo.Users", "DriverId", "dbo.Drivers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "DriverId", "dbo.Drivers");
            DropIndex("dbo.Users", new[] { "DriverId" });
            DropColumn("dbo.Users", "DriverId");
        }
    }
}
