namespace AdvancedTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDocLogic : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPart_Id", "dbo.TheoryParts");
            DropIndex("dbo.UserTheoryDocumentMarks", new[] { "TheoryPart_Id" });
            RenameColumn(table: "dbo.UserTheoryDocumentMarks", name: "TheoryPart_Id", newName: "TheoryPartId");
            AddColumn("dbo.TheoryParts", "TestLength", c => c.Int(nullable: false));
            AlterColumn("dbo.UserTheoryDocumentMarks", "TheoryPartId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserTheoryDocumentMarks", "TheoryPartId");
            AddForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPartId", "dbo.TheoryParts", "Id",cascadeDelete: false);
            DropColumn("dbo.UserTheoryDocumentMarks", "Attempt");
            DropColumn("dbo.UserTheoryDocumentMarks", "Result");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserTheoryDocumentMarks", "Result", c => c.Double(nullable: false));
            AddColumn("dbo.UserTheoryDocumentMarks", "Attempt", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPartId", "dbo.TheoryParts");
            DropIndex("dbo.UserTheoryDocumentMarks", new[] { "TheoryPartId" });
            AlterColumn("dbo.UserTheoryDocumentMarks", "TheoryPartId", c => c.Int());
            DropColumn("dbo.TheoryParts", "TestLength");
            RenameColumn(table: "dbo.UserTheoryDocumentMarks", name: "TheoryPartId", newName: "TheoryPart_Id");
            CreateIndex("dbo.UserTheoryDocumentMarks", "TheoryPart_Id");
            AddForeignKey("dbo.UserTheoryDocumentMarks", "TheoryPart_Id", "dbo.TheoryParts", "Id");
        }
    }
}
