using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VkWcfServer
{
	public class VkLogin
	{
		public int Id { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public string VkUrlPage { get; set; }
	}
}