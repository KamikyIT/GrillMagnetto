using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ContractInterfaces;
using VkWcfServer.Annotations;

namespace VkWcfServer
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
	}

	public class MainWindowViewModel : INotifyPropertyChanged
	{
		public ICommand StartServerCommand { get; set; }
		public ICommand StopServerCommand { get; set; }
		public ICommand ClearServerLogCommand { get; set; }

		public string LogText
		{
			get { return _logText; }
			set
			{
				if (_logText == value)
					return;

				_logText = value;
				OnPropertyChanged("LogText");
			}
		}

		private bool _serverWorking;

		private ServiceHost _host;
		private string _logText;

		public MainWindowViewModel()
		{
			StartServerCommand = new RelayCommand(StartServerExecute, StartServerCanExecute);
			StopServerCommand = new RelayCommand(StopServerExecute, StopServerCanExecute);
			ClearServerLogCommand = new RelayCommand(ClearServerLogExecute);

			StaticLog.LogEvent += StaticLogOnLogEvent;
		}

		private void StaticLogOnLogEvent(string s)
		{
			LogText += DateTime.Now.ToString("T") + " : " + s + Environment.NewLine;
		}


		private void Log(string message)
		{
			LogText += DateTime.Now.ToString("T") + " : " + message + Environment.NewLine;
		}

		#region Commands

		private bool StopServerCanExecute(object arg)
		{
			return _serverWorking;
		}

		private bool StartServerCanExecute(object arg)
		{
			return _serverWorking == false;
		}

		private void StopServerExecute(object obj)
		{
			try
			{
				_host.Close();

				_serverWorking = false;

				StaticLogOnLogEvent("Server STOP.");
			}
			catch (Exception e)
			{
				LogText += e.Message;
				_serverWorking = false;
			}

		}

		private void StartServerExecute(object obj)
		{
			try
			{
				_host = new ServiceHost(typeof(VkService));
				_host.AddServiceEndpoint(typeof(IVkContract), new BasicHttpBinding(), new Uri("http://localhost:4000/IVkContract"));

				_host.Open();

				_serverWorking = true;

				StaticLogOnLogEvent("Server START.");
			}
			catch (Exception e)
			{
				LogText += e.Message;
				_serverWorking = false;
			}
		}

		private void ClearServerLogExecute(object obj)
		{
			LogText = "";
		}

		#endregion

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
