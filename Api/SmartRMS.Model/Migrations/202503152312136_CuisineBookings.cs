namespace SmartRMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CuisineBookings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RestaurantId = c.Long(nullable: false),
                        Name = c.String(maxLength: 100),
                        MobileNo = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100),
                        Designation = c.String(maxLength: 100),
                        LocationId = c.Long(nullable: false),
                        Address = c.String(maxLength: 200),
                        Photo = c.String(maxLength: 200),
                        IdExpiry = c.DateTime(nullable: false),
                        IdPhoto = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: false)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: false)
                .Index(t => t.RestaurantId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.CuisineBookings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Time = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        RestaurantId = c.Long(nullable: false),
                        CuisineId = c.Long(nullable: false),
                        CustomerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cuisines", t => t.CuisineId, cascadeDelete: false)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: false)
                .Index(t => t.RestaurantId)
                .Index(t => t.CuisineId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Cuisines",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        NoOfPeople = c.Single(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RestaurantId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: false)
                .Index(t => t.RestaurantId);
            
            AddColumn("dbo.Bookings", "DriverId", c => c.Long());
            CreateIndex("dbo.Bookings", "DriverId");
            AddForeignKey("dbo.Bookings", "DriverId", "dbo.Drivers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CuisineBookings", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.CuisineBookings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CuisineBookings", "CuisineId", "dbo.Cuisines");
            DropForeignKey("dbo.Cuisines", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Bookings", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Drivers", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Drivers", "LocationId", "dbo.Locations");
            DropIndex("dbo.Cuisines", new[] { "RestaurantId" });
            DropIndex("dbo.CuisineBookings", new[] { "CustomerId" });
            DropIndex("dbo.CuisineBookings", new[] { "CuisineId" });
            DropIndex("dbo.CuisineBookings", new[] { "RestaurantId" });
            DropIndex("dbo.Drivers", new[] { "LocationId" });
            DropIndex("dbo.Drivers", new[] { "RestaurantId" });
            DropIndex("dbo.Bookings", new[] { "DriverId" });
            DropColumn("dbo.Bookings", "DriverId");
            DropTable("dbo.Cuisines");
            DropTable("dbo.CuisineBookings");
            DropTable("dbo.Drivers");
        }
    }
}
