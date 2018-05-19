namespace AdvancedTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPracticeDocs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TheoryDocuments", "IsPractice", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TheoryDocuments", "IsPractice");
        }
    }
}
