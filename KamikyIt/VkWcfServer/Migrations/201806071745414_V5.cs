namespace VkWcfServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VkSearchFilters", "MinFriendsCount", c => c.Int());
            AddColumn("dbo.VkSearchFilters", "MaxFriendsCount", c => c.Int());
            AddColumn("dbo.VkSearchFilters", "MinSubsCount", c => c.Int());
            AddColumn("dbo.VkSearchFilters", "MaxSubsCount", c => c.Int());
            AddColumn("dbo.VkSearchFilters", "Offset", c => c.Int(nullable: false));
            AddColumn("dbo.VkSearchFilters", "SortBy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VkSearchFilters", "SortBy");
            DropColumn("dbo.VkSearchFilters", "Offset");
            DropColumn("dbo.VkSearchFilters", "MaxSubsCount");
            DropColumn("dbo.VkSearchFilters", "MinSubsCount");
            DropColumn("dbo.VkSearchFilters", "MaxFriendsCount");
            DropColumn("dbo.VkSearchFilters", "MinFriendsCount");
        }
    }
}
