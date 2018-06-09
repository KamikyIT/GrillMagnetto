using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Chat.Gui;
using KamikyForms.Bot;
using MahApps.Metro.Controls;
using VkNet.Examples.ForChat;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using KamikyForms.WcfContractManager;
using Microsoft.Win32;

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            string version = getVersion();
            verText.Content = version;
            //List<String> bans = FileParser.getAnswer();

        }

        public string getVersion()
        {
            string version = "";
            DirectoryInfo dir = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
            foreach (var item in dir.GetFiles())
            {
                if (item.Extension == ".exe")
                {
                    version = "build: " + item.LastWriteTime.ToString("yyyy-MM-dd");
                    return version;
                }
            }
            return version;

        }
    }

	public class LoginWindowViewModel : ViewModelNotifyPropertyChanged
	{
		public LoginWindowViewModel()
		{
			_useServer = false;
			_serverAddress = @"http://localhost:4000/IVkContract";

			ReadFromRegistry();

			LoginCommand = new RelayCommand(LoginExecute);
		}

		private bool _useServer;
		private string _serverAddress;
		private string _login;
		private string _password;
		private string _errorText;

		public bool UseServer
		{
			get
			{
				return _useServer;
			}
			set
			{
				_useServer = value;

				StaticVkContractManager.UseServer = _useServer;

				OnPropertyChanged("UseServer");
			}
		}

		public string ServerAddress
		{
			get
			{
				return _serverAddress;
			}
			set
			{
				if (_serverAddress == value)
					return;

				_serverAddress = value;
				OnPropertyChanged("ServerAddress");
			}
		}

		public string Login
		{
			get
			{ return _login; }
			set
			{
				if (_login == value)
					return;

				_login = value;
				OnPropertyChanged();
			}
		}

		public string Password
		{
			get { return _password; }
			set
			{
				if (_password == value)
					return;

				_password = value;
				OnPropertyChanged();

			}
		}

		public string ErrorText
		{
			get { return _errorText; }
			set
			{
				if (_errorText == value)
					return;

				_errorText = value;
				OnPropertyChanged();
			}
		}
		
		public RelayCommand LoginCommand { get; set; }

		public void LoginExecute(object obj)
		{
			var window = obj as Window;

			long userId;

			string res = LoginCoreHelper.Login(Login, Password, out userId);
			if (!String.IsNullOrEmpty(res))
				ErrorText = res;
			else
			{
				var chatWindow = new ChatWindow();
				chatWindow.Show();
				window.Close();
				chatWindow.Show();
			}

			SaveToRegistry();

			if (!UseServer)
				return;

			var client = KamikyForms.WcfContractManager.StaticVkContractManager.GetVkContractInstance();

			var exc = new Exception();

			client.CreateNewLogin(Login, Password, "vk.com/" + userId.ToString(), exc);
		}

		private void ReadFromRegistry()
		{
			var key = Registry.CurrentUser.CreateSubKey("Software\\Borat2", true);

			Login = (string)key.GetValue("Login");

			Password = (string)key.GetValue("Password");

			UseServer = key.GetValue("UseServer") == null ? false : bool.Parse(key.GetValue("UseServer").ToString());

			key.Close();
		}

		private void SaveToRegistry()
		{
			var key = Registry.CurrentUser.CreateSubKey("Software\\Borat2", true);

			key.SetValue("Login", Login);

			key.SetValue("Password", Password);

			key.SetValue("UseServer", UseServer);

			key.Close();
		}
	}

	public abstract class ViewModelNotifyPropertyChanged : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
