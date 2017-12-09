namespace FoxRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gymsessions1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GymSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GymSessionName = c.String(),
                        RemainingPlaces = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GymSessions");
        }
    }
}
