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


		public List<PersonModel> persons = new List<PersonModel>();
		public List<PersonModel> choosenpersons = new List<PersonModel>();

		private void onSearch(object sender, RoutedEventArgs e)
		{
			return;

			//var filter = new SearchFilter();
			//List<PersonModel> peoples = SearchInstrument.getPersons(filter);
			//persons = peoples;
			//UpdateUi();
		}

		private void onSelect(object sender, RoutedEventArgs e)
		{
			return;

			//PreparePersonsForm form = new PreparePersonsForm(persons);
			//var res = form.ShowDialog();
			//if (res == true)
			//{
			//	choosenpersons = form.pl2;
			//	UpdateUi();
			//}
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

		private bool _hasSex;
		private bool _hasYears;
		private bool _hasCountry;
		private bool _hasCity;
		private bool _hasFamilyStatus;
		private bool _hasSort;
		private bool _hasFriendsCount;
		private bool _hasSubsCount;
		private bool _hasOffset;
		private string _foundPeoplesString;
		private string _chosenPeoplesString;
		private List<PersonModel> _foundPeoples;
		private List<PersonModel> _chosenPeoples;

		public FilterModel CurrentFilter
		{
			get { return _currentFilter; }
			set
			{
				if (_currentFilter == value)
					return;

				_currentFilter = value;

				OnPropertyChanged();

				HasSex = _currentFilter.Sex.HasValue;
				OnPropertyChanged("Sex");

				OnPropertyChanged("FamilyStatus");

				HasYears = _currentFilter.Years != null;
				OnPropertyChanged("YearsMin");
				OnPropertyChanged("YearsMax");

				HasCountry = !string.IsNullOrEmpty(_currentFilter.Coutry);
				HasCity = !string.IsNullOrEmpty(_currentFilter.City);

				HasFamilyStatus = _currentFilter.FamilyStatus.HasValue;

				OnPropertyChanged("IsOnline");
				OnPropertyChanged("HasPhoto");
				OnPropertyChanged("HasSort");

				HasFriendsCount = _currentFilter.FriendsCount != null;
				OnPropertyChanged("FriendsCountMin");
				OnPropertyChanged("FriendsCountMax");

				OnPropertyChanged("CommonFriendsCount");

				HasSubsCount = _currentFilter.SubsCount != null;
				OnPropertyChanged("SubsCountMin");
				OnPropertyChanged("SubsCountMax");

				HasOffset = _currentFilter.Offset == 0;
				OnPropertyChanged("Offset");
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

		public ICommand SearchCommand { get; set; }

		public ICommand ChooseCommand { get; set; }

		public string FoundPeoplesString
		{
			get { return _foundPeoplesString; }
			set
			{
				if (_foundPeoplesString == value)
					return;

				_foundPeoplesString = value;

				OnPropertyChanged();
			}
		}

		public string ChosenPeoplesString
		{
			get { return _chosenPeoplesString; }
			set
			{
				if (_chosenPeoplesString == value)
					return;

				_chosenPeoplesString = value;

				OnPropertyChanged();
			}
		}


		#region MVVM CurrentFilter

		public bool HasSex
		{
			get { return _hasSex; }
			set
			{
				if (_hasSex == value)
					return;

				_hasSex = value;

				OnPropertyChanged();
			}
		}

		public Sex Sex
		{
			get { return CurrentFilter.Sex.Value; }
			set
			{
				if (CurrentFilter.Sex == value)
					return;

				CurrentFilter.Sex = value;
			}
		}

		public bool HasYears
		{
			get { return _hasYears; }
			set
			{
				if (_hasYears == value)
					return;

				_hasYears = value;
				OnPropertyChanged();
			}
		}

		public int YearsMin
		{
			get { return CurrentFilter.Years.Min; }
			set
			{
				if (CurrentFilter.Years.Min == value)
					return;

				CurrentFilter.Years.Min = value;
				OnPropertyChanged();
			}
		}

		public int YearsMax
		{
			get { return CurrentFilter.Years.Max; }
			set
			{
				if (CurrentFilter.Years.Max == value)
					return;

				CurrentFilter.Years.Max = value;
				OnPropertyChanged();
			}
		}

		public bool HasCountry
		{
			get { return _hasCountry; }
			set
			{
				if (_hasCountry == value)
					return;

				_hasCountry = value;
				OnPropertyChanged();
			}
		}

		public bool HasCity
		{
			get { return _hasCity; }
			set
			{
				if (_hasCity == value)
					return;

				_hasCity = value;
				OnPropertyChanged();
			}
		}

		public bool HasFamilyStatus
		{
			get { return _hasFamilyStatus; }
			set
			{
				if (_hasFamilyStatus == value)
					return;

				_hasFamilyStatus = value;
				OnPropertyChanged();
			}
		}

		public MyFamilyStatus FamilyStatus
		{
			get { return CurrentFilter.FamilyStatus.Value; }
			set
			{
				if (CurrentFilter.FamilyStatus.Value == value)
					return;

				CurrentFilter.FamilyStatus = value;
				OnPropertyChanged();
			}
		}

		public bool IsOnline
		{
			get { return CurrentFilter.IsOnline.Value; }
			set
			{
				if (CurrentFilter.IsOnline.Value == value)
					return;

				CurrentFilter.IsOnline = value;

				OnPropertyChanged();
			}
		}

		public bool HasPhoto
		{
			get { return CurrentFilter.HasPhoto.Value; }
			set
			{
				if (CurrentFilter.HasPhoto.Value == value)
					return;

				CurrentFilter.HasPhoto = value;

				OnPropertyChanged();
			}
		}

		public bool HasSort
		{
			get { return _hasSort; }
			set
			{
				if (_hasSort == value)
					return;

				_hasSort = value;

				OnPropertyChanged();
			}
		}

		public SearchSortBy Sort
		{
			get { return CurrentFilter.SortBy; }
			set
			{
				if (CurrentFilter.SortBy == value)
					return;

				CurrentFilter.SortBy = value;

				OnPropertyChanged();
			}
		}
		
		public bool HasFriendsCount
		{
			get { return _hasFriendsCount; }
			set
			{
				if (_hasFriendsCount == value)
					return;

				_hasFriendsCount = value;

				OnPropertyChanged();
			}
		}

		public int FriendsCountMin
		{
			get { return CurrentFilter.FriendsCount.Min; }
			set
			{
				if (CurrentFilter.FriendsCount.Min == value)
					return;

				CurrentFilter.FriendsCount.Min = value;

				OnPropertyChanged();
			}
		}

		public int FriendsCountMax
		{
			get { return CurrentFilter.FriendsCount.Max; }
			set
			{
				if (CurrentFilter.FriendsCount.Max == value)
					return;

				CurrentFilter.FriendsCount.Max = value;

				OnPropertyChanged();
			}
		}

		public int CommonFriendsCount
		{
			get { return CurrentFilter.CommonFriends; }
			set
			{
				if (CurrentFilter.CommonFriends == value)
					return;

				CurrentFilter.CommonFriends = value;

				OnPropertyChanged();
			}
		}
		
		public bool HasSubsCount
		{
			get { return _hasSubsCount; }
			set
			{
				if (_hasSubsCount == value)
					return;

				_hasSubsCount = value;

				OnPropertyChanged();
			}
		}

		public int SubsCountMin
		{
			get { return CurrentFilter.SubsCount.Min; }
			set
			{
				if (CurrentFilter.SubsCount.Min == value)
					return;

				CurrentFilter.SubsCount.Min = value;

				OnPropertyChanged();
			}
		}

		public int SubsCountMax
		{
			get { return CurrentFilter.SubsCount.Max; }
			set
			{
				if (CurrentFilter.SubsCount.Max == value)
					return;

				CurrentFilter.SubsCount.Max = value;

				OnPropertyChanged();
			}
		}
		

		public bool HasOffset
		{
			get { return _hasOffset; }
			set
			{
				if (_hasOffset == value)
					return;

				_hasOffset = value;

				OnPropertyChanged();
			}
		}

		public int Offset
		{
			get { return CurrentFilter.Offset; }

			set
			{
				if (CurrentFilter.Offset == value)
					return;

				CurrentFilter.Offset = value;

				OnPropertyChanged();
			}
		}

		#endregion

		public FilterWindowViewModel()
		{
			ApplyCommand = new RelayCommand(ApplyCommandExecute);

			SearchCommand = new RelayCommand(SearchCommandExecute);

			ChooseCommand = new RelayCommand(ChooseCommandExecute, (obj) => _foundPeoples != null && _foundPeoples.Any());

			UseServer = StaticVkContractManager.UseServer;

			if (UseServer)
			{
				var contract = StaticVkContractManager.GetVkContractInstance();

				var allFilters = contract.GetAllSearchFilters();

				FixAllFilters(allFilters);

				AllFilters = new ObservableCollection<FilterModel>(allFilters);

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

		private void FixAllFilters(List<FilterModel> allFilters)
		{
			foreach (var filter in allFilters)
			{
				filter.HasPhoto = filter.HasPhoto.HasValue ? filter.HasPhoto.Value : false;

				filter.IsOnline = filter.IsOnline.HasValue ? filter.IsOnline.Value : false;

				filter.FamilyStatus = filter.FamilyStatus.HasValue ? filter.FamilyStatus.Value : MyFamilyStatus.Single;

				filter.Sex = filter.Sex.HasValue ? filter.Sex.Value : Sex.Unknown;

				filter.Years = filter.Years != null ? filter.Years : new IntervalValue<int>();

				filter.FriendStatus = filter.FriendStatus.HasValue ? filter.FriendStatus.Value : FriendStatus.NotFriend;

				filter.FriendsCount = filter.FriendsCount != null ? filter.FriendsCount : new IntervalValue<int>();

				filter.SubsCount = filter.SubsCount != null ? filter.SubsCount : new IntervalValue<int>();
			}
		}

		private void ApplyCommandExecute(object obj)
		{
			if (UseServer)
			{
				var contract = StaticVkContractManager.GetVkContractInstance();

				var toSendFilter = CurrentFilter.CopyFilter();

				if (!HasYears)
					toSendFilter.Years = null;

				if (!HasSex)
					toSendFilter.Sex = default(Sex?);

				if (!HasSubsCount)
					toSendFilter.SubsCount = null;

				if (!HasFamilyStatus)
					toSendFilter.FamilyStatus = default(MyFamilyStatus?);

				if (!HasFriendsCount)
					toSendFilter.FriendsCount = null;

				if (toSendFilter.Name == NewFilterNameConst)
					contract.AddSearchFilter(toSendFilter, NewFilterName);
				else
					contract.UpdateSearchFilter(toSendFilter);
			}
		}

		private void SearchCommandExecute(object obj)
		{
			var toSendFilter = CurrentFilter.CopyFilter();

			if (!HasYears)
				toSendFilter.Years = null;

			if (!HasSex)
				toSendFilter.Sex = default(Sex?);

			if (!HasSubsCount)
				toSendFilter.SubsCount = null;

			if (!HasFamilyStatus)
				toSendFilter.FamilyStatus = default(MyFamilyStatus?);

			if (!HasFriendsCount)
				toSendFilter.FriendsCount = null;

			_foundPeoples = SearchInstrument.GetPersons(toSendFilter);

			FoundPeoplesString = string.Format("Найдено : {0}", _foundPeoples != null ? _foundPeoples.Count : 0);

			ChosenPeoplesString = string.Format("Выбрано : {0}", 0);
		}

		private void ChooseCommandExecute(object obj)
		{
			var form = new PreparePersonsForm(_foundPeoples);

			if (form.ShowDialog() == false)
				return;

			_chosenPeoples = form.pl2;

			ChosenPeoplesString = string.Format("Выбрано : {0}", _chosenPeoples.Count);
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
