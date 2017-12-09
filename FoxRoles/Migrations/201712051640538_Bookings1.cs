namespace FoxRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bookings1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GymSessionId = c.Int(nullable: false),
                        GymSessionName = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Bookings", new[] { "User_Id" });
            DropTable("dbo.Bookings");
        }
    }
}
