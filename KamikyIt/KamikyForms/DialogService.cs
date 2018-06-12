using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KamikyForms.Gui;

namespace KamikyForms
{
	public static class DialogService
	{
		public static KeyValuePair<bool, TViewModel> ShowDialog<TWindowClass, TViewModel>(TWindowClass window) 
			where  TWindowClass : Window
			where TViewModel : ViewModelNotifyPropertyChanged
		{
			window.ShowDialog();

			var dialogViewModel = window.DataContext as IDialogViewModel;

			return new KeyValuePair<bool, TViewModel>(dialogViewModel != null && dialogViewModel.DialogResult, (TViewModel) window.DataContext);
		}
	}

	public interface IDialogViewModel
	{
		bool DialogResult { get; set; }
	}
}
