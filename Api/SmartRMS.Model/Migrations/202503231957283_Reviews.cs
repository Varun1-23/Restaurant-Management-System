namespace SmartRMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reviews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Rating = c.Single(nullable: false),
                        Description = c.String(maxLength: 200),
                        RestaurantId = c.Long(nullable: false),
                        CustomerId = c.Long(nullable: false),
                        BookingId = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId)
                .Index(t => t.CustomerId)
                .Index(t => t.BookingId);
            
            AddColumn("dbo.Restaurants", "License", c => c.String(maxLength: 250));
            AddColumn("dbo.Restaurants", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Reviews", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Reviews", "BookingId", "dbo.Bookings");
            DropIndex("dbo.Reviews", new[] { "BookingId" });
            DropIndex("dbo.Reviews", new[] { "CustomerId" });
            DropIndex("dbo.Reviews", new[] { "RestaurantId" });
            DropColumn("dbo.Restaurants", "IsApproved");
            DropColumn("dbo.Restaurants", "License");
            DropTable("dbo.Reviews");
        }
    }
}
