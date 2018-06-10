namespace VkWcfServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VkSearchFilters", "MinPostsCount", c => c.Int());
            AddColumn("dbo.VkSearchFilters", "MaxPostsCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VkSearchFilters", "MaxPostsCount");
            DropColumn("dbo.VkSearchFilters", "MinPostsCount");
        }
    }
}
