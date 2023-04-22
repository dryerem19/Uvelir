using System.Windows;
using System.Windows.Controls;
using Uvelir.Entities;

namespace Uvelir.Infractructure
{
	public static class Helper
	{
        public static AppDbContext Context { get; set; }

		public static Frame NavigationFrame { get; set; }
		public static User User { get; set; }

		public static void ShowError(string message)
		{
			MessageBox.Show(message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
		}
    }
}
