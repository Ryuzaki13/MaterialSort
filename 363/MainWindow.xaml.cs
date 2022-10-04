using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using _363.xbase;

namespace _363
{
	public class ItemSort
	{
		public string DisplayName { get; set; }
		public string PropertyName { get; set; }
		public bool Ascending { get; set; }
	}

	public partial class MainWindow : Window
	{
		public static XBaseEntities Connection = new XBaseEntities();

		public ObservableCollection<Material> Materials { get; set; }

		public ObservableCollection<ItemSort> ItemSorts { get; set; }
			= new ObservableCollection<ItemSort>()
			{
				new ItemSort() { DisplayName= "По названию А-Я", PropertyName = "Name", Ascending = true },
				new ItemSort() { DisplayName= "По названию Я-А", PropertyName = "Name", Ascending = false },
				new ItemSort() { DisplayName= "Отстаток по возрастанию", PropertyName = "InStock", Ascending = true },
				new ItemSort() { DisplayName= "Отстаток по убыванию", PropertyName = "InStock", Ascending = false },
				new ItemSort() { DisplayName= "Стоимость по возрастанию", PropertyName = "Price", Ascending = true },
				new ItemSort() { DisplayName= "Стоимость по убыванию", PropertyName = "Price", Ascending = false },

			};

		public MainWindow()
		{
			InitializeComponent();

			Materials =
				new ObservableCollection<Material>(
						Connection.Material.ToList()
					);

			DataContext = this;
		}

		private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var item = cbSort.SelectedItem as ItemSort;

			var view = CollectionViewSource
				.GetDefaultView(lvMaterial.ItemsSource);

			var direction = item.Ascending ?
				ListSortDirection.Ascending :
				ListSortDirection.Descending;

			view.SortDescriptions.Clear();
			view.SortDescriptions.Add(new SortDescription(item.PropertyName, direction));
		}
	}
}
