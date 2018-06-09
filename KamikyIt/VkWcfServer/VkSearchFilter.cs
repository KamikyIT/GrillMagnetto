using System.Data.Entity.Migrations.Builders;
using ContractInterfaces;

namespace VkWcfServer
{
	public class VkSearchFilter
	{
		public int Id { get; set; }

		/// <summary>
		/// Наименование фильтра.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Страна.
		/// </summary>
		public string Coutry { get; set; }

		/// <summary>
		/// Город.
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// Возраст.
		/// </summary>
		public int? MinYear { get; set; }

		public int? MaxYear { get; set; }

		/// <summary>
		/// С Фотографией.
		/// </summary>
		public bool? HasPhoto { get; set; }

		/// <summary>
		/// Сейчас Онлайн.
		/// </summary>
		public bool? IsOnline { get; set; }  

		/// <summary>
		/// Пол.
		/// </summary>
		public Sex? Sex { get; set; }

		/// <summary>
		/// Семейное положение.
		/// </summary>
		public MyFamilyStatus? FamilyStatus { get; set; }

		/// <summary>
		/// Дружественность.
		/// </summary>
		public FriendStatus? FriendStatus { get; set; }

		/// <summary>
		/// Количество друзей.
		/// </summary>
		public int? MinFriendsCount { get; set; }

		/// <summary>
		/// Количество друзей.
		/// </summary>
		public int? MaxFriendsCount { get; set; }

		/// <summary>
		/// Количество подписчиков.
		/// </summary>
		public int? MinSubsCount { get; set; }

		/// <summary>
		/// Количество подписчиков.
		/// </summary>
		public int? MaxSubsCount { get; set; }

		/// <summary>
		/// Оффсет в поиске.
		/// </summary>
		public int Offset { get; set; }

		/// <summary>
		/// Сортировка.
		/// </summary>
		public SearchSortBy SortBy { get; set; }

		/// <summary>
		/// Впервые контактируешь.
		/// </summary>
		public bool? FirstContact { get; set; }

		public VkSearchFilter()
		{
			
		}

		public VkSearchFilter(FilterModel copy)
		{
			Name = copy.Name;
			Coutry = copy.Coutry;
			City = copy.City;

			MinYear = copy.Years != null? copy.Years.Min : 0;
			MaxYear = copy.Years != null? copy.Years.Max : 0;

			HasPhoto = copy.HasPhoto;

			IsOnline = copy.IsOnline;

			Sex = copy.Sex;

			FamilyStatus = copy.FamilyStatus;

			FriendStatus = copy.FriendStatus;

			FirstContact = copy.FirstContact;
		}

		public void CopyProps(FilterModel copy)
		{
			Coutry = copy.Coutry;
			City = copy.City;

			MinYear = copy.Years != null ? copy.Years.Min : 0;
			MaxYear = copy.Years != null ? copy.Years.Max : 0;

			HasPhoto = copy.HasPhoto;

			IsOnline = copy.IsOnline;

			Sex = copy.Sex;

			FamilyStatus = copy.FamilyStatus;

			FriendStatus = copy.FriendStatus;

			FirstContact = copy.FirstContact;
		}
	}
}