namespace VkWcfServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VkSearchFilters", "CommonFriendsCount", c => c.Int(nullable: false));
            DropColumn("dbo.VkSearchFilters", "MinPostsCount");
            DropColumn("dbo.VkSearchFilters", "MaxPostsCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VkSearchFilters", "MaxPostsCount", c => c.Int());
            AddColumn("dbo.VkSearchFilters", "MinPostsCount", c => c.Int());
            DropColumn("dbo.VkSearchFilters", "CommonFriendsCount");
        }
    }
}
