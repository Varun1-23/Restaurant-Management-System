namespace SmartRMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryRemoved : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Restaurants", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Restaurants", new[] { "CategoryId" });
            DropColumn("dbo.Restaurants", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Restaurants", "CategoryId", c => c.Long(nullable: false));
            CreateIndex("dbo.Restaurants", "CategoryId");
            AddForeignKey("dbo.Restaurants", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
