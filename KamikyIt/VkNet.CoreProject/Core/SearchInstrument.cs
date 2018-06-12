using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ContractInterfaces;
using VkNet;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Examples.DataBaseBehaviour;
using VkNet.Examples.ForChat;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using Sex = VkNet.Enums.Sex;

namespace ApiWrapper.Core
{
    public static class SearchInstrument
    {
	    private static List<string> Bans;

        static SearchInstrument()
        {
            Bans = FileParser.getBans();
		}

        public static ApiInstrumentEnum ApiInstrumentEnum;
        public static string ApiVersion;
        private static VkApi api;

	    public static List<PersonModel> GetPersons(FilterModel filter)
	    {
		    UserSearchParams userSearchParams = ConverToVkUserSearchParams(filter);

		    userSearchParams.Fields = ProfileFields.All | ProfileFields.Counters;

		    var peoples = api.Users.Search(userSearchParams);

		    var unbannedUsers = peoples.Where(x => !Bans.Contains(x.Domain));

		    var canWrite = unbannedUsers.Where(x => x.CanWritePrivateMessage && !x.Blacklisted);

		    var niceGrills = canWrite.Where(x => IsNormalGrill(x));

		    var myFilteredPeople = niceGrills.ToList();

			// Если не задали условия по количеству друзей и падпинчикиов.
		    if (filter.FriendsCount == null && filter.SubsCount == null)
			    return myFilteredPeople.Select(x => new PersonModel(x)).ToList();


		    // Иначе молиться Богам, чтобы нас не зобанили.
			var userCounters = new Dictionary<User, Counters>();

		    foreach (var ppl in myFilteredPeople)
		    {
			    var counters = api.Users.Get(new long[] {ppl.Id}, ProfileFields.Counters).FirstOrDefault().Counters;

			    userCounters.Add(ppl, counters);
		    }

		    foreach (var userCounter in userCounters)
			    userCounter.Key.Counters = userCounter.Value;

		    myFilteredPeople = userCounters.Keys.ToList();

		    IEnumerable<User> users = myFilteredPeople;


		    if (filter.FriendsCount != null)
		    {
			    users = users.Where(x =>
				    x.Counters.Friends.HasValue
					    ? x.Counters.Friends.Value >= filter.FriendsCount.Min
					    : true);

			    users = users.Where(x =>
				    x.Counters.Friends.HasValue
					    ? x.Counters.Friends.Value <= filter.FriendsCount.Max
					    : true);
		    }

		    if (filter.SubsCount != null)
		    {
			    users = users.Where(x =>
				    x.Counters.Followers.HasValue
					    ? x.Counters.Followers.Value >= filter.SubsCount.Min
					    : true);

			    users = users.Where(x =>
				    x.Counters.Followers.HasValue
					    ? x.Counters.Followers.Value <= filter.SubsCount.Max
					    : true);
		    }

		    return users.Select(x => new PersonModel(x)).ToList();
	    }

	    private static UserSearchParams ConverToVkUserSearchParams(FilterModel filter)
	    {
		    var userSearchParams = new UserSearchParams()
		    {
			    AgeFrom = filter.Years != null ? (ushort) filter.Years.Min : default(ushort?),
			    AgeTo = filter.Years != null ? (ushort) filter.Years.Max : default(ushort?),
			    Country = GetCountryId(filter.Coutry), //!string.IsNullOrEmpty(filter.Coutry) ? GetCountryId(filter.Coutry) : default(int?),
			    City = GetCityId(filter.City), // !string.IsNullOrEmpty(filter.City) ? GetCityId(filter.City) : default(int?),
			    Sex = filter.Sex.HasValue ? (VkNet.Enums.Sex) filter.Sex : Sex.Unknown,
			    HasPhoto = filter.HasPhoto,
			    Online = filter.IsOnline,
			    Offset = (uint) filter.Offset,
			    Status = filter.FamilyStatus.HasValue
				    ? (MaritalStatus) ((int) filter.FamilyStatus.Value)
				    : default(MaritalStatus?),
				Count = 1000,
		    };

			if (filter.SortBy != SearchSortBy.None)
				userSearchParams.Sort = filter.SortBy == SearchSortBy.ByDate? UserSort.ByRegDate : UserSort.ByPopularity;

		    return userSearchParams;
	    }

	    private static bool IsNormalGrill(User user)
	    {
		    return true;
	    }

		public static bool isNormalGirl(User user, SearchFilter filter)
        {

            if (user.FollowersCount > filter.SubsMax) return false;
            //if (user.Counters.Friends < filter.FriendMin) return false;
            //if (user.Counters.Friends > filter.FriendMax) return false;

            //if (user.Counters.Followers < filter.SubsMin) return false;
            //if (user.Counters.Followers > filter.SubsMax) return false;


            //if (user.Counters.Pages < filter.PostMin) return false;
            //if (user.Counters.Pages > filter.PostMax) return false;
            return true;
        }

        public static List<PersonModel> Test()
        {
            var filter = new SearchFilter()
            {
                AgeInterval = new IntInterval<int>(10, 30),

                CityId = 157,
                CountryId = 1,
                Sex = SexEnum.Woman,
                IsOnline = true,
                HasPhoto = true,
                FamilyState = FamilyState.ActiveSearch,
            };

            var peoples = api.Users.Search(new UserSearchParams()
            {
                AgeFrom = (ushort)filter.AgeInterval.MinYear,
                AgeTo = (ushort)filter.AgeInterval.MaxYear,
                City = filter.CityId,
                Country = filter.CountryId,
                HasPhoto = filter.HasPhoto,
                Online = filter.IsOnline,
                Sex = (VkNet.Enums.Sex)filter.Sex,
                GroupId = 22751485,
                Sort = (VkNet.Enums.UserSort)filter.profileSort,
                Count = 50,
                Fields = ProfileFields.All,
            });

            return peoples.Where(o => o.CanWritePrivateMessage && !o.Blacklisted).Select(x => new PersonModel(x)).ToList();
        }

        public static void SetApiInstrument(ApiInstrumentEnum api, string apiVersion = "5.73")
        {
            ApiInstrumentEnum = api;
            ApiVersion = apiVersion;
        }

        public static List<HumanInfo> GetHumansByFilter(SearchFilter filter)
        {
            if (filter == null)
                return null;

            if (filter.GroupsInfo != null && filter.GroupsInfo.GroupUrls.Any())
            {
                return GetHumansByGroupsFilter(filter);
            }

            return null;
        }

        private static List<HumanInfo> GetHumansByGroupsFilter(SearchFilter filter)
        {
            var result = new List<HumanInfo>();

            foreach (var groupInfo in filter.GroupsInfo.GroupUrls)
            {
                var groupPeoples = GetHumansInGroupByFilter(filter, groupInfo.GroupId);

                result.AddRange(groupPeoples);
            }

            return result;
        }

        private static IEnumerable<HumanInfo> GetHumansInGroupByFilter(SearchFilter filter, int groupId)
        {
            var sb = new StringBuilder();
            // Россия - 1. Чебок - 157. Двач - 22751485.
            sb.Append("https://api.vkontakte.ru/users.search?");

            sb.Append(string.Format("params[count]={0}&", filter.Count));

            if (string.IsNullOrEmpty(filter.Country) == false)
                sb.Append(string.Format("params[country]={0}&", GetCountryId(filter.Country)));

            if (string.IsNullOrEmpty(filter.City) == false)
                sb.Append(string.Format("params[city]={0}&", GetCityId(filter.City)));

            sb.Append(string.Format("params[sex]={0}&", (int)filter.Sex));

			if (filter.IsOnline.HasValue)
				sb.Append(string.Format("params[online]={0}=&", filter.IsOnline.Value ? 1 : 0));

			if (filter.HasPhoto.HasValue)
				sb.Append(string.Format("params[has_photo]={0}=&", filter.HasPhoto.Value ? 1 : 0));

            sb.Append(string.Format("params[v]={0}", ApiVersion));

            // /users.search?
            //params[count]=5&
            //params[fields]=photo%2Cscreen_name&
            //params[city]=157&
            //params[country]=1&
            //params[sex]=1&
            //params[online]=1&
            //params[has_photo]=1&
            //params[group_id]=22751485&
            //params[v]=5.73

            return null;
        }

        private static int GetCityId(string city)
        {
            return 157;
        }

        private static int GetCountryId(string country)
        {
            return 1;
        }

        public static void SendMessage(string id, string message)
        {
            api.Messages.Send(new MessagesSendParams()
            {
                Message = message,
                UserId = int.Parse(id),
            });
        }

        public static void SetAuthorization(VkApi vkApi)
        {
            api = vkApi;
        }

        public static User GetUser(long id)
        {
            var users = api.Users.Get(new List<long>() { id }, ProfileFields.All);

            return users.FirstOrDefault();
        }
    }

    public enum ApiInstrumentEnum
    {
        VkApi,
        InstagrammApi,
        // ...
    }

    /// <summary>
    /// Фильтр поиска челиков.
    /// </summary>
    public class SearchFilter
    {
        public int Count { get; set; }

        //Пол
        public SexEnum Sex { get; set; }

        // Семейное положение
        public FamilyState FamilyState { get; set; }

        // Возраст
        public IntInterval<int> AgeInterval { get; set; }

        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        // Страна
        public string Country { get; set; }
        public int CountryId { get; set; }

        // Город
        public string City { get; set; }
        public int CityId { get; set; }

        // Количество друзей
        public IntInterval<int> FriendsInterval { get; set; }

        // Количество пиздолизов.
        public IntInterval<int> SubscribersInterval { get; set; }

        // Дата регистрации
        public DateTime RegistrationDate { get; set; }

        // Сейчас онлайн
        public bool? IsOnline { get; set; }


        //сортировка по популярности
        public ProfileSort profileSort { get; set; }

        // С фото
        public bool? HasPhoto { get; set; }

        // Список групп
        public GroupsSearchInfo GroupsInfo { get; set; }

        public int FriendMin { get; set; }
        public int FriendMax { get; set; }

        public int SubsMin { get; set; }
        public int SubsMax { get; set; }

        public int PostMin { get; set; }
        public int PostMax { get; set; }
        public int Offcet { get; set; }
    }

    [Flags()]
    public enum FamilyState
    {
        [Display(Name = "не женат (не замужем)")]
        NotMarry = 1,
        [Display(Name = "встречается")]
        Dating = 2,
        [Display(Name = "помолвлен(-а)")]
        Betrothed = 3,
        [Display(Name = "женат (замужем)")]
        Marry = 4,
        [Display(Name = "всё сложно")]
        AllHardShit = 5,
        [Display(Name = "в активном поиске")]
        ActiveSearch = 6,
        [Display(Name = "влюблен(-а)")]
        Loved = 7,
        [Display(Name = "в гражданском браке")]
        CivilMarry = 8,
    }

    public class GroupsSearchInfo
    {
        public List<GroupInfo> GroupUrls;
    }

    public class GroupInfo
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public int GroupId { get; set; }
    }

    public enum SexEnum
    {
        Woman = 1,
        Man = 2,
        Any = 0
    }

    public enum ProfileSort
    {
        date = 1,
        popular = 0
    }

    public class IntInterval<TValue>
    {
        public IntInterval(TValue min, TValue max)
        {
            MinYear = min;
            MaxYear = max;
        }

        public TValue MinYear { get; set; }
        public TValue MaxYear { get; set; }
    }
}
