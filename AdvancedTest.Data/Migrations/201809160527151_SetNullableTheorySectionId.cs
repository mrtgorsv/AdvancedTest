namespace AdvancedTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetNullableTheorySectionId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TheoryParts", "TheorySectionId", "dbo.TheorySections");
            DropForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPartId", "dbo.TheoryParts");
            DropForeignKey("dbo.UserTheoryTestMarks", "TheoryPartId", "dbo.TheoryParts");
            DropIndex("dbo.TheoryParts", new[] { "TheorySectionId" });
            DropIndex("dbo.UserTheoryDocumentMarks", new[] { "TheoryPartId" });
            DropIndex("dbo.UserTheoryTestMarks", new[] { "TheoryPartId" });
            AlterColumn("dbo.TheoryParts", "TheorySectionId", c => c.Int());
            AlterColumn("dbo.UserTheoryDocumentMarks", "TheoryPartId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserTheoryTestMarks", "TheoryPartId", c => c.Int(nullable: false));
            CreateIndex("dbo.TheoryParts", "TheorySectionId");
            CreateIndex("dbo.UserTheoryDocumentMarks", "TheoryPartId");
            CreateIndex("dbo.UserTheoryTestMarks", "TheoryPartId");
            AddForeignKey("dbo.TheoryParts", "TheorySectionId", "dbo.TheorySections", "Id");
            AddForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPartId", "dbo.TheoryParts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserTheoryTestMarks", "TheoryPartId", "dbo.TheoryParts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTheoryTestMarks", "TheoryPartId", "dbo.TheoryParts");
            DropForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPartId", "dbo.TheoryParts");
            DropForeignKey("dbo.TheoryParts", "TheorySectionId", "dbo.TheorySections");
            DropIndex("dbo.UserTheoryTestMarks", new[] { "TheoryPartId" });
            DropIndex("dbo.UserTheoryDocumentMarks", new[] { "TheoryPartId" });
            DropIndex("dbo.TheoryParts", new[] { "TheorySectionId" });
            AlterColumn("dbo.UserTheoryTestMarks", "TheoryPartId", c => c.Int());
            AlterColumn("dbo.UserTheoryDocumentMarks", "TheoryPartId", c => c.Int());
            AlterColumn("dbo.TheoryParts", "TheorySectionId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserTheoryTestMarks", "TheoryPartId");
            CreateIndex("dbo.UserTheoryDocumentMarks", "TheoryPartId");
            CreateIndex("dbo.TheoryParts", "TheorySectionId");
            AddForeignKey("dbo.UserTheoryTestMarks", "TheoryPartId", "dbo.TheoryParts", "Id");
            AddForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPartId", "dbo.TheoryParts", "Id");
            AddForeignKey("dbo.TheoryParts", "TheorySectionId", "dbo.TheorySections", "Id", cascadeDelete: true);
        }
    }
}
