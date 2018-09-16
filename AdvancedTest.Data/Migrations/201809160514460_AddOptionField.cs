namespace AdvancedTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOptionField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTheoryTestMarks", "OptionId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTheoryTestMarks", "OptionId");
        }
    }
}
