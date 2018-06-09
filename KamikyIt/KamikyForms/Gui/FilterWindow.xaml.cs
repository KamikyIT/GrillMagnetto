using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ApiWrapper.Core;
using MahApps.Metro.Controls;

using KamikyForms.WcfContractManager;
using ContractInterfaces;


namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : MetroWindow
    {

        public SearchFilter filter;
        public List<PersonModel> persons = new List<PersonModel>();
        public List<PersonModel> choosenpersons = new List<PersonModel>();

		public List<FilterModel> AllFilters { get; set; }

		public FilterModel _currentFilter;

		public FilterModel CurrentFilter
		{
			get { return _currentFilter; }
			set
			{
				if (_currentFilter == value)
					return;

				_currentFilter = value;

				DisplayCurrentFilter();
			}
		}

		private void DisplayCurrentFilter()
		{
			cgender.IsChecked = CurrentFilter.Sex.HasValue;
			cminage.IsChecked = CurrentFilter.Years != null;
			cmaxage.IsChecked = CurrentFilter.Years != null;
			ccoountry.IsChecked = !string.IsNullOrEmpty(CurrentFilter.Coutry);
			ccity.IsChecked = !string.IsNullOrEmpty(CurrentFilter.City);
			cfamily.IsChecked = CurrentFilter.FamilyStatus.HasValue;
			conline.IsChecked = CurrentFilter.IsOnline.HasValue;
			cfoto.IsChecked = CurrentFilter.HasPhoto.HasValue;
			cfriendmin.IsChecked = CurrentFilter.FriendsCount != null;
			cfriendmax.IsChecked = CurrentFilter.FriendsCount != null;
			csubsmin.IsChecked = CurrentFilter.SubsCount != null;
			csubsmax.IsChecked = CurrentFilter.SubsCount != null;
			cpostmin.IsChecked = CurrentFilter.PostCount != null;
			cpostmax.IsChecked = CurrentFilter.PostCount != null;
			ssort.IsChecked = CurrentFilter.SortBy != SearchSortBy.None;
			coffcet.IsChecked = CurrentFilter.Offset == 0;

			if (CurrentFilter.Sex.HasValue)
				switch (CurrentFilter.Sex.Value)
				{
					case Sex.Unknown:
						gender.SelectedIndex = 2;
						break;
					case Sex.Female:
						gender.SelectedIndex = 0;
						break;
					case Sex.Male:
						gender.SelectedIndex = 1;
						break;
					default:
						break;
				}

			country.SelectedIndex = 0;
			city.SelectedIndex = 0;

			family.SelectedIndex = 0;

			if (CurrentFilter.FamilyStatus.HasValue)
			{
				switch (CurrentFilter.FamilyStatus.Value)
				{
					case MyFamilyStatus.Single:
						family.SelectedIndex = 0;
						break;
					case MyFamilyStatus.Meets:
						family.SelectedIndex = 1;
						break;
					case MyFamilyStatus.Engaged:
						family.SelectedIndex = 2;
						break;
					case MyFamilyStatus.Married:
						family.SelectedIndex = 3;
						break;
					case MyFamilyStatus.ItsComplicated:
						family.SelectedIndex = 4;
						break;
					case MyFamilyStatus.TheActiveSearch:
						family.SelectedIndex = 5;
						break;
					case MyFamilyStatus.InLove:
						family.SelectedIndex = 6;
						break;
					default:
						break;
				}
			}

			online.SelectedIndex = CurrentFilter.IsOnline.HasValue? (CurrentFilter.IsOnline.Value ? 1 : 0) : 0;
			foto.SelectedIndex = CurrentFilter.HasPhoto.HasValue ? (CurrentFilter.HasPhoto.HasValue ? 1 : 0) : 0;

			sort.SelectedIndex = (int)CurrentFilter.SortBy - 1;
			minage.Text = CurrentFilter.Years != null? CurrentFilter.Years.Min.ToString() : "0";
			maxage.Text = CurrentFilter.Years != null ? CurrentFilter.Years.Max.ToString() : "0";
			friendmin.Text = CurrentFilter.FriendsCount != null? CurrentFilter.FriendsCount.Min.ToString() : "0";
			friendmax.Text = CurrentFilter.FriendsCount != null ? CurrentFilter.FriendsCount.Max.ToString() : "0";
			subsmin.Text = CurrentFilter.SubsCount != null ? CurrentFilter.SubsCount.Min.ToString() : "0";
			subsmax.Text = CurrentFilter.SubsCount != null ? CurrentFilter.SubsCount.Max.ToString() : "0";
			postmin.Text = CurrentFilter.PostCount != null ? CurrentFilter.PostCount.Min.ToString() : "0";
			postmax.Text = CurrentFilter.PostCount != null ? CurrentFilter.PostCount.Max.ToString() : "0";
			offcet.Text = CurrentFilter.Offset.ToString();
		}

		public FilterWindow()
        {
            InitializeComponent();

			CustomInitialize();
        }

		private void CustomInitialize()
		{
			AllFilters = StaticVkContractManager.GetVkContractInstance().GetAllSearchFilters();

			CurrentFilter = AllFilters.Any() ? AllFilters.First().CopyFilter() : new FilterModel();
		}


		private void onTest(object sender, RoutedEventArgs e)
        {
            var peoples = SearchInstrument.getPersons(filter);
            MessageBox.Show(peoples[0].ToString());

            return;

        }


        public void getPostMin()
        {
            if (cpostmin.IsChecked == false) return;
            filter.PostMin = Convert.ToInt32(postmin.Text);
        }

        public void getPostMax()
        {
            if (cpostmax.IsChecked == false) return;
            filter.PostMax = Convert.ToInt32(postmax.Text);
        }


        public void getSubsMin()
        {
            if (csubsmin.IsChecked == false) return;
            filter.SubsMin = Convert.ToInt32(subsmin.Text);
        }

        public void getSubsMax()
        {
            if (csubsmax.IsChecked == false) return;
            filter.SubsMax = Convert.ToInt32(subsmax.Text);
        }


        public void getFriendMin()
        {
            if (cfriendmin.IsChecked == false) return;
            filter.FriendMin = Convert.ToInt32(friendmin.Text);
        }

        public void getFriendMax()
        {
            if (cfriendmax.IsChecked == false) return;
            filter.FriendMax = Convert.ToInt32(friendmax.Text);
        }

        public void getMinAge()
        {
            if (cminage.IsChecked == false) return;
            filter.MinAge = Convert.ToInt32(minage.Text);

        }


        public void getMaxAge()
        {
            if (cmaxage.IsChecked == false) return;
            filter.MaxAge = Convert.ToInt32(maxage.Text);

        }

        public void getCountry()
        {
            if (ccoountry.IsChecked == false) return;
            if (country.SelectedIndex == 0)
            {
                filter.CountryId = 1;
            }
        }

        public void getCity()
        {
            if (ccity.IsChecked == false) return;
            if (city.SelectedIndex == 0)
            {
                filter.CityId = 157;
            }
        }

        public void getPhoto()
        {
            if (cfoto.IsChecked == false) return;
            if (foto.SelectedIndex == 0)
            {
                filter.HasPhoto = true;
            }
            else
            {
                filter.HasPhoto = false;
            }
        }

        public void getOnline()
        {
            if (conline.IsChecked == false) return;
            if (online.SelectedIndex == 0)
            {
                filter.IsOnline = true;
            }
            else
            {
                filter.IsOnline = false;

            }
        }

        public void getFamily()
        {
            if (cfamily.IsChecked == false) return;
            filter.FamilyState = (FamilyState)ConvertBack(family);
        }

        public void getSex()
        {
            if (cgender.IsChecked == false) return;
            if (gender.SelectedIndex == 0)
            {
                filter.Sex = SexEnum.Woman;
            }
            if (gender.SelectedIndex == 1)
            {
                filter.Sex = SexEnum.Man;
            }
            if (gender.SelectedIndex == 2)
            {
                filter.Sex = SexEnum.Any;
            }
        }

        public void getSort()
        {
            if (ssort.IsChecked == false) return;
            if (sort.SelectedIndex == 0)
            {
                filter.profileSort = ProfileSort.date;
            }
            if (gender.SelectedIndex == 1)
            {
                filter.profileSort = ProfileSort.popular;

            }

        }

        public void getOffcet()
        {
            if (coffcet.IsChecked == false) return;
            filter.Offcet = Convert.ToInt32(offcet.Text);

        }

        private void oncheckChanged(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            String tag = ch.Tag.ToString();
            if (tag == "gender")
            {
                gender.IsEnabled = ch.IsChecked == true;
            }
            if (tag == "minage")
            {
                minage.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "maxage")
            {
                maxage.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "country")
            {
                country.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "city")
            {
                city.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "family")
            {
                family.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "online")
            {
                online.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "friendmin")
            {
                friendmin.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "friendmax")
            {
                friendmax.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "subsmin")
            {
                subsmin.IsEnabled = ch.IsChecked == true;
            }


            if (tag == "subsmax")
            {
                subsmax.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "postmin")
            {
                postmin.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "postmax")
            {
                postmax.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "sort")
            {
                sort.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "offcet")
            {
                offcet.IsEnabled = ch.IsChecked == true;
            }

        }


        public object ConvertBack(object value)
        {
            var str = (value as ComboBox).SelectionBoxItem.ToString();

            switch (str)
            {
                case "не женат (не замужем)":
                    return FamilyState.NotMarry;
                case "встречается":
                    return FamilyState.Dating;
                case "помолвлен(-а)":
                    return FamilyState.Betrothed;
                case "женат (замужем)":
                    return FamilyState.Marry;
                case "всё сложно<":
                    return FamilyState.AllHardShit;
                case "в активном поиске":
                    return FamilyState.ActiveSearch;
                case "влюблен(-а)":
                    return FamilyState.Loved;
                case "в гражданском браке":
                    return FamilyState.CivilMarry;
                default:
                    throw new Exception("Невозможно распарсить");
            }
        }

        private void onSearch(object sender, RoutedEventArgs e)
        {
            filter = new SearchFilter();
            getSex();
            getMinAge();
            getMaxAge();
            getCountry();
            getCity();
            getFamily();
            getOnline();
            getFriendMin();
            getFriendMax();
            getSubsMin();
            getSubsMax();
            getPostMin();
            getPostMax();
            getOffcet();
            getSort();
            List<PersonModel> peoples = SearchInstrument.getPersons(filter);
            persons = peoples;
            UpdateUi();

        }

        private void onSelect(object sender, RoutedEventArgs e)
        {
            PreparePersonsForm form = new PreparePersonsForm(persons);
            var res = form.ShowDialog();
            if (res == true)
            {
                choosenpersons = form.pl2;
                UpdateUi();
            }
        }

        public void UpdateUi()
        {
            finded.Content = "Найдено : " + persons.Count;
            chosen.Content = "Выбрано : " + choosenpersons.Count;
        }

        private void onApply(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
	}

	public class FilterWindowViewModel : ViewModelNotifyPropertyChanged
	{
		private FilterModel _currentFilter;

		public FilterModel CurrentFilter
		{
			get { return _currentFilter; }
			set
			{
				if (_currentFilter == value)
					return;

				_currentFilter = value;

				OnPropertyChanged();
			}
		}

		public ObservableCollection<FilterModel> AllFilters { get; set; }

		public bool UseServer { get; set; }

		public ICommand ApplyCommand { get; set; }

		public FilterWindowViewModel()
		{
			UseServer = StaticVkContractManager.UseServer;

			ApplyCommand = new ContractInterfaces.RelayCommand(ApplyCommandExecute);

			if (UseServer)
			{
				var contract = StaticVkContractManager.GetVkContractInstance();

				AllFilters = new ObservableCollection<FilterModel>(contract.GetAllSearchFilters());

				AllFilters.Add(new FilterModel()
				{
					Name = "Новый фильтр"
				});


				CurrentFilter = AllFilters.First();
			}
			else
			{
				CurrentFilter = new FilterModel();

				AllFilters = new ObservableCollection<FilterModel>();
			}
		}

		private void ApplyCommandExecute(object obj)
		{
			if (UseServer)
			{
				var contract = StaticVkContractManager.GetVkContractInstance();

				contract.AddSearchFilter(CurrentFilter);
			}
		}
	}
}
