using AdvancedTest.Data.Extensions;

namespace AdvancedTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertPracticeParts : DbMigration
    {
        public override void Up()
        {
            Sql("201810101556135_InsertPracticeParts".GetEmbeddedSqlMigration("TheoryPart_Add_Practice_Parts.sql"));
        }
        
        public override void Down()
        {
        }
    }
}
