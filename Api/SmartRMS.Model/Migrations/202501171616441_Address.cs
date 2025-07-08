namespace SmartRMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerAddresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Address1 = c.String(maxLength: 250),
                        Address2 = c.String(maxLength: 250),
                        Pincode = c.String(maxLength: 100),
                        StateId = c.Long(nullable: false),
                        DistrictId = c.Long(nullable: false),
                        LocationId = c.Long(nullable: false),
                        CustomerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId)
                .Index(t => t.DistrictId)
                .Index(t => t.LocationId)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Bookings", "CustomerAddressId", c => c.Long());
            CreateIndex("dbo.Bookings", "CustomerAddressId");
            AddForeignKey("dbo.Bookings", "CustomerAddressId", "dbo.CustomerAddresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "CustomerAddressId", "dbo.CustomerAddresses");
            DropForeignKey("dbo.CustomerAddresses", "StateId", "dbo.States");
            DropForeignKey("dbo.CustomerAddresses", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.CustomerAddresses", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.CustomerAddresses", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CustomerAddresses", new[] { "CustomerId" });
            DropIndex("dbo.CustomerAddresses", new[] { "LocationId" });
            DropIndex("dbo.CustomerAddresses", new[] { "DistrictId" });
            DropIndex("dbo.CustomerAddresses", new[] { "StateId" });
            DropIndex("dbo.Bookings", new[] { "CustomerAddressId" });
            DropColumn("dbo.Bookings", "CustomerAddressId");
            DropTable("dbo.CustomerAddresses");
        }
    }
}
