using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ContractInterfaces
{
	[DataContract]
	public class FilterModel
	{

		public FilterModel(bool initIntervals = true)
		{
			if (initIntervals)
			{
				Years = new IntervalValue<int>();
				FriendsCount = new IntervalValue<int>();
				SubsCount = new IntervalValue<int>();
				HasPhoto = false;
				IsOnline = false;
				Sex = ContractInterfaces.Sex.Unknown;
				FamilyStatus = MyFamilyStatus.Single;
				FriendStatus = ContractInterfaces.FriendStatus.NotFriend;
			}
		}

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

		[DataMember]
		public int CommonFriends { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public FilterModel CopyFilter()
		{
			var newFilter = (FilterModel)this.MemberwiseClone();

			newFilter.Years = this.Years == null ? null : this.Years.CloneInterval();

			newFilter.FriendsCount = this.FriendsCount == null ? null : this.FriendsCount.CloneInterval();

			newFilter.SubsCount = this.SubsCount == null ? null : this.SubsCount.CloneInterval();

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
		[Display(Name="Не женат")]
		Single = 1,
		//
		// Сводка:
		//     Встречается
		[Display(Name = "Встречается")]
		Meets = 2,
		//
		// Сводка:
		//     Помолвлен
		[Display(Name = "Помолвлен")]
		Engaged = 4,
		//
		// Сводка:
		//     Женат
		[Display(Name = "Женат")]
		Married = 8,
		//
		// Сводка:
		//     Всё сложно
		[Display(Name = "Всё сложно")]
		ItsComplicated = 16,
		//
		// Сводка:
		//     В активном поиске
		[Display(Name = "В активном поиске")]
		TheActiveSearch = 32,
		//
		// Сводка:
		//     Влюблён
		[Display(Name = "Влюблён")]
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
		[Display(Name = "Пользователь не является другом")]
		NotFriend = 0,
		//
		// Сводка:
		//     Пользователю отправлена заявка/подписка.
		[Display(Name = "Пользователю отправлена заявка/подписка")]
		OutputRequest = 1,
		//
		// Сводка:
		//     Имеется входящая заявка/подписка от пользователя.
		[Display(Name = "Имеется входящая заявка/подписка от пользователя")]
		InputRequest = 2,
		//
		// Сводка:
		//     Пользователь является другом.
		[Display(Name = "Пользователь является другом")]
		Friend = 3
	}

	/// <summary>
	/// Сортировка поиска.
	/// </summary>
	public enum SearchSortBy
	{
		None,
		[Display(Name = "По дате регистрации")]
		ByDate,
		[Display(Name = "По популярности")]
		ByPopular,
	}
}