using System.Data.Entity;

namespace VkWcfServer
{
	public class VkContext : DbContext
	{
		public VkContext() : base("DbConnection")
		{
		}

		public DbSet<VkLogin> VkLogins { get; set; }

		public DbSet<VkSearchFilter> VkSearchFilters { get; set; }
	}
}