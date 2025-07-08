namespace SmartRMS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRejectionReasonToRestaurant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "RejectionReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "RejectionReason");
        }
    }
}
