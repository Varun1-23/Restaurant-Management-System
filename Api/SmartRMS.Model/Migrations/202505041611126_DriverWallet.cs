namespace SmartRMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverWallet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DriverWallets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Amount = c.Single(nullable: false),
                        BookingId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .Index(t => t.BookingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DriverWallets", "BookingId", "dbo.Bookings");
            DropIndex("dbo.DriverWallets", new[] { "BookingId" });
            DropTable("dbo.DriverWallets");
        }
    }
}
