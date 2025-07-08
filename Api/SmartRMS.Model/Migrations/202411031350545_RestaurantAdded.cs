namespace SmartRMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestaurantAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "RestaurantId", c => c.Long(nullable: false));
            CreateIndex("dbo.Categories", "RestaurantId");
            AddForeignKey("dbo.Categories", "RestaurantId", "dbo.Restaurants", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Categories", new[] { "RestaurantId" });
            DropColumn("dbo.Categories", "RestaurantId");
        }
    }
}
