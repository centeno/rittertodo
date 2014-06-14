namespace RitterToDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserApiKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToDo", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.ToDo", new[] { "OwnerId" });
            CreateTable(
                "dbo.UserApiKey",
                c => new
                    {
                        ApiKey = c.String(nullable: false, maxLength: 100),
                        UserId = c.String(nullable: false, maxLength: 128),
                        AppName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ApiKey)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.ToDo", "OwnerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ToDo", "OwnerId");
            AddForeignKey("dbo.ToDo", "OwnerId", "dbo.AspNetUsers", "IdentityUserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDo", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserApiKey", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserApiKey", new[] { "UserId" });
            DropIndex("dbo.ToDo", new[] { "OwnerId" });
            AlterColumn("dbo.ToDo", "OwnerId", c => c.String(maxLength: 128));
            DropTable("dbo.UserApiKey");
            CreateIndex("dbo.ToDo", "OwnerId");
            AddForeignKey("dbo.ToDo", "OwnerId", "dbo.AspNetUsers", "IdentityUserId");
        }
    }
}
