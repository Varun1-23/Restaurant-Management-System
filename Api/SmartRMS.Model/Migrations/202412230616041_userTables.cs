namespace SmartRMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(maxLength: 100),
                        PasswordSalt = c.String(maxLength: 256),
                        Password = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                        Role = c.String(maxLength: 50),
                        IsBlocked = c.Boolean(nullable: false),
                        CustomerId = c.Long(),
                        RestaurantId = c.Long(),
                        EmployeeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId)
                .Index(t => t.CustomerId)
                .Index(t => t.RestaurantId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.UserSessions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Token = c.String(maxLength: 256),
                        SessionTimeStamp = c.DateTime(nullable: false),
                        ExpiresInMinutes = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        UserSessionStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSessions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Users", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Users", "CustomerId", "dbo.Customers");
            DropIndex("dbo.UserSessions", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "EmployeeId" });
            DropIndex("dbo.Users", new[] { "RestaurantId" });
            DropIndex("dbo.Users", new[] { "CustomerId" });
            DropTable("dbo.UserSessions");
            DropTable("dbo.Users");
        }
    }
}
