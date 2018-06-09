using System;
using System.Runtime.Serialization;

namespace ContractInterfaces
{
	[DataContract]
	public class FilterModel
	{
		[DataMember]
		/// <summary>
		/// Наименование фильтра.
		/// </summary>
		public string Name { get; set; }

		[DataMember]
		/// <summary>
		/// Страна.
		/// </summary>
		public string Coutry { get; set; }

		[DataMember]
		/// <summary>
		/// Город.
		/// </summary>
		public string City { get; set; }

		[DataMember]
		/// <summary>
		/// Возраст.
		/// </summary>
		public IntervalValue<int> Years { get; set; }

		/// <summary>
		/// Количество друзей.
		/// </summary>
		[DataMember]
		public IntervalValue<int> FriendsCount { get; set; }

		/// <summary>
		/// Количество подписчиков.
		/// </summary>
		[DataMember]
		public IntervalValue<int> SubsCount { get; set; }

		/// <summary>
		/// Количество постов.
		/// </summary>
		[DataMember]
		public IntervalValue<int> PostCount { get; set; }

		[DataMember]
		/// <summary>
		/// С Фотографией.
		/// </summary>
		public bool? HasPhoto { get; set; }

		[DataMember]
		/// <summary>
		/// Сейчас Онлайн.
		/// </summary>
		public bool? IsOnline { get; set; }

		[DataMember]
		/// <summary>
		/// Пол.
		/// </summary>
		public Sex? Sex { get; set; }

		[DataMember]
		/// <summary>
		/// Семейное положение.
		/// </summary>
		public MyFamilyStatus? FamilyStatus { get; set; }

		[DataMember]
		/// <summary>
		/// Дружественность.
		/// </summary>
		public FriendStatus? FriendStatus { get; set; }

		[DataMember]
		/// <summary>
		/// Впервые контактируешь.
		/// </summary>
		public bool FirstContact { get; set; }

		[DataMember]
		/// <summary>
		/// Оффсет в поиске.
		/// </summary>
		public int Offset { get; set; }

		/// <summary>
		/// Сортировка.
		/// </summary>
		[DataMember]
		public SearchSortBy SortBy { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public FilterModel CopyFilter()
		{
			var newFilter = (FilterModel)this.MemberwiseClone();

			newFilter.Years = new IntervalValue<int>()
			{
				Min = this.Years.Min,
				Max = this.Years.Max,
			};

			newFilter.FriendsCount = new IntervalValue<int>()
			{
				Min = this.FriendsCount.Min,
				Max = this.FriendsCount.Max,
			};

			newFilter.SubsCount = new IntervalValue<int>()
			{
				Min = this.FriendsCount.Min,
				Max = this.FriendsCount.Max,
			};

			newFilter.PostCount = new IntervalValue<int>()
			{
				Min = this.FriendsCount.Min,
				Max = this.FriendsCount.Max,
			};

			return newFilter;
		}
	}


	/// <summary>
	/// Пол.
	/// </summary>
	public enum Sex
	{
		Unknown = 0,
		Female,
		Male,
	}

	/// <summary>
	/// Семейное положение.
	/// </summary>
	[Flags()]
	public enum MyFamilyStatus
	{
		//
		// Сводка:
		//     Не женат
		Single = 1,
		//
		// Сводка:
		//     Встречается
		Meets = 2,
		//
		// Сводка:
		//     Помолвлен
		Engaged = 4,
		//
		// Сводка:
		//     Женат
		Married = 8,
		//
		// Сводка:
		//     Всё сложно
		ItsComplicated = 16,
		//
		// Сводка:
		//     В активном поиске
		TheActiveSearch = 32,
		//
		// Сводка:
		//     Влюблён
		InLove = 64
	}

	/// <summary>
	/// Состояние дружбы с пользователями.
	/// </summary>
	public enum FriendStatus
	{
		//
		// Сводка:
		//     Пользователь не является другом.
		NotFriend = 0,
		//
		// Сводка:
		//     Пользователю отправлена заявка/подписка.
		OutputRequest = 1,
		//
		// Сводка:
		//     Имеется входящая заявка/подписка от пользователя.
		InputRequest = 2,
		//
		// Сводка:
		//     Пользователь является другом.
		Friend = 3
	}

	/// <summary>
	/// Сортировка поиска.
	/// </summary>
	public enum SearchSortBy
	{
		None,
		ByDate,
		ByPopular,
	}
}