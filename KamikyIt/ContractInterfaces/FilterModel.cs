using System;
using System.Runtime.Serialization;

namespace ContractInterfaces
{
	[DataContract]
	public class FilterModel
	{
		[DataMember]
		/// <summary>
		/// ������������ �������.
		/// </summary>
		public string Name { get; set; }

		[DataMember]
		/// <summary>
		/// ������.
		/// </summary>
		public string Coutry { get; set; }

		[DataMember]
		/// <summary>
		/// �����.
		/// </summary>
		public string City { get; set; }

		[DataMember]
		/// <summary>
		/// �������.
		/// </summary>
		public IntervalValue<int> Years { get; set; }

		/// <summary>
		/// ���������� ������.
		/// </summary>
		[DataMember]
		public IntervalValue<int> FriendsCount { get; set; }

		/// <summary>
		/// ���������� �����������.
		/// </summary>
		[DataMember]
		public IntervalValue<int> SubsCount { get; set; }

		/// <summary>
		/// ���������� ������.
		/// </summary>
		[DataMember]
		public IntervalValue<int> PostCount { get; set; }

		[DataMember]
		/// <summary>
		/// � �����������.
		/// </summary>
		public bool? HasPhoto { get; set; }

		[DataMember]
		/// <summary>
		/// ������ ������.
		/// </summary>
		public bool? IsOnline { get; set; }

		[DataMember]
		/// <summary>
		/// ���.
		/// </summary>
		public Sex? Sex { get; set; }

		[DataMember]
		/// <summary>
		/// �������� ���������.
		/// </summary>
		public MyFamilyStatus? FamilyStatus { get; set; }

		[DataMember]
		/// <summary>
		/// ���������������.
		/// </summary>
		public FriendStatus? FriendStatus { get; set; }

		[DataMember]
		/// <summary>
		/// ������� �������������.
		/// </summary>
		public bool FirstContact { get; set; }

		[DataMember]
		/// <summary>
		/// ������ � ������.
		/// </summary>
		public int Offset { get; set; }

		/// <summary>
		/// ����������.
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
	/// ���.
	/// </summary>
	public enum Sex
	{
		Unknown = 0,
		Female,
		Male,
	}

	/// <summary>
	/// �������� ���������.
	/// </summary>
	[Flags()]
	public enum MyFamilyStatus
	{
		//
		// ������:
		//     �� �����
		Single = 1,
		//
		// ������:
		//     �����������
		Meets = 2,
		//
		// ������:
		//     ���������
		Engaged = 4,
		//
		// ������:
		//     �����
		Married = 8,
		//
		// ������:
		//     �� ������
		ItsComplicated = 16,
		//
		// ������:
		//     � �������� ������
		TheActiveSearch = 32,
		//
		// ������:
		//     ������
		InLove = 64
	}

	/// <summary>
	/// ��������� ������ � ��������������.
	/// </summary>
	public enum FriendStatus
	{
		//
		// ������:
		//     ������������ �� �������� ������.
		NotFriend = 0,
		//
		// ������:
		//     ������������ ���������� ������/��������.
		OutputRequest = 1,
		//
		// ������:
		//     ������� �������� ������/�������� �� ������������.
		InputRequest = 2,
		//
		// ������:
		//     ������������ �������� ������.
		Friend = 3
	}

	/// <summary>
	/// ���������� ������.
	/// </summary>
	public enum SearchSortBy
	{
		None,
		ByDate,
		ByPopular,
	}
}