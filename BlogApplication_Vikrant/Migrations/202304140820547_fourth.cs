namespace BlogApplication_Vikrant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "PostedName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "PostedName");
        }
    }
}
