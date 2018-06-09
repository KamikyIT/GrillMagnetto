using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ContractInterfaces
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
		void AddSearchFilter(FilterModel newFilter, string newName);
	}
}