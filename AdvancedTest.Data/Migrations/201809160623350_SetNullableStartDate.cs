namespace AdvancedTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetNullableStartDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserTheoryTestMarks", "StartTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserTheoryTestMarks", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
