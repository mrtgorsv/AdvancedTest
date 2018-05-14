namespace AdvancedTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TheoryDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TheoryPartId = c.Int(nullable: false),
                        Seq = c.Int(nullable: false),
                        Name = c.String(),
                        Url = c.String(),
                        IsVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TheoryParts", t => t.TheoryPartId, cascadeDelete: false)
                .Index(t => t.TheoryPartId);
            
            CreateTable(
                "dbo.TheoryParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Seq = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TheoryTestParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TheoryId = c.Int(nullable: false),
                        Seq = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TheoryParts", t => t.TheoryId, cascadeDelete: false)
                .Index(t => t.TheoryId);
            
            CreateTable(
                "dbo.TheoryTestPartAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestPartId = c.Int(nullable: false),
                        IsCorrect = c.Boolean(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TheoryTestParts", t => t.TestPartId, cascadeDelete: false)
                .Index(t => t.TestPartId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Name = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserTheoryTests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TheoryPartId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Attempt = c.Int(nullable: false),
                        Result = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TheoryParts", t => t.TheoryPartId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.TheoryPartId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTheoryTests", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserTheoryTests", "TheoryPartId", "dbo.TheoryParts");
            DropForeignKey("dbo.TheoryDocuments", "TheoryPartId", "dbo.TheoryParts");
            DropForeignKey("dbo.TheoryTestParts", "TheoryId", "dbo.TheoryParts");
            DropForeignKey("dbo.TheoryTestPartAnswers", "TestPartId", "dbo.TheoryTestParts");
            DropIndex("dbo.UserTheoryTests", new[] { "UserId" });
            DropIndex("dbo.UserTheoryTests", new[] { "TheoryPartId" });
            DropIndex("dbo.TheoryTestPartAnswers", new[] { "TestPartId" });
            DropIndex("dbo.TheoryTestParts", new[] { "TheoryId" });
            DropIndex("dbo.TheoryDocuments", new[] { "TheoryPartId" });
            DropTable("dbo.UserTheoryTests");
            DropTable("dbo.Users");
            DropTable("dbo.TheoryTestPartAnswers");
            DropTable("dbo.TheoryTestParts");
            DropTable("dbo.TheoryParts");
            DropTable("dbo.TheoryDocuments");
        }
    }
}
