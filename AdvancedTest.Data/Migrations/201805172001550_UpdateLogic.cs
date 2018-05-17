namespace AdvancedTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLogic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TheoryParts", "IsLast", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TheoryParts", "IsLast");
        }
    }
}
