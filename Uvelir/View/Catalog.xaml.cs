using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Uvelir.Entities;
using Uvelir.Infractructure;
using Path = System.IO.Path;

namespace Uvelir.View
{
	/// <summary>
	/// Interaction logic for Catalog.xaml
	/// </summary>
	public partial class Catalog : Page
	{
		public Catalog()
		{
			InitializeComponent();
			//FillDb();
			ComboDiscount.SelectedIndex = 0;


			if (Helper.User != null)
			{
				UsernameData.Text = Helper.User.Fullname;
				UsernameRoleName.Text = Helper.User.Role.Name;

				if (UsernameRoleName.Text is "Администратор")
				{
					ProductListView.MouseDoubleClick += ProductListView_MouseDoubleClick;
					BottomButton.Click += OnEditProductButtonClick;
					BottomButton.Content = "Добавить товар";
				}
			}
			else
			{
				BottomButton.Click += OnCartButtonClick;
				BottomButton.Content = "Перейти к корзине";
			}

			try
			{
				var items = Helper.Context.Manufacturer.ToList();
				items.Insert(0, new Manufacturer { Name = "Все  производители" });
				ComboManufacturer.ItemsSource = items;
				ComboManufacturer.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				Helper.ShowError(ex.Message);
			}

			Update();
		}

		private void Update()
		{
			try
			{
				var products = Helper.Context.Product.ToList();

				TotalCount.Text = products.Count.ToString();
				SortBySearch(ref products);
				SortByManufacturer(ref products);
				SortByDiscount(ref products);
				CurrentCount.Text = products.Count.ToString();

				ProductListView.ItemsSource = products;
			}
			catch (Exception ex)
			{
				Helper.ShowError(ex.Message);
			}
		}

		private void SortBySearch(ref List<Product> products)
		{
			if (string.IsNullOrEmpty(Search.Text))
			{
				return;
			}

			var search = Search.Text.ToLower().Trim();
			products = products.Where(x =>
			{
				var article = x.Article.ToLower().Trim();
				var name = x.Name.ToLower().Trim();
				var description = x.Description.ToLower().Trim();

				bool a = article.Contains(search);
				bool n = name.Contains(search);
				bool d = description.Contains(search);

				return a || n || d;

			}).ToList();
		}

		private void SortByManufacturer(ref List<Product> products)
		{
			var selected = ComboManufacturer.SelectedItem as Manufacturer;

			if (selected != null)
			{
				if (selected.Name == "Все  производители")
				{
					return;
				}

				products = products.Where(x => x.Manufacturer.Name == selected.Name).ToList();
			}
		}

		private void SortByDiscount(ref List<Product> products)
		{
			var selected = ComboDiscount.SelectedIndex;
			if (selected == 0)
			{
				products = products.OrderBy(x => x.Discount).ToList();
			}
			else
			{
				products = products.OrderByDescending(x => x.Discount).ToList();
			}
		}

		private void ComboDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Update();
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			Update();
		}

		private void ComboManufacturer_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Update();
		}

		private void FillDb()
		{
			foreach (var file in Directory.GetFiles(@"C:\Users\dryerem19\Desktop\Задание на ДЭ_09_1.1-2022_10\Вариант 10\Вариант 10\Сессия 1\Товар_import"))
			{
				var filename = Path.GetFileNameWithoutExtension(file);
				foreach (var product in Helper.Context.Product.ToList())
				{
					if (product.Article == filename)
					{
						product.Image = File.ReadAllBytes(file);
					}
				}
			}

			Helper.Context.SaveChanges();
		}

		private void OnCartButtonClick(object sender, RoutedEventArgs e)
		{
			Helper.NavigationFrame.Navigate(new CartView());
		}

		private void OnEditProductButtonClick(object sender, RoutedEventArgs e)
		{
			Helper.NavigationFrame.Navigate(new EditProduct(null));
		}

		private void ProductListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (ProductListView.SelectedItem != null)
			{
				Helper.NavigationFrame.Navigate(new EditProduct(ProductListView.SelectedItem as Product));
			}
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			Update();
		}
	}
}
