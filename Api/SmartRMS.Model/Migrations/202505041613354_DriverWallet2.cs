namespace SmartRMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverWallet2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DriverWallets", "DriverId", c => c.Long(nullable: false));
            CreateIndex("dbo.DriverWallets", "DriverId");
            AddForeignKey("dbo.DriverWallets", "DriverId", "dbo.Drivers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DriverWallets", "DriverId", "dbo.Drivers");
            DropIndex("dbo.DriverWallets", new[] { "DriverId" });
            DropColumn("dbo.DriverWallets", "DriverId");
        }
    }
}
