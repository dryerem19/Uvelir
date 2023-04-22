using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Uvelir.Infractructure;

namespace Uvelir.View
{
	/// <summary>
	/// Interaction logic for LoginPage.xaml
	/// </summary>
	public partial class LoginPage : Page
	{
		public LoginPage()
		{
			InitializeComponent();
		}

		private void OnGuestButtonClick(object sender, RoutedEventArgs e)
		{
			Helper.NavigationFrame.Navigate(new Catalog());
		}

		private void OnLoginButtonClick(object sender, RoutedEventArgs e)
		{
			var builder = new StringBuilder();

			if (string.IsNullOrEmpty(Login.Text))
			{
				builder.AppendLine("Вы не ввели логин");
			}

			if (string.IsNullOrEmpty(Password.Text))
			{
				builder.AppendLine("Вы не ввели пароль");
			}

			if (builder.Length > 0)
			{
				Helper.ShowError(builder.ToString());
				return;
			}

			try
			{
				var user = Helper.Context.User.FirstOrDefault(x => x.Login == Login.Text && x.Password == Password.Text);
				if (user != null)
				{
					Helper.User = user;
					Helper.NavigationFrame.Navigate(new Catalog());
				}
			}
			catch (Exception ex)
			{
				Helper.ShowError(ex.Message);
			}
		}
    }
}
