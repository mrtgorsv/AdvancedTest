namespace AdvancedTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TheoryTestPartAnswers", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TheoryTestPartAnswers", "ImagePath");
        }
    }
}
