namespace RitterToDo.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Starter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    IdentityRoleId = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.IdentityRoleId);

            CreateTable(
                "dbo.ToDoCategory",
                c => new
                {
                    ToDoCategoryId = c.Guid(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    OwnerId = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.ToDoCategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    IdentityUserId = c.String(nullable: false, maxLength: 128),
                    UserName = c.String(),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.IdentityUserId);

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    IdentityUserClaimId = c.Int(nullable: false, identity: true),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                    User_Id = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.IdentityUserClaimId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.ToDo",
                c => new
                {
                    ToDoId = c.Guid(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Description = c.String(maxLength: 400),
                    DueDate = c.DateTime(),
                    Starred = c.Boolean(nullable: false),
                    Done = c.Boolean(nullable: false),
                    ToDoCategoryId = c.Guid(nullable: false),
                    OwnerId = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.ToDoId)
                .ForeignKey("dbo.ToDoCategory", t => t.ToDoCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .Index(t => t.ToDoCategoryId)
                .Index(t => t.OwnerId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.ToDo", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ToDo", "ToDoCategoryId", "dbo.ToDoCategory");
            DropForeignKey("dbo.ToDoCategory", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ToDo", new[] { "OwnerId" });
            DropIndex("dbo.ToDo", new[] { "ToDoCategoryId" });
            DropIndex("dbo.ToDoCategory", new[] { "OwnerId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropTable("dbo.ToDo");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ToDoCategory");
            DropTable("dbo.AspNetRoles");
        }
    }
}