using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ContractsWpf
{
	[ServiceContract]
	public interface IVkContract
	{
		[OperationContract]
		void CreateNewLogin(string login, string password, string vkUrlPage, Exception exc);

		[OperationContract]
		void DeleteLogin(string login, Exception exc);

		[OperationContract]
		IEnumerable<string> GetAllLogins(Exception exc);

		[OperationContract]
		string GetPasswordForLogin(string login);

		[OperationContract]
		List<FilterModel> GetAllSearchFilters();

		[OperationContract]
		FilterModel GetSearchFilterByName(string name);

		[OperationContract]
		void DeleteSearchFilterByName(string name);

		[OperationContract]
		void UpdateSearchFilter(FilterModel filter);

		[OperationContract]
		void AddSearchFilter(FilterModel newFilter);
	}
}
