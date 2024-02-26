using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Collections.Generic;
using LopushokDemoWPF.ADO;
using System.Linq;
using System;

namespace LopushokDemoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> _source;

        public MainWindow()
        {
            InitializeComponent();
            _source = App.Connection.Product.ToList();
            lvProducts.ItemsSource = _source;

            var filters = new List<ProductType> { new ProductType { Name = "Все типы" } };
            filters.AddRange(App.Connection.ProductType.ToList());
            cbFilter.ItemsSource = filters;
            cbFilter.SelectedIndex = 0;

            var sorts = new List<string> {
                "Без сортировки",
                "Наименование", "Номер производственного цеха ",
                "Минимальная стоимость для агента"
            };
            cbSort.ItemsSource = sorts;
            cbSort.SelectedIndex = 0;

            cbSortOrders.ItemsSource = new List<string> { "По убыванию", "По возрастанию" };
            cbSortOrders.SelectedIndex = 0;

            UpdateSource();
        }

        private void UpdateSource()
        {
            _source = App.Connection.Product
                .ToList();
            Search();
        }

        private void Search()
        {
            lvProducts.ItemsSource = _source
                .Where(x => string.Join(" ", x.Name).ToLower().Contains(tbSearch.Text.ToLower()))
                .ToList();
            //EditRecords();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var toggleButton = (ToggleButton)cbSort.
                      Template.FindName("toggleButton", cbSort);
            var toggle1Button = (ToggleButton)cbSort.
                      Template.FindName("toggleButton", cbFilter);
            var toggle2Button = (ToggleButton)cbSort.
                      Template.FindName("toggleButton", cbSortOrders);

            if (toggleButton != null)
            {
                var border = (Border)toggleButton.
                        Template.FindName("templateRoot", toggleButton);
                if (border != null)
                {
                    border.Background = System.Windows.
                                        Media.Brushes.Transparent;
                }
            }
            if (toggle1Button != null)
            {
                var border = (Border)toggle1Button.
                        Template.FindName("templateRoot", toggle1Button);
                if (border != null)
                {
                    border.Background = System.Windows.
                                        Media.Brushes.Transparent;
                }
            }
            if (toggle2Button != null)
            {
                var border = (Border)toggle2Button.
                        Template.FindName("templateRoot", toggle2Button);
                if (border != null)
                {
                    border.Background = System.Windows.
                                        Media.Brushes.Transparent;
                }
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }
    }
}
