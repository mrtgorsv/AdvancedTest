namespace AdvancedTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInitialFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TheoryParts", "IsInitial", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TheoryParts", "IsInitial");
        }
    }
}
