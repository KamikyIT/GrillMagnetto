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
		/// Количество постов на стене.
		/// </summary>
		public int? MinPostsCount { get; set; }

		/// <summary>
		/// Количество постов на стене.
		/// </summary>
		public int? MaxPostsCount { get; set; }

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
			this.CopyProps(copy);
		}

		public void CopyProps(FilterModel copy)
		{
			Name = copy.Name;
			City = copy.City;
			Coutry = copy.Coutry;
			FamilyStatus = copy.FamilyStatus;
			FirstContact = copy.FirstContact;
			FriendStatus = copy.FriendStatus;
			HasPhoto = copy.HasPhoto;
			IsOnline = copy.IsOnline;
			MinFriendsCount = copy.FriendsCount == null ? default(int?) : copy.FriendsCount.Min;
			MaxFriendsCount = copy.FriendsCount == null ? default(int?) : copy.FriendsCount.Max;
			MinSubsCount = copy.SubsCount == null ? default(int?) : copy.SubsCount.Min;
			MaxSubsCount = copy.SubsCount == null ? default(int?) : copy.SubsCount.Max;
			MinPostsCount = copy.PostCount == null ? default(int?) : copy.PostCount.Min;
			MaxPostsCount = copy.PostCount == null ? default(int?) : copy.PostCount.Max;
			MinYear = copy.Years == null ? default(int?) : copy.Years.Min;
			MaxYear = copy.Years == null ? default(int?) : copy.Years.Max;
			Offset = copy.Offset;
			Sex = copy.Sex;
			SortBy = copy.SortBy;
		}

		public static void CopyToFilterModel(VkSearchFilter copy, FilterModel filter)
		{
			filter.Name = copy.Name;
			filter.City = copy.City;
			filter.Coutry = copy.Coutry;
			filter.FamilyStatus = copy.FamilyStatus;
			filter.FirstContact = false;
			filter.FriendStatus = copy.FriendStatus;
			filter.HasPhoto = copy.HasPhoto;
			filter.IsOnline = copy.IsOnline;
			filter.FriendsCount = copy.MinFriendsCount.HasValue && copy.MaxFriendsCount.HasValue
				? new IntervalValue<int>(copy.MinFriendsCount.Value, copy.MaxFriendsCount.Value)
				: null;
			filter.SubsCount = copy.MinSubsCount.HasValue && copy.MaxSubsCount.HasValue ?  new IntervalValue<int>(copy.MinSubsCount.Value, copy.MaxSubsCount.Value) : null;
			filter.Years = copy.MinYear.HasValue && copy.MaxYear.HasValue ? new IntervalValue<int>(copy.MinYear.Value, copy.MaxYear.Value) : null;
			filter.PostCount = copy.MinPostsCount.HasValue && copy.MaxPostsCount.HasValue
				? new IntervalValue<int>(copy.MinPostsCount.Value, copy.MaxPostsCount.Value)
				: null;
			filter.Offset = copy.Offset;
			filter.Sex = copy.Sex;
			filter.SortBy = copy.SortBy;
		}
	}
}