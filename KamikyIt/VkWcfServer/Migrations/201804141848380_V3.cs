namespace VkWcfServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VkSearchFilters", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VkSearchFilters", "Name");
        }
    }
}
