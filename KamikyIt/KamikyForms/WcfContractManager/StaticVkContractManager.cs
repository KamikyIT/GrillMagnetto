using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ContractInterfaces;

namespace KamikyForms.WcfContractManager
{
	public static class StaticVkContractManager
	{
		static StaticVkContractManager()
		{
			SetFactoryUri();

			UseServer = false;
		}

		public static IVkContract GetVkContractInstance()
		{
			return _factory.CreateChannel();
		}

		private static ChannelFactory<IVkContract> _factory;

		public static void SetFactoryUri(string uri = @"http://localhost:4000/IVkContract")
		{
			_factory = new ChannelFactory<IVkContract>(new BasicHttpBinding(), new EndpointAddress(uri));
		}



		public static bool UseServer { get; set; }
	}
}
