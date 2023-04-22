using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Uvelir.Infractructure;

namespace Uvelir
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			try
			{
				Helper.Context = new Entities.AppDbContext();
			}
			catch (Exception ex)
			{
				Helper.ShowError(ex.Message);
				Current.Shutdown();
			}
		}
	}
}
