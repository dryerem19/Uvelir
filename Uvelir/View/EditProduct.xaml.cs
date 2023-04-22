using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Uvelir.Entities;
using Uvelir.Infractructure;

namespace Uvelir.View
{
	/// <summary>
	/// Interaction logic for EditProduct.xaml
	/// </summary>
	public partial class EditProduct : Page
	{
		private Product _product;
		bool isCreated = false;
		private byte[] _loadedImage = null;

		public EditProduct(Product product)
		{
			InitializeComponent();
			_product = product;
			DataContext = this;

			if (product != null)
			{

				if (_product.Image != null)
				{
					ProductImage.Source = ByteToBitmap(_product.Image);
				}

				Article.Text = _product.Article;
				ProductName.Text = _product.Name;
				Cost.Text = _product.Cost.ToString();
				MaxDiscount.Text = _product.MaxDiscount.ToString();
				Discount.Text = _product.Discount.ToString();
				StorageCount.Text = _product.StorageCount.ToString();
				Description.Text = _product.Description.ToString();
			}
			else
			{
				_product = new Product();
				isCreated = true;
			}

			try
			{
				UnitCombo.ItemsSource = Helper.Context.Unit.ToList();
				ManufacturerCombo.ItemsSource = Helper.Context.Manufacturer.ToList();
				SupplierCombo.ItemsSource = Helper.Context.Supplier.ToList();
				CategoryCombo.ItemsSource = Helper.Context.Category.ToList();

				ManufacturerCombo.SelectedIndex = 0;
				ManufacturerCombo.DisplayMemberPath = "Name";

				SupplierCombo.SelectedIndex = 0;
				SupplierCombo.DisplayMemberPath = "Name";

				CategoryCombo.SelectedIndex = 0;
				CategoryCombo.DisplayMemberPath = "Name";
			}
			catch (Exception ex)
			{
				Helper.ShowError(ex.Message);
			}

		}

		private BitmapImage ByteToBitmap(byte[] bytes)
		{
			var bitmap = new BitmapImage();
			bitmap.BeginInit();
			bitmap.CacheOption = BitmapCacheOption.OnLoad;
			bitmap.StreamSource = new MemoryStream(bytes);
			bitmap.EndInit();

			return bitmap;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			_product.Manufacturer = ManufacturerCombo.SelectedItem as Manufacturer;
			_product.Supplier = SupplierCombo.SelectedItem as Supplier;
			_product.Category = CategoryCombo.SelectedItem as Category;
			_product.Unit = UnitCombo.SelectedItem as Unit;

			var builder = new StringBuilder();

			if (string.IsNullOrEmpty(Article.Text))
			{
				builder.AppendLine("Вы не указали артикул!");
			}

			if (string.IsNullOrEmpty(ProductName.Text))
			{
				builder.AppendLine("Вы не указали название продукта!");
			}

			if (string.IsNullOrEmpty(Description.Text))
			{
				builder.AppendLine("Вы не указали описание продукта!");
			}

			if (!decimal.TryParse(Cost.Text, out decimal cost)) {
				builder.AppendLine("Вы указали цену в неправильном формате!");
			}

			if (!byte.TryParse(MaxDiscount.Text, out byte maxDiscount))
			{
				builder.AppendLine("Вы указали максимальную скидку в неправильном формате!");

				if (maxDiscount < 0 || maxDiscount > 100)
				{
					builder.AppendLine("Максимальная скидка не может быть меньше 0 или больше 100!");
				}
			}

			if (!byte.TryParse(Discount.Text, out byte discount))
			{
				builder.AppendLine("Вы указали скидку в неправильном формате!");

				if (discount < 0 || discount > 100)
				{
					builder.AppendLine("Скидка не может быть меньше 0 или больше 100!");
				}
			}

			if (!int.TryParse(StorageCount.Text, out int storageCount))
			{
				builder.AppendLine("Вы указали количество на складе в неправильном формате!");
			}

			if (builder.Length > 0)
			{
				Helper.ShowError(builder.ToString());
				return;
			}

			_product.Article = Article.Text;
			_product.Name = ProductName.Text;
			_product.Cost = cost;
			_product.MaxDiscount = maxDiscount;
			_product.Discount = discount;
			_product.StorageCount = storageCount;
			_product.Description = Description.Text;

			if (_loadedImage != null)
			{
				_product.Image = _loadedImage;
			}

			try
			{
				if (isCreated)
				{
					Helper.Context.Product.Add(_product);
				}

				Helper.Context.SaveChanges();
				MessageBox.Show("Данные успешно сохранены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				Helper.ShowError(ex.Message);
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			var dialog = new OpenFileDialog
			{
				DefaultExt = ".jpg",
				Filter = "Image files .jpg|*.jpg"
			};

			if (dialog.ShowDialog() != null)
			{
				if (File.Exists(dialog.FileName))
				{
					_loadedImage = File.ReadAllBytes(dialog.FileName);
					ProductImage.Source = ByteToBitmap(_loadedImage);
				}
			}
		}

		private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Вы уверен?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				try
				{
					Helper.Context.Product.Remove(_product);
					Helper.Context.SaveChanges();
					Helper.NavigationFrame.Navigate(new Catalog());
				}
				catch (Exception ex)
				{
					Helper.ShowError(ex.Message);
				}
			}
		}
	}
}
