namespace VkWcfServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VkSearchFilters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Coutry = c.String(),
                        City = c.String(),
                        MinYear = c.Int(),
                        MaxYear = c.Int(),
                        HasPhoto = c.Boolean(),
                        IsOnline = c.Boolean(),
                        Sex = c.Int(),
                        FamilyStatus = c.Int(),
                        FriendStatus = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VkSearchFilters");
        }
    }
}
