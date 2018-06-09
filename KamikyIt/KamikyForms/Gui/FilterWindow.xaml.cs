using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ApiWrapper.Core;
using MahApps.Metro.Controls;

using KamikyForms.WcfContractManager;
using ContractInterfaces;
using KamikyForms.Gui;


namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : MetroWindow
    {
		public FilterWindow()
		{
			InitializeComponent();

			CustomInitialize();
		}

		/// <summary>
		/// Почему-то выдает ошибку, потом как-нибудь.
		/// </summary>
		private void SetNumberedTextBoxValidationRules()
		{
			foreach (var tb in XamlExtensionHelper.FindLogicalChildren<TextBox>(this))
			{
				if (XamlExtensionHelper.CheckStyleName(this.Resources, tb.Style, "ttextboxNumber"))
				{
					var bind = new Binding();

					bind.ValidationRules.Add(new TextBoxNumbersValidationRule());

					bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
					bind.ValidatesOnDataErrors = true;
					bind.NotifyOnValidationError = true;

					tb.SetBinding(TextBox.TextProperty, bind);
				}
			}
		}


		private void CustomInitialize()
		{
			if (StaticVkContractManager.UseServer)
				AllFilters = StaticVkContractManager.GetVkContractInstance().GetAllSearchFilters();

			CurrentFilter = AllFilters != null && AllFilters.Any() ? AllFilters.First().CopyFilter() : new FilterModel();
		}

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

			conline.IsChecked = CurrentFilter.IsOnline;
			cfoto.IsChecked = CurrentFilter.HasPhoto;

			sort.SelectedIndex = (int)CurrentFilter.SortBy - 1;
			minage.Text = CurrentFilter.Years != null ? CurrentFilter.Years.Min.ToString() : "0";
			maxage.Text = CurrentFilter.Years != null ? CurrentFilter.Years.Max.ToString() : "0";
			friendmin.Text = CurrentFilter.FriendsCount != null ? CurrentFilter.FriendsCount.Min.ToString() : "0";
			friendmax.Text = CurrentFilter.FriendsCount != null ? CurrentFilter.FriendsCount.Max.ToString() : "0";
			subsmin.Text = CurrentFilter.SubsCount != null ? CurrentFilter.SubsCount.Min.ToString() : "0";
			subsmax.Text = CurrentFilter.SubsCount != null ? CurrentFilter.SubsCount.Max.ToString() : "0";
			postmin.Text = CurrentFilter.PostCount != null ? CurrentFilter.PostCount.Min.ToString() : "0";
			postmax.Text = CurrentFilter.PostCount != null ? CurrentFilter.PostCount.Max.ToString() : "0";
			offcet.Text = CurrentFilter.Offset.ToString();
		}

		#region Set SearchFilter values

		public void getPostMin()
		{
			filter.PostMin = 0;

			if (cpostmin.IsChecked == false) return;

			var val = 0;

			if (int.TryParse(postmin.Text, out val))
				filter.PostMin = Convert.ToInt32(postmin.Text);
		}

		public void getPostMax()
		{
			filter.PostMax = 0;

			if (cpostmax.IsChecked == false) return;

			var val = 0;

			if (int.TryParse(postmax.Text, out val))
				filter.PostMax = Convert.ToInt32(postmax.Text);
		}

		public void getSubsMin()
		{
			filter.SubsMin = 0;

			if (csubsmin.IsChecked == false) return;

			var val = 0;

			if (int.TryParse(subsmin.Text, out val))
				filter.SubsMin = Convert.ToInt32(subsmin.Text);
		}

		public void getSubsMax()
		{
			filter.SubsMax = 0;

			if (csubsmax.IsChecked == false) return;

			var val = 0;

			if (int.TryParse(subsmax.Text, out val))
				filter.SubsMax = Convert.ToInt32(subsmax.Text);
		}

		public void getFriendMin()
		{
			filter.FriendMin = 0;

			if (cfriendmin.IsChecked == false) return;

			var val = 0;

			if (int.TryParse(friendmin.Text, out val))
				filter.FriendMin = Convert.ToInt32(friendmin.Text);
		}

		public void getFriendMax()
		{
			filter.FriendMax = 0;

			if (cfriendmax.IsChecked == false) return;

			var val = 0;

			if (int.TryParse(friendmax.Text, out val))
				filter.FriendMax = Convert.ToInt32(friendmax.Text);
		}

		public void getMinAge()
		{
			filter.MinAge = 0;

			if (cminage.IsChecked == false) return;

			var val = 0;

			if (int.TryParse(minage.Text, out val))
				filter.MinAge = Convert.ToInt32(minage.Text);
		}

		public void getMaxAge()
		{
			filter.MaxAge = 0;

			if (cmaxage.IsChecked == false) return;

			var val = 0;

			if (int.TryParse(maxage.Text, out val))
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
			filter.HasPhoto = cfoto.IsChecked;
		}

		public void getOnline()
		{
			filter.IsOnline = conline.IsChecked;
		}

		public void getFamily()
		{
			if (cfamily.IsChecked == false) return;
			filter.FamilyState = (FamilyState)ConvertBack(family);
		}

		public void getSex()
		{
			filter.Sex = SexEnum.Any;

			if (cgender.IsChecked == false) return;
			if (gender.SelectedIndex == 0)
			{
				filter.Sex = SexEnum.Woman;
			}
			if (gender.SelectedIndex == 1)
			{
				filter.Sex = SexEnum.Man;
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

		#endregion

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


		private void AllowNumbersTextBox(object sender, TextChangedEventArgs e)
		{
			var tb = (sender as TextBox);

			tb.Text = XamlExtensionHelper.NumbersOnlyText(tb.Text);
		}
	}

	public class FilterWindowViewModel : ViewModelNotifyPropertyChanged
	{
		private FilterModel _currentFilter;
		private string _newFilterName;

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

		public string NewFilterName
		{
			get { return _newFilterName; }
			set
			{
				if (_newFilterName == value)
					return;

				_newFilterName = value;
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

				AllFilters.Insert(0, new FilterModel()
				{
					Name = NewFilterNameConst
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

				if (CurrentFilter.Name == NewFilterNameConst)
					contract.AddSearchFilter(CurrentFilter, NewFilterName);
				else
					contract.UpdateSearchFilter(CurrentFilter);
			}
		}

		private const string NewFilterNameConst = "Новый фильтр";
	}

	public class TextBoxEnableSelectedFilterIndexConverter :IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//targetType - bool
			// value - int

			var index = (int) value;

			return index == 0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
