using System;
using System.Collections.Generic;
using ContractInterfaces;
using System.Linq;
using System.Net.Http.Headers;

namespace VkWcfServer
{
	public class VkService : IVkContract
	{
		public void CreateNewLogin(string login, string password, string vkUrlPage, Exception exc)
		{
			var log = string.Format("CreateNewLogin({0}, {1}, {2})", login, password, vkUrlPage);

			try
			{
				exc = null;

				using (var context = new VkContext())
				{
					var exists = context.VkLogins.FirstOrDefault(x => x.Login == login);

					if (exists != null)
					{
						exists.Password = password;
						exists.VkUrlPage = vkUrlPage;

						log += "; change Password";
					}
					else
					{
						context.VkLogins.Add(new VkLogin()
						{
							Login = login,
							Password = password,
							VkUrlPage = vkUrlPage,
						});
					}

					context.SaveChanges();
				}
			}
			catch (Exception e)
			{
				StaticLog.LogException(e);
				exc = e;
			}

			StaticLog.LogServiceCall(log);
		}

		public void DeleteLogin(string login, Exception exc)
		{
			StaticLog.LogServiceCall(string.Format("DeleteLogin({0})", login));

			try
			{
				using (var context = new VkContext())
				{
					var exist = context.VkLogins.FirstOrDefault(x => x.Login == login);

					if (exist == null)
						throw new Exception("Данный логин не существует : " + login);

					context.VkLogins.Remove(exist);

					context.SaveChanges();
				}
			}
			catch (Exception e)
			{
				StaticLog.LogException(e);
				exc = e;
			}
			
		}

		public string GetPasswordForLogin(string login)
		{
			StaticLog.LogServiceCall(string.Format("GetPasswordForLogin({0})", login));

			try
			{
				using (var context = new VkContext())
				{
					var exist = context.VkLogins.FirstOrDefault(x => x.Login == login);

					if (exist == null)
						throw new Exception("Данный логин не существует : " + login);

					return exist.Password;
				}
			}
			catch (Exception e)
			{
				StaticLog.LogException(e);

				return "";
			}
		}

		public IEnumerable<string> GetAllLogins(Exception exc)
		{
			StaticLog.LogServiceCall("GetAllLogins()");

			try
			{
				using (var context = new VkContext())
					return context.VkLogins.Select(x => x.Login).ToList();
			}
			catch (Exception e)
			{
				StaticLog.LogException(e);
				exc = e;
				return new List<string>();
			}
		}

		public List<FilterModel> GetAllSearchFilters()
		{
			StaticLog.LogServiceCall(string.Format("GetAllSearchFilters()"));

			try
			{
				using (var ctx = new VkContext())
				{
					var allFilters = new List<FilterModel>();

					foreach (var x in ctx.VkSearchFilters)
					{
						var filter = new FilterModel(false);

						VkSearchFilter.CopyToFilterModel(x, filter);

						allFilters.Add(filter);
					}

					return allFilters;
				}
			}
			catch (Exception e)
			{
				StaticLog.LogException(e);

				return null;
			}
		}

		public FilterModel GetSearchFilterByName(string name)
		{
			StaticLog.LogServiceCall(string.Format("GetAllSearchFilters({0})", name));

			try
			{
				using (var ctx = new VkContext())
				{
					var exist = ctx.VkSearchFilters.FirstOrDefault(x => x.Name == name);

					if (exist == null)
						throw new Exception("Фильтра с заданным именем не найден.");

					return new FilterModel()
					{
						Name = exist.Name,
						City = exist.City,
						Coutry = exist.Coutry,
						FamilyStatus = exist.FamilyStatus,
						FriendStatus = exist.FriendStatus,
						HasPhoto = exist.HasPhoto,
						IsOnline = exist.IsOnline,
						Sex = exist.Sex,
						Years = exist.MinYear.HasValue && exist.MaxYear.HasValue ? new IntervalValue<int>()
						{
							Min = exist.MinYear.Value,
							Max = exist.MaxYear.Value,
						} : null,
					};
				}
			}
			catch (Exception e)
			{
				StaticLog.LogException(e);

				return null;
			}

		}

		public void DeleteSearchFilterByName(string name)
		{
			StaticLog.LogServiceCall(string.Format("DeleteSearchFilterByName({0})", name));

			try
			{
				using (var ctx = new VkContext())
				{
					var exist = ctx.VkSearchFilters.FirstOrDefault(x => x.Name == name);

					if (exist == null)
						throw new Exception("Фильтра с заданным именем не найден : " + name);

					ctx.VkSearchFilters.Remove(exist);

					ctx.SaveChanges();
				}
			}
			catch (Exception e)
			{
				StaticLog.LogException(e);
			}
		}

		public void UpdateSearchFilter(FilterModel filter)
		{
			StaticLog.LogServiceCall(string.Format("UpdateSearchFilter({0})", filter));

			try
			{
				using (var ctx = new VkContext())
				{
					var exist = ctx.VkSearchFilters.FirstOrDefault(x => x.Name == filter.Name);

					if (exist == null)
						throw new Exception("Фильтра с заданным именем не найден : " + filter.Name);

					exist.CopyProps(filter);

					ctx.SaveChanges();
				}
			}
			catch (Exception e)
			{
				StaticLog.LogException(e);
			}
		}

		public void AddSearchFilter(FilterModel newFilter, string newName)
		{
			StaticLog.LogServiceCall(string.Format("AddSearchFilter({0})", newFilter));

			try
			{
				using (var ctx = new VkContext())
				{
					var exist = ctx.VkSearchFilters.FirstOrDefault(x => x.Name == newFilter.Name);

					if (exist != null)
						throw new Exception("Фильтр с заданным именем уже существует : " + newFilter.Name);
					
					var newVkSearchFilter = new VkSearchFilter(newFilter);

					newVkSearchFilter.Name = newName;

					ctx.VkSearchFilters.Add(newVkSearchFilter);

					ctx.SaveChanges();
				}
			}
			catch (Exception e)
			{
				StaticLog.LogException(e);
			}
		}
	}

	public static class StaticLog
	{
		public static event Action<string> LogEvent;

		public static void LogException(Exception e)
		{
			LogEvent(e.Message);
		}

		public static void LogException(string message)
		{
			LogEvent(message);
		}

		public static void LogServiceCall(string message)
		{
			LogEvent(message);
		}
	}
}