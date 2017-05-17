namespace EduApi.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBaseEntityModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Student", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Student", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Student", "CreatedDateTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Student", "CreatedApplicationId", c => c.Int(nullable: false));
            AddColumn("dbo.Student", "LastUpdatedDateTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Student", "LastUpdatedApplicationId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Student", "LastUpdatedApplicationId");
            DropColumn("dbo.Student", "LastUpdatedDateTime");
            DropColumn("dbo.Student", "CreatedApplicationId");
            DropColumn("dbo.Student", "CreatedDateTime");
            DropColumn("dbo.Student", "DeletedDate");
            DropColumn("dbo.Student", "Deleted");
        }
    }
}
