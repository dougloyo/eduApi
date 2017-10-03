namespace EduApi.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonKey = c.String(maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        DateOfBirth = c.DateTime(nullable: false),
                        GenderAtBirth = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 75),
                        CreatedUtcDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedApplicationId = c.Int(nullable: false),
                        LastUpdatedUtcDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdatedApplicationId = c.Int(),
                        Deleted = c.Boolean(nullable: false),
                        DeletedApplicationId = c.Int(),
                        DeletedUtcDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Person");
        }
    }
}
