﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiWrapper.Core;
using VkNet.Enums.Filters;
using VkNet.Examples.ForChat;

namespace VkNet.Examples.Core
{
	public static class VkApiInstrument
	{
		static VkApiInstrument()
		{
			OnAuthorization += SetVkApiInstrumentProfile;
			OnAuthorization += SetSearchInstrumentProfile;
			OnAuthorization += SetChatCoreInstance;
		}

		private static void SetChatCoreInstance(object sender, KeyValuePair<string, string> e)
		{
			ChatCoreHelper.SetVkApiInstance(api);
		}

		private static void SetVkApiInstrumentProfile(object sender, KeyValuePair<string, string> e)
		{
			api = new VkNet.VkApi();

			api.Authorize(new ApiAuthParams
			{
				ApplicationId = 6394527,
				Login = e.Key,
				Password = e.Value,
				Settings = Settings.All,
				TwoFactorAuthorization = () =>
				{
					Console.WriteLine("Enter Code:");
					return Console.ReadLine();
				}
			});
		}

		private static VkApi api;

		private static void SetSearchInstrumentProfile(object sender, KeyValuePair<string, string> e)
		{
			SearchInstrument.SetAuthorization(api);
		}

		public static bool Login( string login, string password, out long userId, out string error)
		{
			userId = 0;

			try
			{
				OnAuthorization(null, new KeyValuePair<string, string>(login, password));
				error = "";

				userId = api.UserId.HasValue ? api.UserId.Value : 0;

				return true;
			}
			catch (System.Exception e)
			{
				error = e.Message;
				return false;
			}
		}

		private static EventHandler<KeyValuePair<string, string>> OnAuthorization;
	}
}
