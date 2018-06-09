namespace VkWcfServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VkSearchFilters", "FirstContact", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VkSearchFilters", "FirstContact");
        }
    }
}
