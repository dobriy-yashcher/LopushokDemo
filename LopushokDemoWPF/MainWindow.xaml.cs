using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Collections.Generic;
using LopushokDemoWPF.ADO;
using System.Linq;

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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var toggleButton = (ToggleButton)cbSort.
                      Template.FindName("toggleButton", cbSort);
            var toggle1Button = (ToggleButton)cbSort.
                      Template.FindName("toggleButton", cbFilter);

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

        }
    }
}
