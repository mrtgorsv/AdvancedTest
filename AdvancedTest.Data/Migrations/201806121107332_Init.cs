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
                        IsVisible = c.Boolean(nullable: false),
                        IsPractice = c.Boolean(nullable: false),
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
                        TestTime = c.Int(nullable: false),
                        TheorySectionId = c.Int(nullable: false),
                        IsLast = c.Boolean(nullable: false),
                        IsInitial = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TheorySections", t => t.TheorySectionId, cascadeDelete: false)
                .Index(t => t.TheorySectionId);
            
            CreateTable(
                "dbo.TheorySections",
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
                        CorrectAnswer = c.String(),
                        TestType = c.Int(nullable: false),
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
                        Text = c.String(),
                        ImagePath = c.String(),
                        Options = c.String(),
                        AnswerNumber = c.Int(nullable: false),
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
                "dbo.UserTheoryDocumentMarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocumentId = c.Int(),
                        UserId = c.Int(nullable: false),
                        TheoryPartId = c.Int(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TheoryDocuments", t => t.DocumentId)
                .ForeignKey("dbo.TheoryParts", t => t.TheoryPartId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.DocumentId)
                .Index(t => t.UserId)
                .Index(t => t.TheoryPartId);
            
            CreateTable(
                "dbo.UserTheoryTestMarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        Attempt = c.Int(nullable: false),
                        Result = c.Double(nullable: false),
                        UserId = c.Int(nullable: false),
                        TheoryPartId = c.Int(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TheoryParts", t => t.TheoryPartId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.TheoryPartId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTheoryTestMarks", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserTheoryTestMarks", "TheoryPartId", "dbo.TheoryParts");
            DropForeignKey("dbo.UserTheoryDocumentMarks", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPartId", "dbo.TheoryParts");
            DropForeignKey("dbo.UserTheoryDocumentMarks", "DocumentId", "dbo.TheoryDocuments");
            DropForeignKey("dbo.TheoryDocuments", "TheoryPartId", "dbo.TheoryParts");
            DropForeignKey("dbo.TheoryTestParts", "TheoryId", "dbo.TheoryParts");
            DropForeignKey("dbo.TheoryTestPartAnswers", "TestPartId", "dbo.TheoryTestParts");
            DropForeignKey("dbo.TheoryParts", "TheorySectionId", "dbo.TheorySections");
            DropIndex("dbo.UserTheoryTestMarks", new[] { "TheoryPartId" });
            DropIndex("dbo.UserTheoryTestMarks", new[] { "UserId" });
            DropIndex("dbo.UserTheoryDocumentMarks", new[] { "TheoryPartId" });
            DropIndex("dbo.UserTheoryDocumentMarks", new[] { "UserId" });
            DropIndex("dbo.UserTheoryDocumentMarks", new[] { "DocumentId" });
            DropIndex("dbo.TheoryTestPartAnswers", new[] { "TestPartId" });
            DropIndex("dbo.TheoryTestParts", new[] { "TheoryId" });
            DropIndex("dbo.TheoryParts", new[] { "TheorySectionId" });
            DropIndex("dbo.TheoryDocuments", new[] { "TheoryPartId" });
            DropTable("dbo.UserTheoryTestMarks");
            DropTable("dbo.UserTheoryDocumentMarks");
            DropTable("dbo.Users");
            DropTable("dbo.TheoryTestPartAnswers");
            DropTable("dbo.TheoryTestParts");
            DropTable("dbo.TheorySections");
            DropTable("dbo.TheoryParts");
            DropTable("dbo.TheoryDocuments");
        }
    }
}
