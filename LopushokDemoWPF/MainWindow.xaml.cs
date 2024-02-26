using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Collections.Generic;
using LopushokDemoWPF.ADO;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LopushokDemoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int PageSize = 20;
        private int CurrentPage = 0;
        private int TotalCount;
        private int PagesCount;
        private string FilterName;
        private string SearchFilter = "";
        private string SortField;
        private bool IsAsc = false;

        public MainWindow()
        {
            InitializeComponent();

            var filters = new List<ProductType> { new ProductType { Name = "Все типы" } };
            filters.AddRange(App.Connection.ProductType.ToList());
            cbFilter.ItemsSource = filters;
            cbFilter.SelectedIndex = 0;

            var sorts = new List<string> {
                    "Без сортировки",
                    "Наименование", "Номер производственного цеха",
                    "Минимальная стоимость для агента"};
            cbSort.ItemsSource = sorts;
            cbSort.SelectedIndex = 0;

            cbSortOrders.ItemsSource = new List<string> { "По убыванию", "По возрастанию" };
            cbSortOrders.SelectedIndex = 0;

            Loaded += Window_Loaded;
        }

        private void UpdateSource()
        {
            List<Product> products;
            if (string.IsNullOrEmpty(FilterName) || FilterName == "Все типы")
            {
                products = App.Connection.Product.ToList();
            }
            else
            {
                products = App.Connection.Product.ToList().Where(x => x.ProductType.Name == FilterName).ToList();
            }

            products = products.Where(x => x.Name.ToLower().Contains(SearchFilter.ToLower())).ToList();

            TotalCount = products.Count;
            PagesCount = TotalCount / PageSize + (TotalCount % PageSize != 0 ? 1 : 0);
            PagesListBox.ItemsSource = Enumerable.Range(0, PagesCount).Select(x => x + 1);

            if (IsAsc)
            {
                if (SortField == "Наименование")
                {
                    products = products.OrderBy(x => x.Name).ToList();
                }
                else if (SortField == "Номер производственного цеха")
                {
                    products = products.OrderBy(x => x.NumberOfFactory).ToList();
                }
                {
                    products = products.OrderBy(x => x.MinimalAgentCost).ToList();
                }
            }
            else
            {
                if (SortField == "Наименование")
                {
                    products = products.OrderByDescending(x => x.Name).ToList();
                }
                else if (SortField == "Номер производственного цеха")
                {
                    products = products.OrderByDescending(x => x.NumberOfFactory).ToList();
                }
                else if (SortField == "Минимальная стоимость для агента")

                {
                    products = products.OrderByDescending(x => x.MinimalAgentCost).ToList();
                }
            }
            lvProducts.ItemsSource = products.Skip(PageSize * CurrentPage).Take(PageSize);
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

            UpdateSource();
        }
        private void cbSortOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var order = (string)((ComboBox)sender).SelectedItem;
            if (order == "По возрастанию")
            {
                IsAsc = true;
            }
            else
            {
                IsAsc = false;
            }
            UpdateSource();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortField = (string)((ComboBox)sender).SelectedItem;
            UpdateSource();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (ProductType)((ComboBox)sender).SelectedItem;
            if (selectedItem == null)
                return;
            FilterName = selectedItem.Name;
            UpdateSource();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = ((TextBox)sender).Text;
            SearchFilter = text;
            UpdateSource();
        }

        private void PagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = ((ListBox)sender).SelectedItem;
            var pageNumber = (int)listBox - 1;
            CurrentPage = pageNumber;
            UpdateSource();
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = Math.Min(PagesCount - 1, CurrentPage + 1);
            UpdateSource();
        }

        private void PrevPageButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = Math.Max(0, CurrentPage - 1);
            UpdateSource();
        }
    }
}
