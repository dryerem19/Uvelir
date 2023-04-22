using System.Windows;
using Uvelir.Infractructure;
using Uvelir.View;

namespace Uvelir
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Helper.NavigationFrame = NavigationFrame;
			Helper.NavigationFrame.Navigate(new LoginPage());
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (Helper.NavigationFrame.CanGoBack)
			{
				Helper.NavigationFrame.GoBack();
			}
			else
			{
				Application.Current.Shutdown();
			}
        }

		private void NavigationFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
			if (Helper.NavigationFrame.CanGoBack) HeaderButton.Content = "Назад";
			else HeaderButton.Content = "Выход";
		}
	}
}
