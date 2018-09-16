namespace AdvancedTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetNullableTheoryId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPartId", "dbo.TheoryParts");
            DropForeignKey("dbo.UserTheoryTestMarks", "TheoryPartId", "dbo.TheoryParts");
            DropIndex("dbo.UserTheoryDocumentMarks", new[] { "TheoryPartId" });
            DropIndex("dbo.UserTheoryTestMarks", new[] { "TheoryPartId" });
            AlterColumn("dbo.UserTheoryDocumentMarks", "TheoryPartId", c => c.Int());
            AlterColumn("dbo.UserTheoryTestMarks", "TheoryPartId", c => c.Int());
            CreateIndex("dbo.UserTheoryDocumentMarks", "TheoryPartId");
            CreateIndex("dbo.UserTheoryTestMarks", "TheoryPartId");
            AddForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPartId", "dbo.TheoryParts", "Id");
            AddForeignKey("dbo.UserTheoryTestMarks", "TheoryPartId", "dbo.TheoryParts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTheoryTestMarks", "TheoryPartId", "dbo.TheoryParts");
            DropForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPartId", "dbo.TheoryParts");
            DropIndex("dbo.UserTheoryTestMarks", new[] { "TheoryPartId" });
            DropIndex("dbo.UserTheoryDocumentMarks", new[] { "TheoryPartId" });
            AlterColumn("dbo.UserTheoryTestMarks", "TheoryPartId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserTheoryDocumentMarks", "TheoryPartId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserTheoryTestMarks", "TheoryPartId");
            CreateIndex("dbo.UserTheoryDocumentMarks", "TheoryPartId");
            AddForeignKey("dbo.UserTheoryTestMarks", "TheoryPartId", "dbo.TheoryParts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPartId", "dbo.TheoryParts", "Id", cascadeDelete: true);
        }
    }
}
