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
		[Display(Name="�� �����")]
		Single = 1,
		//
		// ������:
		//     �����������
		[Display(Name = "�����������")]
		Meets = 2,
		//
		// ������:
		//     ���������
		[Display(Name = "���������")]
		Engaged = 4,
		//
		// ������:
		//     �����
		[Display(Name = "�����")]
		Married = 8,
		//
		// ������:
		//     �� ������
		[Display(Name = "�� ������")]
		ItsComplicated = 16,
		//
		// ������:
		//     � �������� ������
		[Display(Name = "� �������� ������")]
		TheActiveSearch = 32,
		//
		// ������:
		//     ������
		[Display(Name = "������")]
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
		[Display(Name = "������������ �� �������� ������")]
		NotFriend = 0,
		//
		// ������:
		//     ������������ ���������� ������/��������.
		[Display(Name = "������������ ���������� ������/��������")]
		OutputRequest = 1,
		//
		// ������:
		//     ������� �������� ������/�������� �� ������������.
		[Display(Name = "������� �������� ������/�������� �� ������������")]
		InputRequest = 2,
		//
		// ������:
		//     ������������ �������� ������.
		[Display(Name = "������������ �������� ������")]
		Friend = 3
	}

	/// <summary>
	/// ���������� ������.
	/// </summary>
	public enum SearchSortBy
	{
		None,
		[Display(Name = "�� ���� �����������")]
		ByDate,
		[Display(Name = "�� ������������")]
		ByPopular,
	}
}