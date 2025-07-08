namespace SmartRMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BookingId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        Quantity = c.Single(nullable: false),
                        Price = c.Single(nullable: false),
                        OfferPrice = c.Single(nullable: false),
                        Total = c.Single(nullable: false),
                        Remarks = c.String(maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: false)
                .Index(t => t.BookingId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CustomerId = c.Long(nullable: false),
                        RestaurantId = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Subtotal = c.Single(nullable: false),
                        TotalDiscount = c.Single(nullable: false),
                        GrandTotal = c.Single(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        DeliveryTime = c.String(maxLength: 100),
                        StatusId = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: false)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: false)
                .Index(t => t.CustomerId)
                .Index(t => t.RestaurantId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        MobileNo = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Photo = c.String(maxLength: 200),
                        StateId = c.Long(nullable: false),
                        DistrictId = c.Long(nullable: false),
                        LocationId = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: false)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: false)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: false)
                .Index(t => t.StateId)
                .Index(t => t.DistrictId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        StateId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: false)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        StateId = c.Long(nullable: false),
                        DistrictId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: false)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: false)
                .Index(t => t.StateId)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Photo = c.String(maxLength: 200),
                        MobileNo = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100),
                        Description = c.String(maxLength: 200),
                        StateId = c.Long(nullable: false),
                        DistrictId = c.Long(nullable: false),
                        LocationId = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Cost = c.String(maxLength: 50),
                        Type = c.String(maxLength: 50),
                        CategoryId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: false)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: false)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: false)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: false)
                .Index(t => t.StateId)
                .Index(t => t.DistrictId)
                .Index(t => t.LocationId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Photo = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Photo = c.String(maxLength: 200),
                        RestaurantId = c.Long(nullable: false),
                        CategoryId = c.Long(nullable: false),
                        Description = c.String(maxLength: 200),
                        Price = c.Single(nullable: false),
                        OfferPrice = c.Single(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: false)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: false)
                .Index(t => t.RestaurantId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Employees",
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
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: false)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: false)
                .Index(t => t.RestaurantId)
                .Index(t => t.LocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Employees", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.BookingDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.BookingDetails", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Bookings", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Restaurants", "StateId", "dbo.States");
            DropForeignKey("dbo.Restaurants", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Restaurants", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Restaurants", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "StateId", "dbo.States");
            DropForeignKey("dbo.Customers", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Locations", "StateId", "dbo.States");
            DropForeignKey("dbo.Locations", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Customers", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Districts", "StateId", "dbo.States");
            DropIndex("dbo.Employees", new[] { "LocationId" });
            DropIndex("dbo.Employees", new[] { "RestaurantId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Products", new[] { "RestaurantId" });
            DropIndex("dbo.Restaurants", new[] { "CategoryId" });
            DropIndex("dbo.Restaurants", new[] { "LocationId" });
            DropIndex("dbo.Restaurants", new[] { "DistrictId" });
            DropIndex("dbo.Restaurants", new[] { "StateId" });
            DropIndex("dbo.Locations", new[] { "DistrictId" });
            DropIndex("dbo.Locations", new[] { "StateId" });
            DropIndex("dbo.Districts", new[] { "StateId" });
            DropIndex("dbo.Customers", new[] { "LocationId" });
            DropIndex("dbo.Customers", new[] { "DistrictId" });
            DropIndex("dbo.Customers", new[] { "StateId" });
            DropIndex("dbo.Bookings", new[] { "StatusId" });
            DropIndex("dbo.Bookings", new[] { "RestaurantId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropIndex("dbo.BookingDetails", new[] { "ProductId" });
            DropIndex("dbo.BookingDetails", new[] { "BookingId" });
            DropTable("dbo.Employees");
            DropTable("dbo.Products");
            DropTable("dbo.Status");
            DropTable("dbo.Categories");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Locations");
            DropTable("dbo.States");
            DropTable("dbo.Districts");
            DropTable("dbo.Customers");
            DropTable("dbo.Bookings");
            DropTable("dbo.BookingDetails");
        }
    }
}
